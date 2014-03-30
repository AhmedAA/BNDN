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
    public class MediaDB
    {
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

        public static Media EditMedia(Potato potato, Media media)
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

                //media to update
                var oldmedia = conc.MediaSet.Find(media.Id);

                //overwrite media
                conc.Entry(oldmedia).CurrentValues.SetValues(media);
                conc.SaveChanges();

                return oldmedia;
            }
        }

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
    }
}
