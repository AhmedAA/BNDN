﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ISeeN_DB
{
    [DataContract]
    public class Potato
    {
        [DataMember]
        public int Id { get; set; }
        //Password encrypted using serverside algorithm
        [DataMember]
        public string EncPassword { get; set; }
    }
}