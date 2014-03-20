using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ISeeN_DB
{
    public class MediaDB
    {
        private static ISeeNDbContext _context;
        private static List<Media> _result;

        public static IList<Media> SearhForMedia(MediasEnum type)
        {
            _context = new ISeeNDbContext();
            _result = new List<Media>();

            using (var db = _context)
            {
                var query = (from m in db.Medias
                    where m.Type == type
                    select m).Take(10);

                foreach (var res in query)
                {
                    _result.Add(res);
                }
            }
            return _result;
        }

        public static IList<Media> SearhForMedia(string searchString)
        {
            _context = new ISeeNDbContext();
            _result = new List<Media>();
            if (searchString == null) throw new ArgumentNullException("searchString");

            using (var db = _context)
            {
                var query = from m in db.Medias
                            where m.Title.Contains(searchString)
                            select m;

                foreach (var res in query)
                {
                    _result.Add(res);
                }
            }
            return _result;
        }

        public static IList<Media> SearhForMedia(string searchString, MediasEnum type)
        {
            _context = new ISeeNDbContext();
            _result = new List<Media>();
            if (searchString == null) throw new ArgumentNullException("searchString");

            using (var db = _context)
            {
                var query = from m in db.Medias
                    where m.Title.Contains(searchString) && m.Type == type
                    select m;

                foreach (var res in query)
                {
                    _result.Add(res);
                }
            }
            return _result;
        }

        public static void AddMedia(Media media)
        {
            _context = new ISeeNDbContext();
            using (var db = _context)
            {
                var _media = new Media()
                {
                    Id = media.Id,
                    Title = media.Title,
                    Description = media.Description,
                    Type = media.Type,
                    ReleaseDate = media.ReleaseDate
                };
                db.Medias.Add(_media);
                db.SaveChanges();
            }
        }

        public static IList<Media> GetAllMedia()
        {
            _context = new ISeeNDbContext();
            _result = new List<Media>();

            using (var db = _context)
            {
                var query = from m in db.Medias
                    orderby m.Title
                    select m;

                foreach (var res in query)
                {
                    _result.Add(res);
                }
            }
            return _result;
        }

        public static Media GetMediaForId(int id)
        {
            _context = new ISeeNDbContext();
            var _mediaRes = new Media();

            using (var db = _context)
            {
                var query = from media in db.Medias
                    where media.Id == id
                    select media;

                foreach (var media in query)
                {
                    _mediaRes.Description = media.Description;
                    _mediaRes.Id = media.Id;
                    _mediaRes.Title = media.Title;
                    _mediaRes.Type = media.Type;
                    _mediaRes.ReleaseDate = media.ReleaseDate;
                }
            }
            return _mediaRes;
        }

        public static void RentMediaById(int id, Potato potato)
        {
            //implement
        }

        public static void EditMedia(Media media)
        {
            //implement
        }

        public static void DeleteMedia(int id)
        {
            _context = new ISeeNDbContext();

            using (var db = _context)
            {
                try
                {
                    var query = (from m in db.Medias
                                 where m.Id == id
                                 select m).First();

                    db.Medias.Remove(query);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("No media with the given ID exists!");
                }
                
            }
        }
    }
}
