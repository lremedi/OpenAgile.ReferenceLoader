using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ResolveDlls
{
    class Program
    {
        static Dictionary<string, string[]> supportedDlls;
        static void Main(string[] args)
        {
            try
            {
                supportedDlls = new Dictionary<string, string[]>();
                supportedDlls.Add(
                    "Microsoft.TeamFoundation.Client", 
                    new string[]{ 
                        "Microsoft.TeamFoundation.Client, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
                        "Microsoft.TeamFoundation.Client, Version=11.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
                        "Microsoft.TeamFoundation.Client, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"}
                );
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                TFSClass tfsclass = new TFSClass();

                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            Assembly assembly = resolveSupportedAssembly(supportedDlls[args.Name.Split(',').First()]);
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            return assembly;
        }

        static Assembly resolveSupportedAssembly(string[] versions)
        {
            Assembly assembly = null;
            foreach (string version in versions)
            {
                try
                {
                    assembly = Assembly.Load(version);
                    return assembly;
                }
                catch{}
            }
            return assembly;
        }
    }
}
