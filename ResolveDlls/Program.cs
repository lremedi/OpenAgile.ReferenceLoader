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
        static Program()
        {
            Dictionary<string, string[]> supportedDlls=new Dictionary<string, string[]>();
            supportedDlls.Add(
                "Microsoft.TeamFoundation.Client",
                new string[]{ 
                                    "Microsoft.TeamFoundation.Client, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
                                    "Microsoft.TeamFoundation.Client, Version=11.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
                                    "Microsoft.TeamFoundation.Client, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"}
            );
            OpenAgile.ReferenceLoader.ResolveDlls(AppDomain.CurrentDomain, supportedDlls);
        }
        static void Main(string[] args)
        {
            try
            {
                Console.ReadKey();
                TFSClass tfsclass = new TFSClass();
                Console.Write(String.Join(",", AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("TeamFoundation")).Select(x => x.FullName).ToArray()));
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
