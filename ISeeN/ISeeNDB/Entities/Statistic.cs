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
        public IList<string> DatesRented
        {
            get { return _DatesRented.Select(date => date.ToString()).ToList(); }
            set 
            {
                _DatesRented = new List<DateTime>();
                foreach (var date in value)
                {
                    _DatesRented.Add(DateTime.Parse(date));
                }
            }
        }

        public IList<DateTime> _DatesRented = new List<DateTime>();
    }
}