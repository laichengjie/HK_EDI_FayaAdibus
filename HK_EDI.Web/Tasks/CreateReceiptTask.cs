using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Service.FayaAdibus.CreateReceipt;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HK_EDI.Web.Tasks
{
    public class CreateReceiptTask : ITask
    {
        private readonly ILogger _logger;
        private readonly ICreateReceiptService _iCreateReceiptService;


        public CreateReceiptTask(ICreateReceiptService iCreateReceiptService, ILogger iLogger)
        {
            this._logger = iLogger;
            this._iCreateReceiptService = iCreateReceiptService;
        }
        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute(SD_Adibus_Interface config)
        {
            _iCreateReceiptService.CreateReceiptMethod(config,"");
        }

    }
}