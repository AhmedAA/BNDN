using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ISeeN_DB
{
    [DataContract]
    public class Picture : Media
    {
        [DataMember]
        public string Author { get; set; }
    }
}
