using TianWei.TaskScheduler.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianWei.TaskScheduler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Scheduler sc = new Scheduler();
            sc.Execute();
            Console.ReadKey();
        }
    }
}
