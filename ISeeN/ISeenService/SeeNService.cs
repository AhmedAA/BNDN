using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using ISeeN_DB;
using Newtonsoft.Json;
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

        public Stream Options()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "GET");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Max-Age", "1728000");

            return Message("Ok");
        }

        private Stream Message(string Json)
        {
            //stuff that should be done before returning

            //CORS Headers added

            // ReSharper disable once PossibleNullReferenceException
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            var toReturn = new MemoryStream(Encoding.UTF8.GetBytes(Json));

            return toReturn;
        }

        public Stream Test1()
        {
            var list = new List<Media>();
            var movie = new Movie {Title = "Die hard", Type = MediasEnum.Movie, Director = "Al Pacino"};
            list.Add(movie);
            var music = new Music {Title = "Ghetto gospel", Artist = "The piano man", Type = MediasEnum.Music};
            list.Add(music);
            var picture = new Picture {Title = "The Scream", Type = MediasEnum.Picture, Author = "Göbels"};
            list.Add(picture);

            string json = JsonConvert.SerializeObject(new Report<IList<Media>> {Data = list});

            var deser = JsonConvert.DeserializeObject<Report<IList<Media>>>(json);

            Console.WriteLine(deser.Data.Count + " " + deser.Data[0].Title + " " + deser.Data[1].Title + " " + deser.Data[2].Title + " ");

            return Message(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParam"></param>
        /// <returns>Report of IList of Media</returns>
        public Stream SearchMedia(string searchParam)
        {
            try
            {
                //first lets take out textParam and typeParam
                var splitted = Regex.Split(searchParam, "==");
                //determine if text only or both text and type
                bool textOnly = splitted.Length < 2;
                int type;

                //text search case (Calls database)
                if (textOnly && splitted.Length>0)
                    return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<IList<Media>> { Data = MediaDB.SearhForMedia(splitted[0]) })));
                //type search case (Calls database)
                if (int.TryParse(splitted[1], out type) && string.IsNullOrEmpty(splitted[0]))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<IList<Media>> { Data = MediaDB.SearhForMedia((MediasEnum)type) })));
                }
                //text and type search (Calls database)
                if (int.TryParse(splitted[1], out type))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<IList<Media>> { Data = MediaDB.SearhForMedia(splitted[0],(MediasEnum)type) })));
                }

                throw new FileNotFoundException();
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1,e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<IList<Media>>{Error = 1})));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamdata"></param>
        /// <returns>Report of Potato</returns>
        public Stream CreateAccount(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get user from JSON string

                var recUser = JObject.Parse(jsonString).ToObject<User>();

                UserDB.AddUser(recUser);
                //todo: this is not that smart
                var potato = UserDB.LoginUser(recUser.Email, recUser.Password);

                //TODO: ACTUALLY GET POTATO FROM SERVER
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Potato> { Data = potato })));
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Potato> { Error = 1 })));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamdata"></param>
        /// <returns>Report of User</returns>
        public Stream EditAccount(Stream streamdata)
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
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<User>{Data = recUser})));
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<User> {Error = 1})));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamdata"></param>
        /// <returns>Report of User</returns>
        public Stream GetAccount(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get potato from JSON string

                var recPotato = JObject.Parse(jsonString).ToObject<Potato>();

                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<User> { Data = UserDB.GetUserByPotato(recPotato)})));
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<User> {Error = 1})));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="streamdata"></param>
        /// <returns>Report of Potato</returns>
        public Stream AccountLogin(string email, Stream streamdata)
        {
            try
            {
                LogAction("Login", email);
                var password = StringFromStreamDebug(streamdata);
                
                //TODO: PASSWORD SHOULD BE ENCRYPTED MAYBE

                //TODO: Actually check email & password with database

                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Potato> {Data = new Potato {EncPassword = "ThisIsMuchEncrypted", Id = 1}})));
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Potato> { Error = 1 })));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamdata"></param>
        /// <returns>Report of int</returns>
        public Stream DeleteAccount(Stream streamdata)
        {
            //TODO: Not done at all
            StringFromStreamDebug(streamdata);
            return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<int>{Data = 1})));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Report of IList of Media</returns>
        public Stream GetAllMedia()
        {
            try
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<IList<Media>> {Data = MediaDB.GetAllMedia()})));
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<IList<Media>> { Error = 1 })));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Report of Media</returns>
        public Stream GetMediaForId(string id)
        {
            try
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Media> {Data = MediaDB.GetMediaForId(int.Parse(id))})));
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Media> { Error = 1 })));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Report of Statistic</returns>
        public Stream GetStatsForId(string id)
        {
            //TODO: DUMMY
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

                    return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(toReturn)));
                }
                throw new ArgumentException();
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Statistic> { Error = 1 })));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="streamdata"></param>
        /// <returns>Report of Media</returns>
        public Stream RentMediaById(string mediaId, Stream streamdata)
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
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Media> { Error = (int)ErrorCodes.GeneralError })));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamdata"></param>
        /// <returns>Report of Media</returns>
        public Stream CreateNewMedia(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get potato, media and byte[] from JSON string

                var jArray = JArray.Parse(jsonString);
                var recPotato = jArray[0].ToObject<Potato>();

                var recMedia = Media.GetMediaUseType(jArray[1]);
                
                var recByteAr = jArray[2].ToObject<byte[]>();

                LogAction("New media [" + recMedia.Title + "]", "Potato ID#" + recPotato.Id);

                MediaDB.AddMedia(recMedia);

                //TODO: Return media with new ID AND SAVE FILE

                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Media> {Data = recMedia})));

            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Media> { Error = (int)ErrorCodes.GeneralError })));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamdata"></param>
        /// <returns>Report of Media</returns>
        public Stream EditMedia(Stream streamdata)
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
                        // ReSharper disable once SpecifyACultureInStringConversionExplicitly
                        Console.WriteLine(byt.ToString());
                    }
                    Console.WriteLine("// End of byte[]");
                }

                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Media> {Data = recMedia})));

                //TODO: Actually put this in the database
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Media> { Error = (int)ErrorCodes.GeneralError })));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="streamdata"></param>
        /// <returns>Report of Media</returns>
        public Stream DeleteMedia(string id, Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStreamDebug(streamdata);
                //Get potato from JSON string

                var recPotato = JObject.Parse(jsonString).ToObject<Potato>();

                //TODO: Check if allowed to delete

                LogAction("Delete", "Potato ID#" + recPotato.Id);

                var deleted = MediaDB.GetMediaForId(int.Parse(id));

                MediaDB.DeleteMedia(int.Parse(id));

                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Media> {Data = deleted})));
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT REAL ERROR CODE
                LogError(1, e.ToString());
                return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Report<Media> { Error = (int)ErrorCodes.GeneralError })));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamdata"></param>
        /// <returns>Report of IList of Reminder</returns>
        public Stream CheckReminders(Stream streamdata)
        {
            var received = StringFromStreamDebug(streamdata);

            var toReturn = new Report<IList<Reminder>> { Data = new List<Reminder>() };
            toReturn.Data.Add(new Reminder{_DateReceived = DateTime.Now, _DateSent = DateTime.MinValue,Id = 1,MediaId = 1,Message = "This is a reminder about blabla...", Title = "Reminder Much Title", UserId = 1});

            return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(toReturn)));
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
                    //var currentBac = Console.BackgroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Stream = |" + s + "|");
                    Console.ForegroundColor = currentFor;
                    return s;
                }

        private void LogAction(string whatRequest, string forWho)
        {
            var currentFor = Console.ForegroundColor;
            //var currentBac = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(whatRequest + " requested for: " + forWho);
            Console.ForegroundColor = currentFor;
        }

        private void LogError(int error, string message)
        {
            var currentFor = Console.ForegroundColor;
            //var currentBac = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR#" + error + " " + message);
            Console.ForegroundColor = currentFor;
        }
    }
}
