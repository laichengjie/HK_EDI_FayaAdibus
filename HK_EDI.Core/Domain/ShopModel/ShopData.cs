using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.ShopModel
{
    public class ShopData : BaseEntity
    {
        public string CompanyID { get; set; }
        public string ShopID { get; set; }
        public string ShopCode { get; set; }
        public string ShopName { get; set; }
        public int ShopType { get; set; }
    }
}
