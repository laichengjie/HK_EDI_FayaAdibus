using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Service.WMS.SKU360.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.SKU360
{
    public interface ISKU360WebApi
    {
       void Confirm(string content, ref HK_EDI.Service.WMS.SKU360.Models.Response Rs);

    }
}
