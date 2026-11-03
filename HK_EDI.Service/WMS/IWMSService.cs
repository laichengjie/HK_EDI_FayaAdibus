using HK_EDI.Core.Common;
using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Service.WMS.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS
{
    public interface IWMSService
    {
        void PostByOperator(SD_Adibus_Interface config);

        void PostSku(SD_Adibus_Interface config);

        void PostStockQuery(SD_Adibus_Interface config);

        void PostOutOrder(SD_Adibus_Interface config);

        void PostInOrder(SD_Adibus_Interface config);

        string GetOrderStatus(SD_Adibus_Interface config, string singCode);

        CommonReturnModel ForceProcess(SD_Adibus_Interface config, string SignCodes);

        CommonReturnModel CancelOrder(SD_Adibus_Interface config, string SignCodes);

        CommonReturnModel CancelOrderList(SD_Adibus_Interface config, string SignCodes);

        void CompleteOrder(SD_Adibus_Interface config);

        IEnumerable<BaseItem> GetOrderStatusEnums();

        bool IsCancelStatus(int status);

        void PostOrderByBillNo(SD_Adibus_Interface config, string BillNo);
        void PostOrderBySignCode(SD_Adibus_Interface config, string CompanyId,  string SignCode);

        string PostSummaryBillNo(SD_Adibus_Interface config, string CompanyId,string ShopID,string DateTime);
        void DelOrderBySignCode(SD_Adibus_Interface config, string SignCode);

    }
}
