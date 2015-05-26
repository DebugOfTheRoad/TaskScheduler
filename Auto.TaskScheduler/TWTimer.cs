using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianWei.TaskScheduler
{
    /// <summary>
    /// 自定义Timer
    /// </summary>
    public class TWTimer : System.Timers.Timer
    {
        public JobDetail JobDetail { get; set; }
    }
}
