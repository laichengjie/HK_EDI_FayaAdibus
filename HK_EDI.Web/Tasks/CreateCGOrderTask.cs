using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Service.FayaAdibus.CreateCGOrder;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HK_EDI.Web.Tasks
{
    public class CreateCGOrderTask : ITask
    {
        private readonly ILogger _logger;
        private readonly ICreateCGOrderService _iCreateCGOrderTService;


        public CreateCGOrderTask(ICreateCGOrderService iCreateCGOrderTService, ILogger iLogger)
        {
            this._logger = iLogger;
            this._iCreateCGOrderTService = iCreateCGOrderTService;
        }
        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute(SD_Adibus_Interface config)
        {
            _iCreateCGOrderTService.CreateCGOrderMethod(config, "");
        }

    }
}