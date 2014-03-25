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
    public class Picture : Media
    {
        private Media _media;

        [DataMember]
        public string Author { get; set; }

        //public virtual Picture picture { get; set; }

        //[ForeignKey("Id"), Column(Order = 0)]
        //public int Id { get; set; }

    }
    
}
