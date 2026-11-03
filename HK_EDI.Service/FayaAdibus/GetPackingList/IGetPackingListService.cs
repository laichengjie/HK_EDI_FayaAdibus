using HK_EDI.Core.Domain.EDI_Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.GetPackingList
{
    public interface IGetPackingListService
    {
        void GetPackingListMethod(SD_Adibus_Interface config, string BillNo);
    }
}       
