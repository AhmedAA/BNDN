using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISeeN_DB
{
    class UserDB
    {
        private static ISeeNDbContext _context;

        public static void AddUser(ISeeN_DB.User user)
        {
            _context = new ISeeNDbContext();
            using (var db = _context)
            {
                var _user = new ISeeN_DB.User()
                {
                    Name = user.Name,
                    Bio = user.Bio,
                    City = user.City,
                    Country = user.Country,
                    Email = user.Email,
                    Id = user.Id,
                    IsAdmin = user.IsAdmin,
                    Password = user.Password
                };

                db.Users.Add(_user);
                db.SaveChanges();

            }
        }

        public IList<int> GetAllUsers()
        {
            // implement
            return null;
        }

        public int GetUserById(ISeeN_DB.User user)
        {
            // implement
            return 0;
        }
    }
}
