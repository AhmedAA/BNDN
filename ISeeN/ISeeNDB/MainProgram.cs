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
            // testing
            var media0 = new ISeeN_DB.Media()
            {
                Title = "1. Film",
                Id = 1337,
                Type = MediasEnum.Movie
            };
            var media1 = new ISeeN_DB.Media()
            {
                Title = "2. Film",
                Id = 1337,
                Type = MediasEnum.Movie
            };
            var media2 = new ISeeN_DB.Media()
            {
                Title = "3. Film",
                Id = 1337,
                Type = MediasEnum.Movie
            };

            MediaDB.AddMedia(media0);
            MediaDB.AddMedia(media1);
            MediaDB.AddMedia(media2);

            var searchResult = MediaDB.SearhForMedia("Film", MediasEnum.Movie);

            foreach (var elem in searchResult)
            {
                Console.WriteLine(elem.Title);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            }
        }
    }
