using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISeeN.Entities;

namespace ISeeN.TestRelated_Entities
{
    public class TMedia : IMedia
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public void Do()
        {
            throw new NotImplementedException();
        }
    }
}