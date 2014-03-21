using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ISeeN_DB
{
    [DataContract]
    public class Movie : Media
    {
        [DataMember]
        public string Director { get; set; }

    }
}
