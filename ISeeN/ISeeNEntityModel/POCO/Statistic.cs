using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISeeNEntityModel.POCO
{
    public class Statistic
    {
        public int MediaId { get; set; }
        public int ThisAmountOfRents { get; set; }
        public int TotalAmountOfRents { get; set; }
        public IList<String> CountriesRent { get; set; } 
    }
}
