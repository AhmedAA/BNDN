using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Web;
using System.Web;

namespace ISeeNIIS.Entities
{
    public class FileManager
    {
        public static string Path;

        private static void PreCheck()
        {
            //Get current location
            Path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "/Medias";

            //Check media folder exists/create 
            var chck = Directory.Exists(Path);
            if (!chck) Directory.CreateDirectory(Path);
        }

        public static bool AddEditMedia(int id, string base64)
        {
            try
            {
                PreCheck();
                File.WriteAllText(Path + "/" + id + ".iseen", base64);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
                
        }

        public static bool RemoveMedia(int id)
        {
            try
            {
                PreCheck();
                if (File.Exists(Path + "/" + id + ".iseen"))
                    File.Delete(Path + "/" + id + ".iseen");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        public static string GetFile(string id)
        {
            return File.ReadAllText(Path + "/" + id + ".iseen");
        }
    }
}