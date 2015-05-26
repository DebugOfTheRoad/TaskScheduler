using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianWei.TaskScheduler
{
    /// <summary>
    /// 作业接口
    /// </summary>
    public interface IJob
    {
        /// <summary>
        /// 作业需要继承的接口
        /// </summary>
        void Execute();

    }
}
