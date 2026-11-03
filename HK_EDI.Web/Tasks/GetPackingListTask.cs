using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Service.FayaAdibus.GetPackingList;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HK_EDI.Web.Tasks
{
    public class GetPackingListTask : ITask
    {
        private readonly ILogger _logger;
        private readonly IGetPackingListService _iGetPackingListService;


        public GetPackingListTask(IGetPackingListService iGetPackingListService, ILogger iLogger)
        {
            this._logger = iLogger;
            this._iGetPackingListService = iGetPackingListService;
        }
        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute(SD_Adibus_Interface config)
        {
            _iGetPackingListService.GetPackingListMethod(config, "");
        }

    }
}