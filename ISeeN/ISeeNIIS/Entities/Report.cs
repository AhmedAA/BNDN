using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ISeeNIIS.Entities
{
    [DataContract]
    public class Report<TData>
    {
        [DataMember]
        public TData Data { get; set; }
        //int corresponds to specific error code.
        [DataMember]
        public int Error { get; set; }
    }
}