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
                // Create and save a new Blog 
                Console.Write("Enter a name for a new User: ");
                var name = Console.ReadLine();

                var user = new ISeeN_DB.User { Name = name };
                db.Users.Add(user);
                db.SaveChanges();

                // Display all Blogs from the database 
                var query = from b in db.Users
                            orderby b.Name
                            select b;

                Console.WriteLine("All users in the database:");
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
