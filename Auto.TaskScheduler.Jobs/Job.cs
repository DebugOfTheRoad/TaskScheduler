using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace TianWei.TaskScheduler.Jobs
{
    [Export(typeof(IJob))]
    public class Job1 : IJob
    {
        public void Execute()
        {
            Console.WriteLine("Job1" + DateTime.Now);
        }
    }

    [Export(typeof(IJob))]
    public class Job2 : IJob
    {
        public void Execute()
        {
            Console.WriteLine("Job2" + DateTime.Now);
        }
    }

    [Export(typeof(IJob))]
    public class Job3 : IJob
    {
        public void Execute()
        {
            Console.WriteLine("Job3" + DateTime.Now);
        }
    }
}
