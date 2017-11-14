namespace payload
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("powershell.exe");
            psi.Arguments = "-c Invoke-WebRequest -Uri www.google.com";
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = psi;
            process.Start();
            string s = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            var x = new System.IO.DirectoryInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Parent.FullName;            
            using (System.IO.StreamWriter outfile = new System.IO.StreamWriter(x + "\\psInvokeWebReq.txt", true))
            {
                outfile.Write(s);
            }
            System.Diagnostics.Process.Start(psi);
        }
    }
}
