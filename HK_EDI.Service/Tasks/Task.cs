using System;
using Autofac;
using HK_EDI.Core.Domain.Tasks;
using HK_EDI.Core.Infrastructure;
using HK_EDI.Service.EDI_Config;
using HK_EDI.Services.Logging;
//using HK_EDI.Services.Logging;

namespace HK_EDI.Services.Tasks
{
    /// <summary>
    /// Task
    /// </summary>
    public partial class Task
    {
        /// <summary>
        /// Ctor for Task
        /// </summary>
        private Task()
        {
            this.Enabled = true;
        }

        /// <summary>
        /// Ctor for Task
        /// </summary>
        /// <param name="task">Task </param>
        public Task(ScheduleTask task)
        {
            this.Type = task.Type;
            this.Enabled = task.Enabled;
            this.StopOnError = task.StopOnError;
            this.Name = task.Name;
            this.CompanyId = task.CompanyID;
            this.OSInterfaceID = task.OSInterfaceID;
        }

        private ITask CreateTask(ILifetimeScope scope)
        {
            ITask task = null;
            if (this.Enabled)
            {
                var type2 = System.Type.GetType(this.Type);
                if (type2 != null)
                {
                    object instance;
                    if (!EngineContext.Current.ContainerManager.TryResolve(type2, scope, out instance))
                    {
                        //not resolved
                        instance = EngineContext.Current.ContainerManager.ResolveUnregistered(type2, scope);
                    }
                    task = instance as ITask;
                }
            }
            return task;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="throwException">A value indicating whether exception should be thrown if some error happens</param>
        /// <param name="dispose">A value indicating whether all instances hsould be disposed after task run</param>
        public void Execute(bool throwException = false, bool dispose = true)
        {
            this.IsRunning = true;

            //background tasks has an issue with Autofac
            //because scope is generated each time it's requested
            //that's why we get one single scope here
            //this way we can also dispose resources once a task is completed
            var scope = EngineContext.Current.ContainerManager.Scope();
            var scheduleTaskService = EngineContext.Current.ContainerManager.Resolve<IScheduleTaskService>("", scope);
            var edi_configService = EngineContext.Current.ContainerManager.Resolve<IEDI_ConfigService>("", scope);
            var scheduleTask = scheduleTaskService.GetTaskByType(this.Type);
            var config = edi_configService.GetConfig(this.CompanyId, this.OSInterfaceID);

            try
            {
                var task = this.CreateTask(scope);
                if (task != null && !CheckStopTaskCondition())
                {
                    this.LastStartUtc = DateTime.Now;
                    if (scheduleTask != null)
                    {
                        //update appropriate datetime properties
                        scheduleTask.LastStartUtc = this.LastStartUtc;
                        scheduleTaskService.UpdateTask(scheduleTask);
                    }
                    //execute task
                    task.Execute(config);
                    this.LastEndUtc = this.LastSuccessUtc = DateTime.Now;
                }
            }
            catch (Exception exc)
            {
                this.Enabled = !this.StopOnError;
                this.LastEndUtc = DateTime.Now;

                //log error
                var logger = EngineContext.Current.ContainerManager.Resolve<ILogger>("", scope);
                logger.Error(string.Format("Error while running the '{0}' schedule task. {1}", this.Name, exc.Message), exc);
                if (throwException)
                    throw;
            }

            if (scheduleTask != null)
            {
                //update appropriate datetime properties
                scheduleTask.LastEndUtc = this.LastEndUtc;
                scheduleTask.LastSuccessUtc = this.LastSuccessUtc;
                scheduleTaskService.UpdateTask(scheduleTask);
            }

            //dispose all resources
            if (dispose)
            {
                scope.Dispose();
            }

            this.IsRunning = false;
        }

        /// <summary>
        /// A value indicating whether a task is running
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Datetime of the last start
        /// </summary>
        public DateTime? LastStartUtc { get; private set; }

        /// <summary>
        /// Datetime of the last end
        /// </summary>
        public DateTime? LastEndUtc { get; private set; }

        /// <summary>
        /// Datetime of the last success
        /// </summary>
        public DateTime? LastSuccessUtc { get; private set; }

        /// <summary>
        /// A value indicating type of the task
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// A value indicating whether to stop task on error
        /// </summary>
        public bool StopOnError { get; private set; }

        /// <summary>
        /// Get the task name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A value indicating whether the task is enabled
        /// </summary>
        public bool Enabled { get; set; }

        public string CompanyId { get; set; }

        public string OSInterfaceID { get; set; }

        private bool CheckStopTaskCondition()
        {
            bool result = false;
            try
            {
                var _golbalSetting = EngineContext.Current.Resolve<Core.Domain.SettingModel.GolbalSetting>();
                var taskStopExecuteTime = _golbalSetting.TaskStopExecuteTime;
                if (string.IsNullOrWhiteSpace(taskStopExecuteTime))
                {
                    return result;
                }
                //Wednesday,Sunday@3:00-7:00
                var splitStr = taskStopExecuteTime.Split('@');
                if (splitStr == null || splitStr.Length != 2)
                {
                    return result;
                }
                var weekStr = splitStr[0].Split(',');
                var currentDateTime = DateTime.Now;
                foreach (var week in weekStr)
                {
                    if (currentDateTime.DayOfWeek.ToString().Equals(week, StringComparison.OrdinalIgnoreCase))
                    {
                        //周几相同，还要判断时间是否相等
                        var timeStr = splitStr[1].Split('-');
                        TimeSpan beginTime, endTime;
                        if (!TimeSpan.TryParse(timeStr[0], out beginTime))
                        {
                            break;
                        }
                        if (!TimeSpan.TryParse(timeStr[1], out endTime))
                        {
                            break;
                        }
                        var currentTime = currentDateTime.TimeOfDay;
                        if (currentTime >= beginTime && currentTime <= endTime)
                        {
                            result = true;
                        }
                        break;
                    }
                }
            }
            catch
            {
                //不需要进行异常处理
            }
            return result;
        }
    }
}
