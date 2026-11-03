using HK_EDI.Core.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Data.Mapping.Tasks
{
    public class TaskRunTimeMap : NopEntityTypeConfiguration<TaskRunTime>
    {
        public TaskRunTimeMap()
        {
            base.ToTable("HK_Adibus_TaskRunTime");
            base.HasKey(m => m.Key);
            base.Property(m => m.Key).HasMaxLength(300);
            base.Ignore(n => n.Id);
        }
    }
}
