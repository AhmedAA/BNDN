using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Text.RegularExpressions;
using ISeeN_DB;
using Newtonsoft.Json.Linq;

namespace ISeenService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SeeNService : ISeeNService
    {
        public enum ErrorCodes
        {
            NoError = 0,
            GeneralError = 1,

        }

        //TODO: REMOVE THIS
        List<Media> _mediaList = new List<Media>(); 

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

                //text search case (Calls database)
                if (textOnly && splitted.Length>0)
                    return new Report<IList<Media>> { Data = MediaDB.SearhForMedia(splitted[0]) };
                //type search case (Calls database)
                if (int.TryParse(splitted[1], out type) && string.IsNullOrEmpty(splitted[0]))
                {
                    return new Report<IList<Media>> { Data = MediaDB.SearhForMedia((MediasEnum)type) };
                }
                //text and type search (Calls database)
                if (int.TryParse(splitted[1], out type))
                {
                    return new Report<IList<Media>> { Data = MediaDB.SearhForMedia(splitted[0],(MediasEnum)type) };
                }

                throw new FileNotFoundException();
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1,e.ToString());
                return new Report<IList<Media>>{Error = 1};
            }
        }

        public Report<Potato> CreateAccount(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get user from JSON string

                var recUser = JObject.Parse(jsonString).ToObject<User>();

                //TODO: ACTUALLY CREATE USER

                //TODO: ACTUALLY GET POTATO FROM SERVER
                return new Report<Potato>() { Data = new Potato { EncPassword = "TODO/ENCRYPT("+recUser.Password+")", Id = recUser.Id} };
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new Report<Potato> { Error = 1 };
            }
        }

        public Report<User> EditAccount(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get potato and user from JSON string

                var jArray = JArray.Parse(jsonString);
                var recPotato = jArray[0].ToObject<Potato>();
                var recUser = jArray[1].ToObject<User>();


                //TODO: Potato check with database that potato is correct for user

                //TODO: Maybe send user from database or just the one we're supposed to set?
                return new Report<User>{Data = recUser};
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new Report<User> {Error = 1};
            }
        }

        public Report<User> GetAccount(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get potato from JSON string

                var recPotato = JObject.Parse(jsonString).ToObject<Potato>();

                //TODO: Get actual user from database

                return new Report<User>
                {
                    Data =
                        new User
                        {
                            Bio = "This is bio",
                            Id = 1,
                            Name = "Name is my",
                            City = "Compton",
                            Country = "The Great Denmark",
                            Email = "This@Is.me",
                            IsAdmin = true,
                            Password = "DECRYPTED(" + recPotato.EncPassword + ")"
                        }
                };
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new Report<User> {Error = 1};
            }
        }

        public Report<Potato> AccountLogin(string email, Stream streamdata)
        {
            try
            {
                LogAction("Login", email);
                var password = StringFromStreamDebug(streamdata);
                
                //TODO: PASSWORD SHOULD BE ENCRYPTED MAYBE

                //TODO: Actually check email & password with database

                return new Report<Potato> {Data = new Potato {EncPassword = "ThisIsMuchEncrypted", Id = 1}};
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new Report<Potato> { Error = 1 };
            }

        }

        public Report<int> DeleteAccount(Stream streamdata)
        {
            //TODO: Not done at all
            StringFromStreamDebug(streamdata);
            return new Report<int>{Data = 1};
        }

        public Report<IList<Media>> GetAllMedia()
        {
            try
            {
                return new Report<IList<Media>> {Data = _mediaList};
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
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
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
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
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new Report<Statistic> { Error = 1 };
            }
        }

        public Report<Media> RentMediaById(string mediaId, Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get potato from JSON string

                var recPotato = JObject.Parse(jsonString).ToObject<Potato>();
                LogAction("Rent","Potato ID#" + recPotato.Id);

                //TODO: Actually rent the movie

                return GetMediaForId(mediaId);

            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new Report<Media> { Error = (int)ErrorCodes.GeneralError };
            }
        }

        public Report<Media> CreateNewMedia(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get potato, media and byte[] from JSON string

                var jArray = JArray.Parse(jsonString);
                var recPotato = jArray[0].ToObject<Potato>();
                var recMedia = jArray[1].ToObject<Media>();
                var recByteAr = jArray[2].ToObject<byte[]>();

                LogAction("New media [" + recMedia.Title + "]", "Potato ID#" + recPotato.Id);

                return new Report<Media> {Data = recMedia};

                //TODO: Actually put this in the database

            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new Report<Media> { Error = (int)ErrorCodes.GeneralError };
            }
        }

        public Report<Media> EditMedia(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get potato, media and byte[] from JSON string

                var jArray = JArray.Parse(jsonString);

                var recPotato = jArray[0].ToObject<Potato>();
                var recMedia = jArray[1].ToObject<Media>();

                //TODO: Check if user has priviledges

                LogAction("Edit media [" + recMedia.Title + "]", "Potato ID#" + recPotato.Id);

                //Is a slow edit (byte array to overwrite)
                if (jArray.Count == 3)
                {
                    var recByteAr = jArray[2].ToObject<byte[]>();
                    //TODO: Actually put this in database
                    Console.WriteLine("Start of byte[] //");
                    foreach (var byt in recByteAr)
                    {
                        Console.WriteLine(byt.ToString());
                    }
                    Console.WriteLine("// End of byte[]");
                }

                return new Report<Media> {Data = recMedia};

                //TODO: Actually put this in the database
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new Report<Media> { Error = (int)ErrorCodes.GeneralError };
            }
        }

        public Report<Media> DeleteMedia(string id, Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get potato from JSON string

                var recPotato = JObject.Parse(jsonString).ToObject<Potato>();

                //TODO: Check if allowed to delete

                LogAction("Delete", "Potato ID#" + recPotato.Id);

                //TODO: ACTUALLY DELETE

                return new Report<Media> {Data = _mediaList[0]};
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new Report<Media> { Error = (int)ErrorCodes.GeneralError };
            }
        }

        public Report<IList<Reminder>> CheckReminders(Stream streamdata)
        {
            var received = StringFromStreamDebug(streamdata);

            var toReturn = new Report<IList<Reminder>> { Data = new List<Reminder>() };
            toReturn.Data.Add(new Reminder{_DateReceived = DateTime.Now, _DateSent = DateTime.MinValue,Id = 1,MediaId = 1,Message = "This is a reminder about blabla...", Title = "Reminder Much Title", UserId = 1});

            return toReturn;
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

        private void LogAction(string whatRequest, string forWho)
        {
            var currentFor = Console.ForegroundColor;
            var currentBac = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(whatRequest + " requested for: " + forWho);
            Console.ForegroundColor = currentFor;
        }

        private void LogError(int error, string message)
        {
            var currentFor = Console.ForegroundColor;
            var currentBac = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR#" + error + " " + message);
            Console.ForegroundColor = currentFor;
        }
    }
}
