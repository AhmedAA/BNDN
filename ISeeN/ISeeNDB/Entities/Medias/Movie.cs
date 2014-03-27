using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ISeeN_DB
{
    [NotMapped]
    public class Movie : Media
    {
        
        public string Director { get; set; }

        //public virtual Movie movie { get; set; }

        //[ForeignKey("Id"), Column(Order = 0)]
        //public int Id { get; set; }

    }
}
