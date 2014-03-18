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
        public DbSet<ISeeN_DB.Potato> Potatoes { get; set; }
        public DbSet<ISeeN_DB.Reminder> Reminders { get; set;}
        public DbSet<ISeeN_DB.Media> Medias { get; set; }
        public DbSet<ISeeN_DB.Statistic> Statistics { get; set; }
        public DbSet<ISeeN_DB.Movie> Movies { get; set; }
        public DbSet<ISeeN_DB.Music> Music { get; set; }
        public DbSet<ISeeN_DB.Picture> Pictures { get; set; }
    }
}
