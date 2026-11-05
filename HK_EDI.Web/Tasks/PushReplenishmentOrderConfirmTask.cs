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
    public class PushReplenishmentOrderConfirmTask : ITask
    {
        private readonly ILogger _logger;
        private readonly IPushReplenishmentOrderConfirmService _iPushReplenishmentOrderConfirm;


        public PushReplenishmentOrderConfirmTask(IPushReplenishmentOrderConfirmService iPushReplenishmentOrderConfirmService, ILogger iLogger)
        {
            this._logger = iLogger;
            this._iPushReplenishmentOrderConfirm = iPushReplenishmentOrderConfirmService;
        }
        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute(SD_Adibus_Interface config)
        {
            _iPushReplenishmentOrderConfirm.PushReplenishmentOrderConfirmMethod(config, "");
        }

    }
}
