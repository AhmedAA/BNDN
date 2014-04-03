using System.Data;
using System.Linq;
using System.Security.Authentication;
using ISeeNEntityModel.POCO;

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
                    return PotatoHandle(emailqur.First().Id);
                throw new InvalidCredentialException("Password was not correct");
            }
        }

        public static User EditAccount(Potato recPotato, User recUser)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                //first check potato is matched
                if (PotatoHandle(recPotato.Id).EncPassword != recPotato.EncPassword)
                    throw new InvalidCredentialException("Potato was not correct");

                //check if email already exists
                var chk = from u in conc.UserSet
                          where u.Email == recUser.Email && u.Id!=recUser.Id
                          select u;
                if (chk.Any())
                    throw new DuplicateNameException("Email already in database");

                //User to update
                var olduser = conc.UserSet.Find(recPotato.Id);

                //overwrite user
                conc.Entry(olduser).CurrentValues.SetValues(recUser);
                conc.SaveChanges();

                return olduser;
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

                //check potato match
                if (PotatoHandle(recPotato.Id).EncPassword != recPotato.EncPassword)
                    throw new InvalidCredentialException("Potato was not correct");
               
                return qur.First();
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

        public static Potato PotatoHandle(int Id)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                var user = conc.UserSet.Find(Id);

                //new potato based on (not so) wild security
                var potato = new Potato
                {
                    Id = Id,
                    EncPassword = (user.Password.GetHashCode() + 1337).ToString().Replace('1', 'a').Replace('5', 'p')
                };

                return potato;
            }
        }
    }
}
