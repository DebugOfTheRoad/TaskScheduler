using TianWei.TaskScheduler.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Timers;

namespace TianWei.TaskScheduler
{
    public class Scheduler
    {
        [ImportMany(typeof(IJob))]
        public List<IJob> jobs;
        public Dictionary<string, IJob> dicJobs;
        public Dictionary<string, TWTimer> dicTimer;
        private void Run()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory));
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public void Execute()
        {
            Run();
            SetDicJobs();
            SetDicTimers();
            FileWatcher();
        }

        private void SetDicJobs()
        {
            if (jobs != null)
            {
                dicJobs = new Dictionary<string, IJob>();
                foreach (var job in jobs)
                {
                    dicJobs.Add(job.ToString(), job);
                }
            }
        }

        private void SetDicTimers()
        {
            dicTimer = new Dictionary<string, TWTimer>();
            var jobList = (List<JobDetail>)XmlHelper.XmlDeserialize(typeof(List<JobDetail>), Config.ConfigPath);
            if (jobList != null)
            {
                foreach (var item in jobList)
                {
                    SetTimer(item);
                }
            }
        }

        /// <summary>
        /// Timer
        /// </summary>
        /// <param name="jobDetail"></param>
        private void SetTimer(JobDetail jobDetail)
        {
            TWTimer timer = new TWTimer();
            timer.JobDetail = CronHelper.SetCron(jobDetail);
            if (timer.JobDetail.WorkType == WorkType.Loop)
            {
                timer.Interval = timer.JobDetail.Interval;
            }
            else
            {
                timer.Interval = 1000;
            }
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            dicTimer.Add(timer.JobDetail.Name, timer);
        }

        /// <summary>
        /// Timer事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                var timer = (TWTimer)source;
                if (dicJobs.Any(o => o.Key == timer.JobDetail.JobType))
                {
                    Job job = new Job();
                    job.Execute(timer.JobDetail, dicJobs[timer.JobDetail.JobType]);
                }
            }
            catch (Exception ex)
            {
                //记录日志
            }
        }

        /// <summary>
        /// 文件监听
        /// </summary>
        private void FileWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Environment.CurrentDirectory;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = Config.ConfigFile;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// 文件改动事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var jobList = (List<JobDetail>)XmlHelper.XmlDeserialize(typeof(List<JobDetail>), Config.ConfigPath);
            if (jobList != null)
            {
                foreach (var item in jobList)
                {
                    if (dicTimer.Any(o => o.Key == item.Name))
                    {
                        var timer = dicTimer[item.Name];
                        if (item.JobType != timer.JobDetail.JobType || item.CronExpression != timer.JobDetail.CronExpression)
                        {
                            timer.JobDetail = CronHelper.SetCron(item);
                            if (timer.JobDetail.WorkType == WorkType.Loop)
                            {
                                timer.Interval = timer.JobDetail.Interval;
                            }
                            else
                            {
                                timer.Interval = 1000;
                            }
                        }
                        timer.JobDetail.Enabled = item.Enabled;
                        timer.JobDetail.StartTime = item.StartTime;
                        timer.JobDetail.EndTime = item.EndTime;
                    }
                    else
                    {
                        SetTimer(item);
                    }
                }
            }
        }
    }
}
