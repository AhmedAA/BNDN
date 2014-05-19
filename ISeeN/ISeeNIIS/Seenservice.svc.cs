using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using ISeeNEntityModel;
using ISeeNEntityModel.Funcs;
using ISeeNEntityModel.POCO;
using ISeeNIIS.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ISeeNIIS
{
    public class Seenservice : ISeenservice
    {
        /// <summary>
        /// This enum will hold all the error codes
        /// </summary>
        public enum ErrorCodes
        {
            NoError = 0,
            GeneralError = 1,

        }

        /// <summary>
        /// This method returns the correct headers for CORS (Preflight check)
        /// </summary>
        /// <param name="end"></param>
        /// <returns>Message response with headers</returns>
        public Stream CORSOptions(string end)
        {
            return Options("GET, PUT, POST, DELETE, OPTIONS");
        }

        /// <summary>
        /// Returns all medias matching tha search param
        /// </summary>
        /// <param name="searchParam">Search parameters NOTE: Format searchParam like:
        ///TextParam:string==TypeParam:int
        ///Example:
        ///GoodFellas==1
        ///GoodFellas
        ///==1
        ///</param>
        /// <returns>Report of IList of Media</returns>
        public Stream SearchMedia(string searchParam)
        {
            try
            {
                SearchedFor(searchParam);
                //first lets take out textParam and typeParam
                var splitted = Regex.Split(searchParam, "==");
                //determine if text only or both text and type
                bool textOnly = splitted.Length < 2;
                int type;

                //text search case (Calls database)
                if (textOnly && splitted.Length > 0)
                    return Message(JsonConvert.SerializeObject(new Report<IList<Media>> { Data = MediaDB.SearchText(splitted[0]) }));
                //type search case (Calls database)
                if (int.TryParse(splitted[1], out type) && string.IsNullOrEmpty(splitted[0]))
                {
                    return Message(JsonConvert.SerializeObject(new Report<IList<Media>> { Data = MediaDB.SearchType(type) }));
                }
                //text and type search (Calls database)
                if (int.TryParse(splitted[1], out type))
                {
                    return Message(JsonConvert.SerializeObject(new Report<IList<Media>> { Data = MediaDB.SearchBoth(splitted[0],type) }));
                }

                throw new FileNotFoundException();
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<IList<Media>> { Error = 1 }));
            }
        }

        /// <summary>
        /// Returns a list of recent searches. Does not use the report structure for speed
        /// </summary>
        /// <returns>recent searches</returns>
        public Stream RecentSearchParams()
        {
            //initialize queue
            var path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "recent.isearched";
            var stringQueue = File.ReadAllText(path);
            return Message(stringQueue);
        }

        private void SearchedFor(string s)
        {
            //initialize queue
            var path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath+"recent.isearched";
            var searches = new Queue<string>();
            if (File.Exists(path))
            {
                var stringQueue = File.ReadAllText(path);
                var temp = JArray.Parse(stringQueue).ToObject<string[]>();
                foreach (var st in temp)
                {
                    searches.Enqueue(st);
                }
            }

            //check size of queue
            if (searches.Count > 9)
            {
                searches.Dequeue();
                searches.Enqueue(s);
            }
            else
            {
                searches.Enqueue(s);
            }

            //save queue
            File.WriteAllText(path,JsonConvert.SerializeObject(searches));
        }

        /// <summary>
        /// Creates an accout from the input
        /// </summary>
        /// <param name="streamdata">A User to be created</param>
        /// <returns>Report of Potato</returns>
        public Stream CreateAccount(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStream(streamdata);
                //Get user from JSON string

                var recUser = JObject.Parse(jsonString).ToObject<User>();

                var potato = UserDB.AddUser(recUser);

                return Message(JsonConvert.SerializeObject(new Report<Potato> { Data = potato }));
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<Potato> { Error = 1 }));
            }
        }

        /// <summary>
        /// Edits the account
        /// </summary>
        /// <param name="streamdata">A User to be edited and a valid potato</param>
        /// <returns>Report of User</returns>
        public Stream EditAccount(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStream(streamdata);
                //Get potato and user from JSON string

                var jArray = JArray.Parse(jsonString);
                var recPotato = jArray[0].ToObject<Potato>();
                var recUser = jArray[1].ToObject<User>();

                return Message(JsonConvert.SerializeObject(new Report<User> { Data = UserDB.EditAccount(recPotato,recUser) }));
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<User> { Error = 1 }));
            }
        }

        /// <summary>
        /// Get account corresponding to potato send
        /// </summary>
        /// <param name="streamdata">Potato to get account for</param>
        /// <returns>Report of User</returns>
        public Stream GetAccount(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStream(streamdata);
                //Get potato from JSON string

                var recPotato = JObject.Parse(jsonString).ToObject<Potato>();

                return Message(JsonConvert.SerializeObject(new Report<User> { Data = UserDB.GetUserByPotato(recPotato) }));
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<User> { Error = 1 }));
            }
        }

        /// <summary>
        /// Returns a valid potato for user
        /// </summary>
        /// <param name="streamdata">Email and password</param>
        /// <returns>Report of Potato</returns>
        public Stream AccountLogin(Stream streamdata)
        {
            try
            {
                var input = JsonConvert.DeserializeObject<string[]>(StringFromStream(streamdata));
                var email = input[0];
                var password = input[1];

                return Message(JsonConvert.SerializeObject(new Report<Potato> { Data = UserDB.LoginUser(email,password) }));
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<Potato> { Error = 1 }));
            }

        }

        /// <summary>
        /// Deletes account corresponding to potato
        /// </summary>
        /// <param name="streamdata">Potato to delete account for</param>
        /// <returns>Report of int</returns>
        public Stream DeleteAccount(Stream streamdata)
        {
            var jsonString = StringFromStream(streamdata);
            //Get potato and user from JSON string

            var recPotato = JsonConvert.DeserializeObject<Potato>(jsonString);

            return Message(JsonConvert.SerializeObject(new Report<int> { Data = UserDB.DeleteUser(recPotato) }));
        }

        /// <summary>
        /// Get all media in the database
        /// </summary>
        /// <returns>Report of IList of Media</returns>
        public Stream GetAllMedia()
        {
            try
            {
                return Message(JsonConvert.SerializeObject(new Report<IList<Media>> { Data = MediaDB.GetAll() }));
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<IList<Media>> { Error = 1 }));
            }
        }

        /// <summary>
        /// Get a specific media by id
        /// </summary>
        /// <param name="id">Id of media</param>
        /// <returns>Report of Media</returns>
        public Stream GetMediaForId(string id)
        {
            try
            {
                return Message(JsonConvert.SerializeObject(new Report<Media> { Data = MediaDB.GetMediaForId(int.Parse(id)) }));
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<Media> { Error = 1 }));
            }
        }

        /// <summary>
        /// Get statistics for a media
        /// </summary>
        /// <param name="id">Id of media</param>
        /// <returns>Report of Statistic</returns>
        public Stream GetStatsForId(string id)
        {
            try
            {
                return Message(JsonConvert.SerializeObject(new Report<Statistic> { Data = MediaDB.GetStatsForId(int.Parse(id)) }));
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<Statistic> { Error = 1 }));
            }
        }

        /// <summary>
        /// Rents media for a user
        /// </summary>
        /// <param name="streamdata">Media id and potato of user</param>
        /// <returns>Report of Media</returns>
        public Stream RentMediaById(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStream(streamdata);
                //Get potato from JSON string
                var input = JArray.Parse(jsonString);

                var id = int.Parse(input[0].ToString());
                var recPotato = JObject.Parse(input[1].ToString()).ToObject<Potato>();

                return Message(JsonConvert.SerializeObject(new Report<Media> { Data = MediaDB.RentMedia(id, recPotato) }));

            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<Media> { Error = (int)ErrorCodes.GeneralError }));
            }
        }

        /// <summary>
        /// Creates the new media
        /// </summary>
        /// <param name="streamdata">Potato, media and base64string to create</param>
        /// <returns>Report of Media</returns>
        public Stream CreateNewMedia(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStream(streamdata);
                //Get potato, media and byte[] from JSON string

                var jArray = JArray.Parse(jsonString);
                var recPotato = jArray[0].ToObject<Potato>();

                var recMedia = GetMediaUseType(jArray[1]);

                var base64String = jArray[2].Value<string>();

                var newMedia = MediaDB.AddMedia(recMedia);

                //try adding the file to the file system. If it fails delete the media in the database
                if (!FileManager.AddEditMedia(newMedia.Id, base64String))
                {
                    MediaDB.DeleteMedia(newMedia.Id,recPotato);
                    throw new FileLoadException("Filemanager failed to manage file correctly");
                }
                    

                return Message(JsonConvert.SerializeObject(new Report<Media> { Data = newMedia }));

            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<Media> { Error = (int)ErrorCodes.GeneralError }));
            }
        }

        /// <summary>
        /// Edits the media
        /// </summary>
        /// <param name="streamdata">Either a Potato and a Media or a Potato, a media and a base64string</param>
        /// <returns>Report of Media</returns>
        public Stream EditMedia(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStream(streamdata);
                //Get potato, media and byte[] from JSON string

                var jArray = JArray.Parse(jsonString);

                var recPotato = jArray[0].ToObject<Potato>();
                var recMedia = jArray[1].ToObject<Media>();

                var media = MediaDB.EditMedia(recPotato, recMedia);

                //Is a slow edit (byte array to overwrite)
                if (jArray.Count == 3)
                {
                    var base64String = jArray[2].Value<string>();
                    if(!FileManager.AddEditMedia(media.Id, base64String))
                        throw new FileLoadException("Filemanager failed to manage file correctly");

                }

                return Message(JsonConvert.SerializeObject(new Report<Media> { Data = media }));

            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<Media> { Error = (int)ErrorCodes.GeneralError }));
            }
        }

        /// <summary>
        /// Deletes media for the id
        /// </summary>
        /// <param name="streamdata">Id of media and potato</param>
        /// <returns>Report of Media</returns>
        public Stream DeleteMedia(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStream(streamdata);
                //Get potato from JSON string
                var input = JArray.Parse(jsonString);

                var id = input[0].ToString();
                var recPotato = JObject.Parse(input[1].ToString()).ToObject<Potato>();

                var media = MediaDB.DeleteMedia(int.Parse(id), recPotato);

                //checks that media deletion in database was actually a succes
                if (media == 1)
                {
                    if (!FileManager.RemoveMedia(int.Parse(id)))
                        LogError(1,"Exception when trying to remove media");
                }
                    

                return Message(JsonConvert.SerializeObject(new Report<int> { Data = media }));
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<int> { Error = (int)ErrorCodes.GeneralError }));
            }
        }

        /// <summary>
        /// Gets the file for a rental
        /// </summary>
        /// <param name="streamdata">Id of media and potato</param>
        /// <returns>Report of base64string</returns>
        public Stream GetFileForRental(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStream(streamdata);
                //Get potato from JSON string
                var input = JArray.Parse(jsonString);

                var id = input[0].ToString();
                var recPotato = JObject.Parse(input[1].ToString()).ToObject<Potato>();

                //check that it is actually rented
                var rented = RentalDB.CheckUserRented(int.Parse(id), recPotato);

                if (rented)
                    return Message(JsonConvert.SerializeObject(new Report<string> { Data = FileManager.GetFile(id) }));

                //file was not rented
                throw new KeyNotFoundException("File was not rented for user");
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<string> { Error = (int)ErrorCodes.GeneralError }));
            }
        }

        /// <summary>
        /// Gets all rentals for a user
        /// </summary>
        /// <param name="streamdata">Potato of user</param>
        /// <returns>Report of IList of Rentals</returns>
        public Stream GetRentalsForUser(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStream(streamdata);
                //Get potato from JSON string

                var recPotato = JObject.Parse(jsonString).ToObject<Potato>();

                var rents = RentalDB.RentalsForUser(recPotato);

                return Message(JsonConvert.SerializeObject(new Report<IList<Rental>> {Data = rents}));
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<IList<Rental>> { Error = (int)ErrorCodes.GeneralError }));
            }
        }

        /// <summary>
        /// Checks wether or not a user rented a media
        /// </summary>
        /// <param name="streamdata">id of media and potato</param>
        /// <returns>Report of bool</returns>
        public Stream CheckUserRented(Stream streamdata)
        {
            try
            {
                var jsonString = StringFromStream(streamdata);
                //Get potato from JSON string
                var input = JArray.Parse(jsonString);

                var id = input[0].ToString();
                var recPotato = JObject.Parse(input[1].ToString()).ToObject<Potato>();

                return Message(JsonConvert.SerializeObject(new Report<bool> {Data = RentalDB.CheckUserRented(int.Parse(id), recPotato)}));
            }
            catch (Exception e)
            {
                LogError(1, e.ToString());
                return Message(JsonConvert.SerializeObject(new Report<bool> { Error = (int)ErrorCodes.GeneralError }));
            }
        }





        private Media GetMediaUseType(JToken jToken)
        {
            var tochck = (int)jToken["Type"];
            var type = (MediasEnum)tochck;

            if (type == MediasEnum.Media)
                return jToken.ToObject<Media>();
            if (type == MediasEnum.Movie)
                return jToken.ToObject<Movie>();
            if (type == MediasEnum.Music)
                return jToken.ToObject<Music>();
            if (type == MediasEnum.Picture)
                return jToken.ToObject<Picture>();

            throw new ObjectNotFoundException("Type was not valid for media");
        }

        private Stream Options(string method)
        {
            // ReSharper disable once PossibleNullReferenceException
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", method.ToUpper());
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Max-Age", "1728000");
            return Message("Preflight-check: Ok");
        }

        private Stream Message(string json)
        {
            //stuff that should be done before returning
            //CORS Headers added
            // ReSharper disable once PossibleNullReferenceException
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            var toReturn = new MemoryStream(Encoding.UTF8.GetBytes(json));
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

        private void LogError(int error, string message)
        {
            try
            {
                // ReSharper disable PossibleNullReferenceException
                WebOperationContext.Current.OutgoingResponse.Headers.Add("ERROR-"+error, message);
            }
            catch (Exception e)
            {
                WebOperationContext.Current.OutgoingResponse.Headers.Add("ERROR-"+error+"-BREAK", e.Message);
                // ReSharper restore PossibleNullReferenceException
            }
        }
    }
}