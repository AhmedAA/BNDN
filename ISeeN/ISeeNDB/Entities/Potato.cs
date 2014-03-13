using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ISeeN.Entities
{
    public class Potato : DbContext
    {
        public int Id;
        //Password encrypted using serverside algorithm
        public string EncPassword;
    }
}