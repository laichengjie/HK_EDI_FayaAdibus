using HK_EDI.Core.Domain.EDI_Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.EDI_Config
{
    public partial interface IEDI_ConfigService
    {
        SD_Adibus_Interface GetConfig(string CooperationSys);

        SD_Adibus_Interface GetConfig(string companyID, string OSInterfaceID);

        IList<SD_Adibus_InterfaceDetail> GetConfigDetail(string companyID, string OSInterfaceID);

        IList<SD_Adibus_Interface> GetAll();

        void Delete(SD_Adibus_Interface config);

        void Insert(SD_Adibus_Interface config);

        void Update(SD_Adibus_Interface config);
    }
}
