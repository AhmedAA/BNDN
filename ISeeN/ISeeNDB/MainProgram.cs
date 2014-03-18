using System;
using System.Collections.Generic;
using System.Linq;
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
            using (var db = new ISeeNDbContext())
            {
                var user = new ISeeN_DB.User { Name = "Carsten" };
                var potato = new ISeeN_DB.Potato { Id = 10, EncPassword = "Hemmeligt Kodeord!" };
                var reminder = new ISeeN_DB.Reminder { Id = 10, Title = "Reminder!" };
                var media = new ISeeN_DB.Media { Id = 10, Title = "MediaTitle" };
                var statistic = new ISeeN_DB.Statistic { MediaId = 10 };
                var movies = new ISeeN_DB.Movie { Director = "Steven S." };
                var pictures = new ISeeN_DB.Picture { Author = "Chigal" };
                var music = new ISeeN_DB.Music { Artist = "Slim Shady" };
                db.Users.Add(user);
                db.Potatoes.Add(potato);
                db.Reminders.Add(reminder);
                db.Medias.Add(media);
                db.Statistics.Add(statistic);
                db.Movies.Add(movies);
                db.Pictures.Add(pictures);
                db.Music.Add(music);
                db.SaveChanges();

                // Query to print out data
                var query = from b in db.Users
                            orderby b.Id
                            select b;

                Console.WriteLine("Current query output:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            } 
        }
    }
}
