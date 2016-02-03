using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZwhAid
{
    /// <summary>
    /// 序列号生成，单例模式
    /// </summary>
    public class GenerateSN
    {
        private static GenerateSN gsn;
        private static object obj = new Object();
        private GenerateSN() { }
        public static GenerateSN GSN()
        {
            lock (obj)
            {
                if (gsn == null)
                {
                    gsn = new GenerateSN();
                }
                return gsn;
            }
        }
    }
}
