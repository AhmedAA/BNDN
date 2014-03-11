using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISeeN.Entities
{
    public class Report<TData>
    {
        public TData Data { get; set; }
        //int corresponds to specific error code.
        public int Error { get; set; }
    }
}