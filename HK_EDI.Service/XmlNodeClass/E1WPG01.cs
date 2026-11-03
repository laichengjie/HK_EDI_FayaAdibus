using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.XmlNodeClass
{
   public  class E1WPG01
    {
        public string BELEGDATUM= string.Empty;
        public string BONNUMMER= string.Empty;
        public string KASSIERER= string.Empty;
        public string AUTORISIER= string.Empty;
        public ZE1WPG01 ZE1WPG01 { get; set; }
        public List<E1WPG02> E1WPG02 { get; set; }
    }
}
