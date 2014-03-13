using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISeeN_DB
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Bio { get; set; }
        public bool IsAdmin { get; set; }
    }
}