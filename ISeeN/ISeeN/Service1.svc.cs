using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ISeeN.Entities;
using ISeeN.TestRelated_Entities;

namespace ISeeN
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        readonly List<IMedia> _database = new List<IMedia>
            {
                new TMedia
                {
                    Description = "TestDesc",
                    Id = 1,
                    ReleaseDate = new DateTime(1993, 12, 21),
                    Title = "Test Media",
                    Type = 1
                },
                new TMovie
                {
                    Description = "TestDesc",
                    Id = 1,
                    ReleaseDate = new DateTime(1993, 12, 21),
                    Title = "Test Media",
                    Type = 2,
                    SomeMovieField = "SomeSpecific",
                    SomethingElse = 11
                }
            };

        public string test(string t)
        {
            return t;
        }

        public Report<IList<IMedia>> SearchMediaByName(string searchParam)
        {
            var toReturn = new Report<IList<IMedia>>();

            if (searchParam.ToLower().Equals("work"))
                toReturn.Data = _database;
            else
                toReturn.Error = 1;

            return toReturn;
        }

        public Report<IList<IMedia>> SearchMediaByType(int type)
        {
            var toReturn = new Report<IList<IMedia>>();
            var tmp = from data in _database
                where data.Type == type
                select data;

            if (!tmp.Any())
                toReturn.Error = 1;
            else
                toReturn.Data = tmp.ToList();

            return toReturn;
        }

        public Report<IList<IMedia>> SearchMediaByNameType(string searchParam, int type)
        {
            return new Report<IList<IMedia>>{Error = 1};

        }

        public Report<Potato> CreateAccount(User newUser)
        {
            return new Report<Potato>{Data = new Potato{EncPassword = "SuchEncryptionMuchWow", Id = newUser.Id}};
        }

        public Report<Potato> Login(string email, string password)
        {
            return new Report<Potato> { Data = new Potato { EncPassword = "SuchEncryptionMuchWow", Id = 1 } };
        }

        public Report<User> GetAccountInfo(Potato potato)
        {
            return new Report<User>
            {
                Data =
                    new User
                    {
                        Bio = "This is bio",
                        City = "Such City",
                        Country = "Much Country",
                        Email = "Very@Email.com",
                        Id = 1,
                        IsAdmin = false,
                        Password = "ThisIsPassword!!"
                    }
            };
        }

        public Report<User> SetAccountInfo(Potato potato, User editedUser)
        {
            return new Report<User> {Data = editedUser};
        }

        public Report<IMedia> RentMedia(int id, Potato potato)
        {
            var tmp = from data in _database
                where data.Id == id
                select data;

            var toReturn = new Report<IMedia>();

            if (!tmp.Any())
                toReturn.Error = 1;
            else
                toReturn.Data = tmp.First();

            return toReturn;
        }

        public Report<IMedia> NewMedia(IMedia media, byte[] file)
        {
            return new Report<IMedia>{Data = media};
        }

        public Report<IMedia> GetMediaById(int id)
        {
            return RentMedia(id,new Potato());
        }

        public Report<Statistic> GetStatisticsForMedia(int id)
        {
            return new Report<Statistic>
            {
                Data = new Statistic
                {
                    DatesRented =
                        new List<DateTime>
                    {
                        DateTime.Now,
                        DateTime.Now,
                        DateTime.Now,
                        DateTime.Now,
                        DateTime.Now,
                        DateTime.Now,
                        DateTime.Now,
                        new DateTime(1993, 12, 21)
                    },
                    MediaId = id
                }
            };
        }

        public Report<IList<Reminder>> CheckReminders(Potato potato)
        {
            var toReturn = new Report<IList<Reminder>>
            {
                Data = new List<Reminder>
                {
                    new Reminder
                    {
                        DateReceived = DateTime.Now,
                        DateSent = DateTime.Now,
                        Id = 1,
                        MediaId = 1,
                        Title = "This is a reminder :-)",
                        Message = "Lorum ipsum this is awsum",
                        UserId = potato.Id
                    }
                }
            };

            return toReturn;
        }
    }
}
