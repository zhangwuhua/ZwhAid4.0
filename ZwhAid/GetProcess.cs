using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace ZwhAid
{
    /// <summary>
    /// 
    /// </summary>
    public class GetProcess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Process[] GetInstance(string name)
        {
            List<Process> lists = new List<Process>();
            try
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    if (p.ProcessName.ToLower().Equals(name))
                    {
                        lists.Add(p);
                    }
                }
            }
            catch { }
            return lists.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Process[] GetCurrentInstance()
        {
            List<Process> lists = new List<Process>();
            try
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    if (p.Id != current.Id && p.ProcessName == current.ProcessName)
                    {
                        string path = Assembly.GetExecutingAssembly().Location;
                        if (path.Substring(0, path.LastIndexOf("\\")) == current.MainModule.FileName.Substring(0, current.MainModule.FileName.LastIndexOf("\\")))
                        {
                            lists.Add(p);
                        }
                    }
                }
            }
            catch { }
            return lists.ToArray();
        }

        public static bool ProcessRun(Process process)
        {
            bool bl = false;
            try
            {
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    if (p.Id != process.Id && p.ProcessName == process.ProcessName)
                    {
                        string path = Assembly.GetExecutingAssembly().Location;
                        if (path.Substring(0, path.LastIndexOf("\\")) == process.MainModule.FileName.Substring(0, process.MainModule.FileName.LastIndexOf("\\")))
                        {
                            bl = true;
                        }
                    }
                }
            }
            catch { }
            return bl;
        }
    }
}
