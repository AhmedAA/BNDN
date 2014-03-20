using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ISeeN_DB
{
    class UserDB
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
                    Password = user.Password
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

        public static User GetUserById(int userid)
        {
            _context = new ISeeNDbContext();
            var _userid = userid;
            var _user = new User();
            using (var db = _context)
            {
                var query = from b in db.Users
                    where b.Id == _userid
                    select b;

                foreach (var m in query)
                {
                    _user.Id = m.Id;
                    _user.Bio = m.Bio;
                    _user.City = m.City;
                    _user.Country = m.Country;
                    _user.Email = m.Email;
                    _user.IsAdmin = m.IsAdmin;
                    _user.Name = m.Name;
                    _user.Password = m.Password;
                }
            }
            return _user;
        }

        public static void EditAccount(Potato potato, User user)
        {
            //implement
        }

        public static void LoginUser(User user)
        {
            //implement
        }

        public static void DeleteUser(User user, Potato potato)
        {
            //implement
        }
    }
}
