using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZwhAid
{
    public class DBTypeSwitch
    {
        public static MUType GetDBType(int item)
        {
            MUType mut = MUType.NULL;
            try
            {
                switch (item)
                {
                    case 1:
                        mut = MUType.MSSQLSERVER;
                        break;
                    case 2:
                        mut = MUType.MYSQL;
                        break;
                    case 3:
                        mut = MUType.ORACLE;
                        break;
                    case 4:
                        mut = MUType.MONGODB;
                        break;
                        break;
                }
            }
            catch { }
            return mut;
        }
    }
}
