using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ISeeN_DB
{
    class ISeeNDbContext : DbContext
    {
        public DbSet<ISeeN_DB.User> Users { get; set; }
    }
}
