using HK_EDI.Core.Domain.EDI_Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.GetEstimatedShipment
{
    public interface IGetEstimatedShipmentService
    {
        void GetEstimatedShipmentMethod(SD_Adibus_Interface config, string BillNo);
    }
}       
