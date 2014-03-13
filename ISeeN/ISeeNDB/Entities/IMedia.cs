using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISeeN_DB
{
    public interface IMedia
    {
        //Id of media
        int Id { get; set; }
        //Title of media
        string Title { get; set; }
        //Type of media (int = type, 0=movie...)
        int Type { get; set; }
        //Release time of media
        DateTime ReleaseDate { get; set; }
        //Description of media
        string Description { get; set; }

        /// <summary>
        /// This method will be implemented in implementers, and will handle the action to do when media is activated.
        /// </summary>
        void Do();
    }
}
