using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ISeeN_DB
{
    class MediaDB
    {
        private static ISeeNDbContext _context;
        private static List<Media> _result; 

        public static List<Media> SearhForMedia(string searchString, MediasEnum type)
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
    }
}
