using System;

namespace ZwhAid
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogAid : ZwhBase
    {
        private string path;
        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            set { path = value; }
            get { return path; }
        }

        /// <summary>
        /// 
        /// </summary>
        public LogAid()
        {
            IOAid io = new IOAid();
            path = io.GetRootPath();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectString"></param>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public bool TxtLog(string connectString, string sqlText)
        {
            try
            {
                SQLAid sa = new SQLAid(connectString);
                zBool = sa.ExecuteSQL(sqlText);
            }
            catch { }

            return ZBool;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool TxtDayLog(string text)
        {
            try
            {
                IOAid io = new IOAid();
                io.FilePath = path + "Log";
                if (io.CreateDirectory(io.FilePath))
                {
                    DateTime dtnow = DateTime.Now;
                    io.FileName = dtnow.Year.ToString() + dtnow.Month.ToString() + dtnow.Day.ToString();
                    io.FileFullPath = io.FilePath + "\\" + io.FileName;
                    io.CreateFile(io.FileFullPath, false);
                    string headDes = io.FileName + dtnow.Hour.ToString() + dtnow.Minute.ToString() + dtnow.Second.ToString() + dtnow.Millisecond.ToString();
                    io.AppendLineText(headDes + "······" + text);
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool TxtWeekLog(string text)
        {
            try
            {
                IOAid io = new IOAid();
                io.FilePath = path + "Log";
                if (io.CreateDirectory(io.FilePath))
                {
                    DateTime dtnow = DateTime.Now;
                    io.FileName = GetWeekOfYear().ToString();
                    io.FileFullPath = io.FilePath + "\\" + io.FileName;
                    io.CreateFile(io.FileFullPath, false);
                    string headDes = io.FileName + dtnow.Hour.ToString() + dtnow.Minute.ToString() + dtnow.Second.ToString() + dtnow.Millisecond.ToString();
                    io.AppendLineText(headDes + "······" + text);
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        /// <summary>
        /// 获取当前第几周
        /// </summary>
        /// <returns></returns>
        private static int GetWeekOfYear()
        {
            //一.找到第一周的最后一天（先获取1月1日是星期几，从而得知第一周周末是几）
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(DateTime.Today.Year + "-1-1").DayOfWeek);
            //二.获取今天是一年当中的第几天
            int currentDay = DateTime.Today.DayOfYear;
            //三.（今天 减去 第一周周末）/7 等于 距第一周有多少周 再加上第一周的1 就是今天是今年的第几周了
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool TxtMonthLog(string text)
        {
            try
            {
                IOAid io = new IOAid();
                io.FilePath = path + "Log";
                if (io.CreateDirectory(io.FilePath))
                {
                    DateTime dtnow = DateTime.Now;
                    io.FileName = dtnow.Month.ToString();
                    io.FileFullPath = io.FilePath + "\\" + io.FileName;
                    io.CreateFile(io.FileFullPath, false);
                    string headDes = io.FileName + dtnow.Hour.ToString() + dtnow.Minute.ToString() + dtnow.Second.ToString() + dtnow.Millisecond.ToString();
                    io.AppendLineText(headDes + "······" + text);
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool TxtSeasonLog(string text)
        {
            try
            {
                IOAid io = new IOAid();
                io.FilePath = path + "Log";
                if (io.CreateDirectory(io.FilePath))
                {
                    DateTime dtnow = DateTime.Now;
                    switch (dtnow.Month)
                    {
                        case 1:
                            io.FileName = "spring";
                            break;
                        case 2:
                            io.FileName = "spring";
                            break;
                        case 3:
                            io.FileName = "spring";
                            break;
                        case 4:
                            io.FileName = "summer";
                            break;
                        case 5:
                            io.FileName = "summer";
                            break;
                        case 6:
                            io.FileName = "summer";
                            break;
                        case 7:
                            io.FileName = "autumn";
                            break;
                        case 8:
                            io.FileName = "autumn";
                            break;
                        case 9:
                            io.FileName = "autumn";
                            break;
                        case 10:
                            io.FileName = "winter";
                            break;
                        case 11:
                            io.FileName = "winter";
                            break;
                        case 12:
                            io.FileName = "winter";
                            break;
                    }
                    io.FileFullPath = io.FilePath + "\\" + io.FileName;
                    io.CreateFile(io.FileFullPath, false);
                    string headDes = io.FileName + dtnow.Hour.ToString() + dtnow.Minute.ToString() + dtnow.Second.ToString() + dtnow.Millisecond.ToString();
                    io.AppendLineText(headDes + "······" + text);
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool TxtYearLog(string text)
        {
            try
            {
                IOAid io = new IOAid();
                io.FilePath = path + "Log";
                if (io.CreateDirectory(io.FilePath))
                {
                    DateTime dtnow = DateTime.Now;
                    io.FileName = dtnow.Year.ToString();
                    io.FileFullPath = io.FilePath + "\\" + io.FileName;
                    io.CreateFile(io.FileFullPath, false);
                    string headDes = io.FileName + dtnow.Hour.ToString() + dtnow.Minute.ToString() + dtnow.Second.ToString() + dtnow.Millisecond.ToString();
                    io.AppendLineText(headDes + "······" + text);
                    zBool = true;
                }
            }
            catch { }

            return ZBool;
        }
    }
}
