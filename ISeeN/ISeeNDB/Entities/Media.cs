using System;
using System.Data.Entity.Core;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace ISeeN_DB
{
    [DataContract]
    public class Media
    {
        //Id of media
        [DataMember]
        public int Id { get; set; }

        //Title of media
        [DataMember]
        public string Title { get; set; }
        //Type of media (int = type, 0=movie...)
        [DataMember]
        public MediasEnum Type { get; set; }
        //Release time of media
        [DataMember]
        public string ReleaseDate
        {
            get { return _ReleaseDate.ToString(); }
            //private set, not used atm.
            set { _ReleaseDate = DateTime.Parse(value);  }
        }

        public DateTime _ReleaseDate = DateTime.MinValue;
        private int _id;

        //Description of media
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// This method will be implemented in implementers, and will handle the action to do when media is activated.
        /// </summary>
        public void Do()
        {
            throw new NotImplementedException();
        }

        public static Media GetMediaUseType(JToken jToken)
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
    }
}
