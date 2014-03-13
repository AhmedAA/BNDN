using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISeeN_DB
{
    public class Statistic
    {
        public int MediaId { get; set; }
        public IList<DateTime> DatesRented { get; set; }  
    }
}