using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ISeeN_DB;

namespace ISeeN_DB
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            // testing stuff

            var media = new Media()
            {
                Id = 10,
                Description = "Description",
                Title = "Titel 1",
                Type = MediasEnum.Movie
            };

            var media1 = new Media()
            {
                Id = 10,
                Description = "Description",
                Title = "Titel 2",
                Type = MediasEnum.Movie
            };

            var media2 = new Media()
            {
                Id = 10,
                Description = "Description",
                Title = "Titel 3",
                Type = MediasEnum.Movie
            };

            MediaDB.AddMedia(media);
            MediaDB.AddMedia(media1);
            MediaDB.AddMedia(media2);

            var snask = MediaDB.GetAllMedia();

            foreach (var elem in snask)
            {
               Console.WriteLine(elem.Title);
            }

            Console.WriteLine("Press any key to terminate...");
            Console.ReadKey();
            }
        }
    }
