using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianWei.TaskScheduler
{
    /// <summary>
    /// 作业工作类型
    /// </summary>
    public enum WorkType
    {
        /// <summary>
        /// 循环
        /// </summary>
        Loop = 1,
        /// <summary>
        /// 每天
        /// </summary>
        Daily = 2,
        /// <summary>
        /// 每月
        /// </summary>
        Monthly = 3,
        /// <summary>
        /// 每年
        /// </summary>
        Yearly = 4,
        /// <summary>
        /// 每周
        /// </summary>
        Week = 5
    }
}
