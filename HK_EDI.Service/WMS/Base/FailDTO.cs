using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS
{
    public class FailDTO
    {

        public string SKUCode { get; set; }

        public string CreateTime { get { return DateTime.Now.ToString(); } }

        public int InterFaceType { get { return 1; } }

        public int IsSuccess { get { return 0; } }
        public int Qty { get { return 0; } }
        public String OSInterfaceID { get; set; }
    }
}
