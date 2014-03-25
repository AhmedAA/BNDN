using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ISeeN_DB
{
    [DataContract]
    public class Music : Media
    {
        [DataMember]
        public string Artist { get; set; }

        //public virtual Music music { get; set; }

        //[ForeignKey("Id"), Column(Order = 0)]
        //public int Id { get; set; }

    }
}
