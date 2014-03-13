using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ISeeN.Entities
{
    public class Statistic : DbContext
    {
        public int MediaId { get; set; }
        public IList<DateTime> DatesRented { get; set; }  
    }
}