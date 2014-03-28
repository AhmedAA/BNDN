using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ISeeNEntityModel.Funcs
{
    public class UserDB
    {
        public static Potato AddUser(User user)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                //first check if email already exists
                var chk = from u in conc.UserSet
                    where u.Email == user.Email
                    select u;
                if (chk.Any())
                    throw new DuplicateNameException("Email already in database");

                //add user
                conc.UserSet.Add(user);
                conc.SaveChanges();
                //add potato
                var newUser = chk.First();
                var potato = new Potato { EncPassword = user.Password.GetHashCode().ToString(), Id = newUser.Id };
                conc.PotatoSet.Add(potato);

                conc.SaveChanges();

                //return potato
                return LoginUser(user.Email, user.Password);
            }
        }

        public static Potato LoginUser (string email, string password)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                var emailqur = from u in conc.UserSet
                    where u.Email == email
                    select u;
                if (emailqur.First().Password == password)
                    return conc.PotatoSet.Find(emailqur.First().Id);
                throw new InvalidCredentialException("Password was not correct");
            }
        }

        public static User EditAccount(Potato recPotato, User recUser)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                //first check potato is matched
                var dbPotato = conc.PotatoSet.Find(recPotato.Id);

                if (dbPotato.EncPassword != recPotato.EncPassword)
                    throw new InvalidCredentialException("Potato was not correct");

                //check if email already exists
                var chk = from u in conc.UserSet
                          where u.Email == recUser.Email
                          select u;
                if (chk.Any())
                    throw new DuplicateNameException("Email already in database");

                //User to update
                var olduser = conc.UserSet.Find(recPotato.Id);

                //overwrite user
                conc.Entry(olduser).CurrentValues.SetValues(recUser);
                conc.SaveChanges();

                return GetUserByPotato(recPotato);
            }
        }

        public static User GetUserByPotato(Potato recPotato)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                var qur = from u in conc.UserSet
                    where u.Id == recPotato.Id
                    select u;

                var quk = from p in conc.PotatoSet
                    where p.Id == recPotato.Id && p.EncPassword == recPotato.EncPassword
                    select p;

                if (qur.First().Id == quk.First().Id)
                    return qur.First();
                else
                    throw new InvalidCredentialException("Potato was not correct");
            }
        }

        public static int DeleteUser(Potato recPotato)
        {
            var userToDelete = GetUserByPotato(recPotato);
            var conc = new RentIt02Entities();
            using (conc)
            {
                conc.UserSet.Attach(userToDelete);
                conc.UserSet.Remove(userToDelete);
                conc.SaveChanges();
            }

            return 1;
        }
    }
}
