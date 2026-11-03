using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Common
{
    [System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumResourceAttribute : System.Attribute
    {

        public EnumResourceAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; set; }


    }
}
