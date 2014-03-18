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
            var result = UserDB.GetAllUsers();

            foreach (var re in result)
            {
                Console.WriteLine(re.ToString());
            }

            var resUser = UserDB.GetUserById(10);

            Console.WriteLine(resUser.Id);
            Console.WriteLine(resUser.Name);
            Console.WriteLine(resUser.Bio);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            }
        }
    }
