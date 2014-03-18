using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
            // testing UserDB.AddUser()
            var user = new ISeeN_DB.User()
            {
                Name = "Rasmus Løbner Christensen",
                Bio = "Flot fyr i sin bedste alder",
                City = "Frederiksberg",
                Country = "Denmark",
                Email = "rloc@itu.dk",
                Id = 1337,
                IsAdmin = true,
                Password = "Hemmeligt!"
            };

            // calling the method
            UserDB.AddUser(user);

            using (var db = new ISeeNDbContext())
            {

                var query = from b in db.Users
                    orderby b.Id
                    select b;

                // write result to nice console
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
