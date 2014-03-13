using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISeeN_DB
{
    public class Reminder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime DateReceived { get; set; }
    }
}