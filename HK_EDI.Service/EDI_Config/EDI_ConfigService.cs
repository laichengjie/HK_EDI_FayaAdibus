using HK_EDI.Core.Data;
using HK_EDI.Core.Domain.EDI_Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HK_EDI.Core;

namespace HK_EDI.Service.EDI_Config
{
    public class EDI_ConfigService : IEDI_ConfigService
    {
        #region Fields

        private readonly IRepository<SD_Adibus_Interface> _masterConfig;
        private readonly IRepository<SD_Adibus_InterfaceDetail> _detailConfig;

        #endregion

        #region Ctor

        public EDI_ConfigService(IRepository<SD_Adibus_Interface> masterConfig, IRepository<SD_Adibus_InterfaceDetail> detailConfig)
        {
            this._masterConfig = masterConfig;
            this._detailConfig = detailConfig;
        }

        #endregion

        public Core.Domain.EDI_Config.SD_Adibus_Interface GetConfig(string CooperationSys)
        {
            if (CooperationSys.IsNullOrEmpty())
                return null;

            var config = _masterConfig.Table.FirstOrDefault(m => m.CooperationSys == CooperationSys );
            if (config != null)
            {
                var detail = _detailConfig.Table.Where(m => m.CompanyID == config.CompanyID && m.OSInterfaceID == config.OSInterfaceID).ToArray();
                config.SD_Adibus_InterfaceDetail = detail;
            }
            return config;
        }
        public Core.Domain.EDI_Config.SD_Adibus_Interface GetConfig(string companyID, string OSInterfaceID)
        {
            if (OSInterfaceID.IsNullOrEmpty())
                return null;

            var config = _masterConfig.Table.FirstOrDefault(m=> m.CompanyID == companyID && m.OSInterfaceID == OSInterfaceID);
            if (config != null)
            {
                var detail = _detailConfig.Table.Where(m => m.CompanyID == companyID && m.OSInterfaceID == OSInterfaceID).ToArray();
                config.SD_Adibus_InterfaceDetail = detail;
            }
            return config;
        }

        public IList<SD_Adibus_InterfaceDetail> GetConfigDetail(string companyID, string OSInterfaceID)
        {
            return _detailConfig.Table.Where(m => m.CompanyID == companyID && m.OSInterfaceID == OSInterfaceID).ToList();
        }
        public IList<Core.Domain.EDI_Config.SD_Adibus_Interface> GetAll()
        {
            return _masterConfig.Table.ToList();
        }

        public void Delete(Core.Domain.EDI_Config.SD_Adibus_Interface config)
        {
            foreach (var item in config.SD_Adibus_InterfaceDetail)
            {
                _detailConfig.Delete(item);
            }
            _masterConfig.Delete(config);
        }

        public void Insert(Core.Domain.EDI_Config.SD_Adibus_Interface config)
        {
            foreach (var item in config.SD_Adibus_InterfaceDetail)
            {
                _detailConfig.Insert(item);
            }
            _masterConfig.Insert(config);
        }

        public void Update(Core.Domain.EDI_Config.SD_Adibus_Interface config)
        {
            foreach (var item in config.SD_Adibus_InterfaceDetail)
            {
                _detailConfig.Update(item);
            }
            _masterConfig.Update(config);
        }


        
    }
}
