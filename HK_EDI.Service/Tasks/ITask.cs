
using HK_EDI.Core.Domain.EDI_Config;
namespace HK_EDI.Services.Tasks
{
    /// <summary>
    /// Interface that should be implemented by each task
    /// </summary>
    public partial interface ITask
    {
        /// <summary>
        /// Execute task
        /// </summary>
        void Execute(SD_Adibus_Interface config);
    }
}
