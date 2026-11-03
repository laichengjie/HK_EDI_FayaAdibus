using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Service.WMS.ChunFeng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.ChunFeng
{
    public interface IChunFengWebApi
    {
       void Confirm(string content, ref Response Rs);

        
    }
}
