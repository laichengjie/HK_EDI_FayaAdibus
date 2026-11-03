using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.Tasks
{
    public class TaskRunTime : BaseEntity
    {

        public string Key { get; set; }

        public DateTime LastTime { get; set; }
    }
}
