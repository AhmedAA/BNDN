using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using ISeeNEntityModel.POCO;

namespace ISeeNEntityModel.Funcs
{
    /// <summary>
    /// This class handles the rental related database calls
    /// </summary>
    public class RentalDB
    {
        /// <summary>
        /// Gets all rentals for a user
        /// </summary>
        /// <param name="potato">Potato of user to get rentals for</param>
        /// <returns>IList of Rentals for the user</returns>
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

        /// <summary>
        /// Checks if a user rented a specific media
        /// </summary>
        /// <param name="mediaid">Media id</param>
        /// <param name="potato">Potato for user
        /// </param>
        /// <returns>Boolean whether the use has rented the media or not</returns>
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
