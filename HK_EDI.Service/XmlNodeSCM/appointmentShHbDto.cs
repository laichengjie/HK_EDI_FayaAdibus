using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.XmlNodeSCM
{
    public class appointmentShHbDto
    {
        public string shNo { get; set; }
        public string designNo { get; set; }
        public string productCode { get; set; }

        public string hkOrderNo { get; set; }

        public List<shitemVO> shitemVOs { get; set; }
    }
}
