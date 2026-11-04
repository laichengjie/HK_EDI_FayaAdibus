using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Service.FayaAdibus.CreateReplenishmentOrder;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HK_EDI.Web.Tasks
{
    public class CreateReplenishmentOrderTask : ITask
    {
        private readonly ILogger _logger;
        private readonly ICreateReplenishmentOrderService _iCreateReplenishmentOrderService;


        public CreateReplenishmentOrderTask(ICreateReplenishmentOrderService iCreateReplenishmentOrderService, ILogger iLogger)
        {
            this._logger = iLogger;
            this._iCreateReplenishmentOrderService = iCreateReplenishmentOrderService;
        }
        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute(SD_Adibus_Interface config)
        {
            _iCreateReplenishmentOrderService.CreateReplenishmentOrderMethod(config, "");
        }

    }
}
