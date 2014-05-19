using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ISeeNEntityModel.POCO;

namespace ISeeNEntityModel.Funcs
{
    /// <summary>
    /// This class handles the media related database calls
    /// </summary>
    public class MediaDB
    {
        /// <summary>
        /// This method adds a media to the database, if it it doesn't already exist
        /// </summary>
        /// <param name="media">media to add(id is ignored)</param>
        /// <returns>added media (id is correct)</returns>
        public static Media AddMedia(Media media)
        {
            //TODO: CHECK IF ADMIN

            var conc = new RentIt02Entities();
            using (conc)
            {
                //identical media
                var query = from m in conc.MediaSet
                    where m.Title.ToLower() == media.Title.ToLower() && m.Type == media.Type
                    select m;

                if (query.Any())
                    throw new DuplicateNameException("Identical media already found. Consider edit");

                //add media
                conc.MediaSet.Add(media);
                conc.SaveChanges();

                //return potato
                return media;
            }
        }

        /// <summary>
        /// Searches the database with the text parameter
        /// </summary>
        /// <param name="textParam">text parameters</param>
        /// <returns>Matches, ordered by title length.</returns>
        public static IList<Media> SearchText(string textParam)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                var query = from m in conc.MediaSet
                    where m.Title.ToLower().Contains(textParam.ToLower())
                    orderby m.Title.Length
                    select m;

                return query.Take(50).ToList();
            }
        }

        /// <summary>
        /// Searches the database by type
        /// </summary>
        /// <param name="type">type of media to look for</param>
        /// <returns>Matches, ordered by title length.</returns>
        public static IList<Media> SearchType(int type)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                var query = from m in conc.MediaSet
                    where m.Type == type.ToString()
                    orderby m.Title
                    select m;

                return query.Take(50).ToList();
            }
        }

        /// <summary>
        /// Searches by both text param and type
        /// </summary>
        /// <param name="textParam">text parameters</param>
        /// <param name="type">type of media to look for</param>
        /// <returns>Matches, ordered by title length.</returns>
        public static IList<Media> SearchBoth(string textParam, int type)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                var query = from m in conc.MediaSet
                    where m.Title.ToLower().Contains(textParam.ToLower()) && m.Type == type.ToString()
                    orderby m.Title.Length
                    select m;

                return query.Take(50).ToList();
            }
        } 

        /// <summary>
        /// Gets all media in the database
        /// </summary>
        /// <returns>List of all media</returns>
        public static IList<Media> GetAll()
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                var query = from m in conc.MediaSet
                    select m;

                return query.ToList();
            }
        }

        /// <summary>
        /// Gets a specific media by id
        /// </summary>
        /// <param name="id">Id of media to return</param>
        /// <returns>Media matching id</returns>
        public static Media GetMediaForId(int id)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                var query = from m in conc.MediaSet
                            where m.Id == id
                            select m;

                var list = query.ToList();

                //check if found
                if (!list.Any())
                    throw new KeyNotFoundException("No media by that id");
                
                return list.First();
            }
        }

        /// <summary>
        /// Deletes a media by id
        /// </summary>
        /// <param name="id">Id of media</param>
        /// <param name="potato">Potato for user deleting</param>
        /// <returns>returns 1 if deleted</returns>
        public static int DeleteMedia(int id, Potato potato)
        {
            //TODO: CHECK IF ADMIN

            var mediaToDelete = GetMediaForId(id);
            var conc = new RentIt02Entities();
            using (conc)
            {
                conc.MediaSet.Attach(mediaToDelete);
                conc.MediaSet.Remove(mediaToDelete);
                conc.SaveChanges();
            }

            return 1;
        }

        /// <summary>
        /// Edits a media
        /// </summary>
        /// <param name="potato">Potato for user editing</param>
        /// <param name="media">Edited media, id will be used for overwriting current</param>
        /// <returns>Edited media</returns>
        public static Media EditMedia(Potato potato, Media media)
        {
            //TODO: CHECK IF ADMIN

            var conc = new RentIt02Entities();
            using (conc)
            {
                //identical media
                var query = from m in conc.MediaSet
                            where m.Title.ToLower() == media.Title.ToLower() && m.Type == media.Type && m.Id != media.Id
                            select m;

                if (query.Any())
                    throw new DuplicateNameException("Identical media already found. Consider edit");

                //media to update
                var oldmedia = conc.MediaSet.Find(media.Id);

                //overwrite media
                conc.Entry(oldmedia).CurrentValues.SetValues(media);
                conc.SaveChanges();

                return oldmedia;
            }
        }

        /// <summary>
        /// Rents a media for a user
        /// </summary>
        /// <param name="id">Media id for rent</param>
        /// <param name="recPotato">Potato for user renting</param>
        /// <returns>Media rented</returns>
        public static Media RentMedia(int id, Potato recPotato)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                //get media
                var media = conc.MediaSet.Find(id);

                //check if there is a media by that id
                if (media == null)
                    throw new KeyNotFoundException("No media by that id");

                //get user by potato
                var user = UserDB.GetUserByPotato(recPotato);

                //new rental
                var rental = new Rental {Id = 1, MediaId = id, UserId = user.Id};

                conc.RentalSet.Add(rental);
                conc.SaveChanges();

                return media;
            }
        }

        /// <summary>
        /// Gets stats for a media by id
        /// </summary>
        /// <param name="id">Media id</param>
        /// <returns>Statistics for this media</returns>
        public static Statistic GetStatsForId(int id)
        {
            var conc = new RentIt02Entities();
            using (conc)
            {
                //get media
                var media = conc.MediaSet.Find(id);

                //check if there is a media by that id
                if (media == null)
                    throw new KeyNotFoundException("No media by that id");

                //get rentals for this media id
                var query = from r in conc.RentalSet
                    where r.MediaId == media.Id
                    select r.User.Country;

                //get amount of all rentals
                var totalRents = conc.RentalSet.Count();

                var thisCountries = query.ToList();

                var thisRents = thisCountries.Count();

                //put into statistic
                var stats = new Statistic
                {
                    MediaId = media.Id,
                    ThisAmountOfRents = thisRents,
                    TotalAmountOfRents = totalRents,
                    CountriesRent = thisCountries
                };

                return stats;
            }
        }
    }
}
