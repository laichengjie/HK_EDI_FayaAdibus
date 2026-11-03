using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.XmlNodeClass
{
   public  class E1WPG02
    {
        public string QUALVORG = string.Empty;
        //public string REMARK1 = string.Empty;
        public string REFERENZNR = string.Empty;
        public string LITEM = string.Empty;
        public string QUALARTNR = string.Empty;
        public string ARTNR = string.Empty;
        public string BEWART = string.Empty;
        public string MEINH = string.Empty;
        public string MENGE = string.Empty;
        public string LIEFMENGE = string.Empty;
        public string WAERS = string.Empty;
        public string WERT = string.Empty;
        public string EXBWR = string.Empty;
        public string ZFILIALE = string.Empty;
        public string KUNDNR = string.Empty;
        public string VERKAEUFER = string.Empty;
        public string LGORT = string.Empty;
        public string CHARG = string.Empty;
        public string LIFNR = string.Empty;
        public string SAKNR = string.Empty;
        public string KOSTL = string.Empty;
        public string SOBKZ = string.Empty;
        public string ELIKZ = string.Empty;
        public string KZBEW = string.Empty;
        public string GRUND = string.Empty;
        public string VFDAT = string.Empty;
        public string CHECK = string.Empty;
        public string UMLGO = string.Empty;
        public string ARTNR_LONG = string.Empty;
        public E1WXX01 E1WXX01 { get; set; }

        public List<E1WXX01> E1WXX01S { get; set; }

    }
}
