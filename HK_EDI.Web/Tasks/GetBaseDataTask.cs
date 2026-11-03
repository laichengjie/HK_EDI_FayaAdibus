using Autofac;
using HK_EDI.Core;
using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Core.Domain.Tasks;
using HK_EDI.Core.Infrastructure;
using HK_EDI.Data;
using HK_EDI.Service.Tasks;
using HK_EDI.Service.WMS;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using HK_EDI.Core;
using HK_EDI.Web.ExtensionsCommon;

namespace HK_EDI.Web.Tasks
{
    public class GetBaseDataTask : ITask
    {

        public GetBaseDataTask(
            )
        {
        }

        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute(SD_Adibus_Interface config)
        {
            
        }

        

    }

}