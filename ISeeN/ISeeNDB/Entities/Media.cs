using System;
using System.Runtime.Serialization;

namespace ISeeN_DB.Entities
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
        public int Type { get; set; }
        //Release time of media
        [DataMember]
        public DateTime? ReleaseDate { get; set; }
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
    }
}
