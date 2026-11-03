using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Service.FayaAdibus.GetEstimatedShipment;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HK_EDI.Web.Tasks
{
    public class GetEstimatedShipmentTask : ITask
    {
        private readonly ILogger _logger;
        private readonly IGetEstimatedShipmentService _iGetEstimatedShipmentService;


        public GetEstimatedShipmentTask(IGetEstimatedShipmentService iGetEstimatedShipmentService, ILogger iLogger)
        {
            this._logger = iLogger;
            this._iGetEstimatedShipmentService = iGetEstimatedShipmentService;
        }
        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute(SD_Adibus_Interface config)
        {
            _iGetEstimatedShipmentService.GetEstimatedShipmentMethod(config, "");
        }

    }
}