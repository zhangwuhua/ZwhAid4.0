using System.Management;

namespace ZwhAid
{
    /// <summary>
    /// 计算机硬件操作
    /// </summary>
    public class HardwareAid : ZwhBase
    {
        /// <summary>
        /// 获取CPU序列号
        /// </summary>
        /// <returns></returns>
        public string GetCPUSN()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    zString = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
            }
            catch { }

            return ZString;
        }

        /// <summary>
        /// 获取磁盘C盘序列号
        /// </summary>
        /// <returns></returns>
        public string GetDiskSN()
        {
            try
            {
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
                zString = disk.Properties["VolumeSerialNumber"].Value.ToString();
            }
            catch { }

            return ZString;
        }

        /// <summary>
        /// 获取主板序列号
        /// </summary>
        /// <returns></returns>
        public string GetBoardSN()
        {
            try
            {
                ManagementClass mc = new ManagementClass("WIN32_BaseBoard");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    zString = mo["SerialNumber"].ToString();
                    break;
                }
            }
            catch { }

            return ZString;
        }

        /// <summary>
        /// 获取网卡序列号
        /// </summary>
        /// <returns></returns>
        public string GetMACSN()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                        zString = mo["MacAddress"].ToString();
                    mo.Dispose();
                }
            }
            catch { }

            return ZString;
        }
    }
}
