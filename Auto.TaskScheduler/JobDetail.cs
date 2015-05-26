using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TianWei.TaskScheduler
{
    /// <summary>
    /// 作业请情
    /// </summary>
    [XmlRootAttribute("xml", IsNullable = false)]
    public class JobDetail
    {
        /// <summary>
        /// 作业名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 作业执行类
        /// </summary>
        public string JobType { get; set; }

        /// <summary>
        /// 自定义Cron表达式
        /// </summary>
        public string CronExpression { get; set; }

        /// <summary>
        /// 作业类型
        /// </summary>
        [XmlIgnoreAttribute]
        public WorkType WorkType { get; set; }

        /// <summary>
        /// 如果是每周 周几
        /// </summary>
        [XmlIgnoreAttribute]
        public DayOfWeek Week { get; set; }

        /// <summary>
        /// 执行表达式
        /// </summary>
        [XmlIgnoreAttribute]
        public string ExecuteExpression { get; set; }

        /// <summary>
        /// 执行间隔 循环执行有效
        /// </summary>
        [XmlIgnoreAttribute]
        public int Interval { get; set; }

        /// <summary>
        /// 作业状态 停启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 作业开始工作时间- 可为空
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 作业结束时间-可为空
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 作业开始工作时间-默认为最小时间
        /// </summary>
        [XmlIgnoreAttribute]
        public DateTime JobStartTime
        {
            get
            {
                DateTime value = DateTime.MinValue;
                if (StartTime != null)
                {
                    DateTime.TryParse(StartTime, out value);
                }
                return value;
            }
            set { }
        }

        /// <summary>
        /// 作业结束工作时间-默认为最大时间
        /// </summary>
        [XmlIgnoreAttribute]
        public DateTime JobEndTime
        {
            get
            {
                DateTime value = DateTime.MaxValue;
                if (EndTime != null)
                {
                    DateTime.TryParse(EndTime, out value);
                }
                return value;
            }
            set { }
        }
    }
}
