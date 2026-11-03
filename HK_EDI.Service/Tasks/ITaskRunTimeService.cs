using HK_EDI.Core.Domain.Tasks;
using HK_EDI.Core.PagedExend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Tasks
{
    public interface ITaskRunTimeService
    {
        TaskRunTime GetByKey(string key);

        void Add(TaskRunTime m);

        void Delete(string key);

        void Update(TaskRunTime m);

        IPagedList<TaskRunTime> SearchTaskRunTime(
            string key,
            int pageIndex = 0,
            int pageSize = int.MaxValue
            );

        void RecordTaskTime(string syncKey, TaskRunTime taskTime, DateTime endtime);

        IList<TaskRunTime> GetAll();
    }
}
