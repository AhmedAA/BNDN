using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ISeeN_DB
{
    [DataContract]
    class Music
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Artist { get; set; }
    }
}
