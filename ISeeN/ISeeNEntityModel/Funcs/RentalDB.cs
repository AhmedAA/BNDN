using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using ISeeNEntityModel.POCO;

namespace ISeeNEntityModel.Funcs
{
    public class RentalDB
    {
        public static IList<Rental> RentalsForUser(Potato potato)
        {
            //check potato is valid
            var valPotato = UserDB.PotatoHandle(potato.Id);
            if (valPotato.EncPassword == potato.EncPassword)
            {
                var conc = new RentIt02Entities();
                using (conc)
                {
                    var query = from r in conc.RentalSet
                        where r.UserId == potato.Id
                        select r;

                    return query.ToList();
                }
            }
            //potato did not match
            throw new InvalidCredentialException("Potato did not match");
        }

        public static bool CheckUserRented(int mediaid, Potato potato)
        {
            //check potato is valid
            var valPotato = UserDB.PotatoHandle(potato.Id);
            if (valPotato.EncPassword == potato.EncPassword)
            {
                var conc = new RentIt02Entities();
                using (conc)
                {
                    var query = from r in conc.RentalSet
                                where r.UserId == potato.Id && r.MediaId == mediaid
                                select r;

                    //to bool
                    if (query.Any())
                        return true;

                    return false;
                }
            }
            //potato did not match
            throw new InvalidCredentialException("Potato did not match");
        }
    }
}
