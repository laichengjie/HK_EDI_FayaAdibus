using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Service.FayaAdibus.GetReplenishmentOrder;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HK_EDI.Web.Tasks
{
    public class GetReplenishmentOrderTask : ITask
    {
        private readonly ILogger _logger;
        private readonly IGetReplenishmentOrderService _iGetReplenishmentOrderService;


        public GetReplenishmentOrderTask(IGetReplenishmentOrderService iGetReplenishmentOrderService, ILogger iLogger)
        {
            this._logger = iLogger;
            this._iGetReplenishmentOrderService = iGetReplenishmentOrderService;
        }
        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute(SD_Adibus_Interface config)
        {
            _iGetReplenishmentOrderService.GetReplenishmentOrderMethod(config, "");
        }

    }
}
