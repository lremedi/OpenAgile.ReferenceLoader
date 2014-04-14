using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ResolveDlls
{
    public class TFSClass
    {
        public TFSClass()
        {
            TfsTeamProjectCollection TfsServer;
            TfsServer = ConnectToTFS();
            var eventService = (IEventService)TfsServer.GetService(typeof(IEventService));
        }

        public TfsTeamProjectCollection ConnectToTFS()
        {
            try
            {
                var creds = new NetworkCredential("v1deploy", "Versi0n1.c26nu", "\\");
                var tfsServer = new TfsTeamProjectCollection(new Uri("http://ec2-54-198-234-132.compute-1.amazonaws.com:8080/tfs"), creds);
                tfsServer.Authenticate();
                return tfsServer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
