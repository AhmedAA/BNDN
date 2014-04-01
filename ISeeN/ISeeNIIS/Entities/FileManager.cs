using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ISeeNIIS.Entities
{
    public class FileManager
    {
        public static readonly string path;

        private static void PreCheck()
        {
            //Get current location
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+"/Medias";

            //Check media folder exists/create
            var chck = Directory.Exists(path);
            if (!chck) Directory.CreateDirectory(path);
        }

        public static void AddMedia(int id, byte[] recByteAr)
        {
            PreCheck();

        }

        public static void EditMedia(int id, byte[] recByteAr)
        {
            PreCheck();
        }

        public static void RemoveMedia(int parse)
        {
            PreCheck();
        }
    }
}