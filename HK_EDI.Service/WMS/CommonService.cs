using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS
{
   public  class CommonService
    {
       public static string GetInvoiceString(string title, string[] strContent)
       {
           if (strContent.Count() <= 0) return "";
           for (int i = 0; i < strContent.Count(); i++)
           {
               if (strContent[i].Contains(title))
               {
                   string[] str = strContent[i].Split(':');
                   if (str.Count() < 2)
                   {
                       return "";
                   }
                   else
                   {
                       return str[1];
                   }
               }
           }
           return "";
       }
    }
}
