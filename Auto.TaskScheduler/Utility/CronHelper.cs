using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianWei.TaskScheduler.Utility
{
    /// <summary>
    /// Cron解析类
    /// </summary>
    public class CronHelper
    {
        /// <summary>
        /// Cron解析
        /// </summary>
        /// <param name="jobDetail">作业详情</param>
        /// <returns></returns>
        public static JobDetail SetCron(JobDetail jobDetail)
        {
            if (jobDetail == null) return jobDetail;
            if (string.IsNullOrEmpty(jobDetail.CronExpression)) return jobDetail;
            var cronArr = jobDetail.CronExpression.Split(' ');
            VerifyCron(cronArr);
            if (cronArr.Length != 6) return jobDetail;
            if (cronArr[5] != "*")
            {
                jobDetail.Week = (DayOfWeek)Convert.ToInt32(cronArr[5]);
                jobDetail.WorkType = WorkType.Week;
                jobDetail.ExecuteExpression = cronArr[2] + cronArr[1] + cronArr[0];
            }
            else if (cronArr[4] != "*")
            {
                jobDetail.WorkType = WorkType.Yearly;
                jobDetail.ExecuteExpression = cronArr[4] + cronArr[3] + cronArr[2] + cronArr[1] + cronArr[0];
            }
            else if (cronArr[3] != "*")
            {
                if (cronArr[3] == "0")
                {
                    jobDetail.WorkType = WorkType.Daily;
                    jobDetail.ExecuteExpression = cronArr[2] + cronArr[1] + cronArr[0];
                }
                else
                {
                    jobDetail.WorkType = WorkType.Monthly;
                    jobDetail.ExecuteExpression = cronArr[3] + cronArr[2] + cronArr[1] + cronArr[0];
                }
            }
            else
            {
                jobDetail.WorkType = WorkType.Loop;
                jobDetail.Interval = Convert.ToInt32(cronArr[2]) * 60 * 60 * 1000 + Convert.ToInt32(cronArr[1]) * 60 * 1000 + Convert.ToInt32(cronArr[0]) * 1000;
            }
            return jobDetail;
        }

        private static void VerifyCron(string[] arr)
        {
            try
            {
                for (int i = 0; i < 6;i++ )
                {
                    if (arr[i].Trim() != "*")
                    {
                        int time = Convert.ToInt32(arr[i].Trim());
                        if (time < 0 || time > 59)
                        {
                            throw new Exception("Cron格式不正确");
                        }
                        arr[i] = arr[i].Trim().PadLeft(2, '0');
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
