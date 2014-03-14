using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ISeeN_DB
{
    [DataContract]
    public class Statistic
    {
        [DataMember]
        public int MediaId { get; set; }
        [DataMember]
        public IList<DateTime> DatesRented { get; set; }  
    }
}