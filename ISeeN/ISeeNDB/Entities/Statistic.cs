using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ISeeN_DB
{
    [DataContract]
    public class Statistic
    {
        [DataMember]
        [Key]
        public int MediaId { get; set; }

        [DataMember]
        public IList<string> DatesRented
        {
            get { return _DatesRented.Select(date => date.ToString()).ToList(); }
            //private set, not used atm.
            set 
            {
                _DatesRented = new List<DateTime>();
                foreach (var date in value)
                {
                    _DatesRented.Add(DateTime.Parse(date));
                }
            }
        }
        [DataMember]
        public IList<DateTime> _DatesRented { get; set;}
    }
}