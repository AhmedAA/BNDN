using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ISeeN_DB
{
    [DataContract]
    public class Reminder
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int MediaId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string DateSent
        {
            get { return _DateSent.ToString(); }
            //private set, not used atm.
            private set { _DateSent = DateTime.Parse(value); }
        }

        public DateTime _DateSent = DateTime.MinValue;

        [DataMember]
        public string DateReceived
        {
            get { return _DateReceived.ToString(); }
            //private set, not used atm.
            private set { _DateReceived = DateTime.Parse(value); }
        }

        public DateTime _DateReceived = DateTime.MinValue;
    }
}