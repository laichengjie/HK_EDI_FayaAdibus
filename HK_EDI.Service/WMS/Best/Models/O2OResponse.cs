using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.Best.Models
{
    public class O2OResponse
    {
        public string orderStatus { get; set; }

        public List<error> errors { get; set; }
    }
    public class error
    {
        public string errorCode { get; set; }
        public string errorDescription { get; set; }
    }
}
