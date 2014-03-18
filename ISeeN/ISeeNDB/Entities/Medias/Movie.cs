using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ISeeN_DB
{
    [DataContract]
    class Movie
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Director { get; set; }

    }
}
