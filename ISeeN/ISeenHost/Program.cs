using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ISeenHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //Prompt for admin rights
            var pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
            if (!hasAdministrativeRight)
            {
                // relaunch the application with admin rights
                string fileName = Assembly.GetExecutingAssembly().Location;
                var processInfo = new ProcessStartInfo();
                processInfo.Verb = "runas";
                processInfo.FileName = fileName;

                try
                {
                    Process.Start(processInfo);
                }
                catch (Win32Exception)
                {
                    // This will be thrown if the user cancels the prompt
                }

                return;
            }


            //Actual host
            try
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Uri httpUrl = new Uri("http://localhost:8090/service/SeeNService");
                WebServiceHost host = new WebServiceHost(typeof(ISeenService.SeeNService), httpUrl);
                host.Open();

                foreach (ServiceEndpoint se in host.Description.Endpoints)
                    Console.WriteLine("Service is hosted at endpoint: " + se.Address + "\n");
                //Console.WriteLine("ASP.Net : " + ServiceHostingEnvironment.AspNetCompatibilityEnabled);
                Console.WriteLine("Host is running... Press <Enter> key to stop");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("EXCEPTION");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
