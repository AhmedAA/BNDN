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

        public static bool AddEditMedia(int id, byte[] recByteAr)
        {
            try
            {
                PreCheck();
                File.WriteAllBytes(Path + "/" + id + ".iseen", recByteAr);
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
    }
}