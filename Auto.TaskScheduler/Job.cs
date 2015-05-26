using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace TianWei.TaskScheduler
{
    public class Job
    {
        public void Execute(JobDetail jobDetail, IJob job)
        {
            if (!jobDetail.Enabled) return;
            if (DateTime.Now < jobDetail.JobStartTime || DateTime.Now > jobDetail.JobEndTime) return;
            if (jobDetail.WorkType == WorkType.Week)
            {
                if (jobDetail.Week == DateTime.Now.DayOfWeek && jobDetail.ExecuteExpression == DateTime.Now.ToString("HHmmss"))
                {
                    job.Execute();
                }
            }
            else if (jobDetail.WorkType == WorkType.Yearly)
            {
                if (jobDetail.ExecuteExpression == DateTime.Now.ToString("MMddHHmmss"))
                {
                    job.Execute();
                }
            }
            else if (jobDetail.WorkType == WorkType.Monthly)
            {
                if (jobDetail.ExecuteExpression == DateTime.Now.ToString("ddHHmmss"))
                {
                    job.Execute();
                }
            }
            else if (jobDetail.WorkType == WorkType.Daily)
            {
                if (jobDetail.ExecuteExpression == DateTime.Now.ToString("HHmmss"))
                {
                    job.Execute();
                }
            }
            else if (jobDetail.WorkType == WorkType.Loop)
            {
                job.Execute();
            }
        }



    }
}
