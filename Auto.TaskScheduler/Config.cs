using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianWei.TaskScheduler
{
    public class Config
    {
        /// <summary>
        /// 配置文件地址
        /// </summary>
        public static string ConfigPath = Environment.CurrentDirectory + ConfigurationManager.AppSettings["JobsConfig"];
        /// <summary>
        /// 配置文件名
        /// </summary>
        public static string ConfigFile = System.IO.Path.GetFileName(ConfigPath);
    }
}
