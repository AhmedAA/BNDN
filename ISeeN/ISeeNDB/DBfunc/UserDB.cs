using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ISeeN_DB
{
    public class UserDB
    {
        private static ISeeNDbContext _context;

        public static void AddUser(User user)
        {
            _context = new ISeeNDbContext();
            using (var db = _context)
            {
                var _user = new User()
                {
                    Name = user.Name,
                    Bio = user.Bio,
                    City = user.City,
                    Country = user.Country,
                    Email = user.Email,
                    Id = user.Id,
                    IsAdmin = user.IsAdmin,
                    Password = user.Password,
                    Gender = user.Gender
                };

                db.Users.Add(_user);
                db.SaveChanges();

            }
        }

        public static List<int> GetAllUsers()
        {
            _context = new ISeeNDbContext();
            var _allUsers = new List<int>();
            using (var db = _context)
            {
                var query = from b in db.Users
                    orderby b.Id
                    select b;

                _allUsers.AddRange(query.Select(item => item.Id));
            }
            return _allUsers;
        }

        public static User GetUserByPotato(Potato potato)
        {
            _context = new ISeeNDbContext();
            var _userid = potato.Id;
            using (var db = _context)
            {
                var query = from b in db.Users
                    where b.Id == _userid
                    select b;

                return query.First();
            }
        }

        public static void EditAccount(Potato potato, User user)
        {
            //implement
        }

        public static Potato LoginUser(string email, string password)
        {
            _context = new ISeeNDbContext();

            using (_context)
            {
                var query = from u in _context.Users
                    where email == u.Email
                    select u;

                if (!query.Any())
                    throw new ObjectNotFoundException("Email not found in database");

                var tryuser = query.First();

                if (tryuser.Password == password)
                    //return correct potato
                    throw new NotImplementedException();
                else 
                    throw new InvalidDataException("Password did not match");
            }
        }

        public static void DeleteUser(User user, Potato potato)
        {
            //implement
        }
    }
}
