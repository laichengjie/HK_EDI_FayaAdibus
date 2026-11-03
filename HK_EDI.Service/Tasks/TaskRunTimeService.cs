using HK_EDI.Core.Data;
using HK_EDI.Core.Domain.Tasks;
using HK_EDI.Core.PagedExend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Tasks
{
    public class TaskRunTimeService : ITaskRunTimeService
    {
        private readonly IRepository<TaskRunTime> _taskRunTimeRepository;

        public TaskRunTimeService(
            IRepository<TaskRunTime> taskRunTimeRepository
            )
        {
            this._taskRunTimeRepository = taskRunTimeRepository;
        }

        public TaskRunTime GetByKey(string key)
        {
            return this._taskRunTimeRepository.Table.FirstOrDefault(t => t.Key == key);
        }

        public IList<TaskRunTime> GetAll()
        {
            return _taskRunTimeRepository.Table.ToList();
        }

        public void Add(TaskRunTime m)
        {
            this._taskRunTimeRepository.Insert(m);
        }

        public void Delete(string key)
        {
            var m = this.GetByKey(key);
            if (m != null)
                this._taskRunTimeRepository.Delete(m);
        }

        public void Update(TaskRunTime m)
        {
            this._taskRunTimeRepository.Update(m);
        }

        public IPagedList<TaskRunTime> SearchTaskRunTime(
             string key,
             int pageIndex = 0,
             int pageSize = int.MaxValue
             )
        {
            int index = 1;
            var query = (from p in _taskRunTimeRepository.Table
                         orderby p.LastTime descending
                         select p);

            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(key.Trim()))
            {
                query = from p in _taskRunTimeRepository.Table
                        where p.Key == key.Trim()
                        orderby p.LastTime descending
                        select p;

            }

            foreach (var item in query)
            {
                item.Id = index++;
            }


            var ERPTaskRunTime = new PagedList<TaskRunTime>(query, pageIndex, pageSize);

            return ERPTaskRunTime;
        }

        public void RecordTaskTime(string syncKey, TaskRunTime taskTime, DateTime endtime)
        {
            if (taskTime != null)
            {
                taskTime.LastTime = endtime;//不能取dateNow，因为在同步过程中有可能变更，会漏掉
                this.Update(taskTime);
            }
            else
            {
                taskTime = new TaskRunTime();
                taskTime.Key = syncKey;
                taskTime.LastTime = endtime;
                this.Add(taskTime);
            }
        }
    }
}
