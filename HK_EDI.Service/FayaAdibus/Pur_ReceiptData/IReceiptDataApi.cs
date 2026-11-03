using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.Pur_ReceiptData
{
    public interface IReceiptDataApi
    {
        void GetReceiptDataSync(string response,
        ref Response Result);
    }
}       
