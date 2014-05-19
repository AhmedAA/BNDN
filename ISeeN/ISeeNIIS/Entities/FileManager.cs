using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Web;
using System.Web;

namespace ISeeNIIS.Entities
{
    /// <summary>
    /// This is the file manager for the service. It handles saving, removing and getting files for medias
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// path used for media
        /// </summary>
        public static string Path;

        private static void PreCheck()
        {
            //Get current location
            Path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "/Medias";

            //Check media folder exists/create 
            var chck = Directory.Exists(Path);
            if (!chck) Directory.CreateDirectory(Path);
        }

        /// <summary>
        /// Adds or edits the file for a media
        /// </summary>
        /// <param name="id">id of media</param>
        /// <param name="base64">base64string to save in a file</param>
        /// <returns>boolean whether save was a succes or not</returns>
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

        /// <summary>
        /// Removes the file for a media
        /// </summary>
        /// <param name="id">id of media</param>
        /// <returns>boolean whether or not delete was succesful</returns>
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

        /// <summary>
        /// Gets the file for a media id
        /// </summary>
        /// <param name="id">id of media</param>
        /// <returns>base64string</returns>
        public static string GetFile(string id)
        {
            return File.ReadAllText(Path + "/" + id + ".iseen");
        }
    }
}