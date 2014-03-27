using System;
using System.Collections.Generic;
using System.Data;
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

        public static Potato AddUser(User user)
        {
            _context = new ISeeNDbContext();
            using (var db = _context)
            {
                var checkEmail = from u in db.Users
                    where u.Email == user.Email.ToLower()
                    select u;
                if (checkEmail.Any())
                    throw new DuplicateNameException("Email already in database");

                var _user = new User()
                {
                    Name = user.Name,
                    Bio = user.Bio,
                    City = user.City,
                    Country = user.Country,
                    Email = user.Email.ToLower(),
                    Id = user.Id,
                    IsAdmin = user.IsAdmin,
                    Password = user.Password,
                    Gender = user.Gender
                };

                db.Users.Add(_user);
                db.SaveChanges();

                return LoginUser(user.Email, user.Password);
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

                if (!query.Any())
                    throw new ObjectNotFoundException("No user matching id of potato found");

                return query.First();
            }
        }

        public static User EditAccount(Potato potato, User user)
        {
            _context = new ISeeNDbContext();

            //check user and potato match
            MatchUserPotato(user, potato);

            var original = _context.Users.Find(potato.Id);

            //TODO: SHOULD BE SOME CHECKING (e.g. gender is M or F, email is valid etc.)
            if (original != null)
            {
                original.Name = user.Name;
                original.Email = user.Email;
                original.Country = user.Country;
                original.City = user.City;
                original.Bio = user.Bio;
                original.Gender = user.Gender;
                _context.SaveChanges();

                return _context.Users.Find(user.Id);
            }
            throw new ObjectNotFoundException("No user found by that id");
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
                {
                    //TODO: return correct potato
                    return new Potato {EncPassword = "ENCRYPTED(" + tryuser.Password + ")", Id = tryuser.Id};
                }
                else 
                    throw new InvalidDataException("Password did not match");
            }
        }

        public static int DeleteUser(User user, Potato potato)
        {
            MatchUserPotato(user,potato);

            _context = new ISeeNDbContext();

            using (_context)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return 1;
        }

        private static void MatchUserPotato(User user, Potato potato)
        {
            if (potato.Id != user.Id)
                throw new InvalidDataException("Potato id did not match user id");
        }
    }
}
