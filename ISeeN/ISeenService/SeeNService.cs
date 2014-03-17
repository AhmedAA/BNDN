using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Text.RegularExpressions;
using ISeeN_DB;
using ISeeN_DB.Entities;

namespace ISeenService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SeeNService : ISeeNService
    {
        readonly List<Media> _mediaList = new List<Media>
        #region TestRelatedMEDIA
            {
                new Media
                {
                    Description = "TestDesc",
                    Id = 1,
                    _ReleaseDate = new DateTime(1993, 11, 21),
                    Title = "Media",
                    Type = 1
                },
                new Media()
                {
                    Description = "Description blabla",
                    Id = 2,
                    _ReleaseDate = new DateTime(1992, 12, 21),
                    Title = "Test",
                    Type = 2
                },
                new Media()
                {
                    Description = "This is a test",
                    Id = 3,
                    _ReleaseDate = new DateTime(1991, 12, 21),
                    Title = "GoodFellas",
                    Type = 1
                }
            };
        #endregion

        public Report<IList<Media>> SearchMedia(string searchParam)
        {
            try
            {
                //first lets take out textParam and typeParam
                var splitted = Regex.Split(searchParam, "==");
                //determine if text only or both text and type
                bool textOnly = splitted.Length < 2;
                int type;

                var toReturn = new Report<IList<Media>>();

                //text search case
                if (textOnly)
                    return new Report<IList<Media>> { Data = MediaBySearchText(splitted[0], _mediaList).ToList() };
                //type search case
                if (int.TryParse(splitted[1], out type) && string.IsNullOrEmpty(splitted[0]))
                {
                    return new Report<IList<Media>> { Data = MediaBySearchType(type, _mediaList).ToList() };
                }
                //text and type search
                if (int.TryParse(splitted[1], out type))
                {
                    var byText = MediaBySearchText(splitted[0], _mediaList);
                    var byBoth = MediaBySearchType(type, byText);
                    return new Report<IList<Media>> { Data = byBoth.ToList() };
                }

                throw new FileNotFoundException();
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                return new Report<IList<Media>>{Error = 1};
            }
        }

        public Report<Potato> CreateAccount(User newUser)
        {
            try
            {
                return new Report<Potato>() { Data = new Potato { EncPassword = "ThisIsVeryEncrypted", Id = 2 } };
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                return new Report<Potato> { Error = 1 };
            }
        }

        public Report<IList<Media>> GetAllMedia()
        {
            try
            {
                return new Report<IList<Media>> {Data = _mediaList};
            }
            catch (Exception)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                return new Report<IList<Media>> { Error = 1 };
            }
        }

        public Report<Media> GetMediaForId(string id)
        {
            try
            {
                var search = from media in _mediaList
                    where media.Id == int.Parse(id)
                    select media;
                if (search.Any())
                    return new Report<Media> {Data = search.First()};

                throw new FileNotFoundException();
            }
            catch (Exception)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                return new Report<Media> { Error = 1 };
            }
        }

        public Report<Statistic> GetStatsForId(string id)
        {
            try
            {
                if (int.Parse(id) == 1)
                {
                    var toReturn = new Report<Statistic>
                    {
                        Data = new Statistic
                        {
                            _DatesRented = new List<DateTime>
                            {
                                DateTime.Now,
                                DateTime.Now,
                                DateTime.Now,
                                DateTime.Now,
                                DateTime.Now,
                                DateTime.Now
                            },
                            MediaId = 1
                        }
                    };

                    return toReturn;
                }
                throw new ArgumentException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //TODO: IMPLEMENT REAL ERROR CODE
                return new Report<Statistic> { Error = 1 };
            }
        }

        public Report<IList<Reminder>> CheckReminders(Stream streamdata)
        {
            var received = StringFromStreamDebug(streamdata);

            var toReturn = new Report<IList<Reminder>> { Data = new List<Reminder>() };
            toReturn.Data.Add(new Reminder{_DateReceived = DateTime.Now, _DateSent = DateTime.MinValue,Id = 1,MediaId = 1,Message = "This is a reminder about blabla...", Title = "Reminder Much Title", UserId = 1});

            return toReturn;
        }

        private string StringFromStreamDebug(Stream stream)
        {
            var s = StringFromStream(stream);
            var currentFor = Console.ForegroundColor;
            var currentBac = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Stream = |" + s + "|");
            Console.ForegroundColor = currentFor;
            return s;
        }

        private string StringFromStream(Stream stream)
        {
            string s;
            using (var reader = new StreamReader(stream))
            {
                s = reader.ReadToEnd();
            }

            return s;
        }

        private IEnumerable<Media> MediaBySearchText(string s, IEnumerable<Media> medias)
        {
            return from media in medias
                   where media.Title.Contains(s)
                   select media;
        }

        private IEnumerable<Media> MediaBySearchType(int type, IEnumerable<Media> medias)
        {
            return from media in medias
                   where media.Id == type
                   select media;
        }
    }
}
