using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ZwhAid
{
    public static class SequentialGuid
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        static extern int UuidCreateSequential(out Guid guid);

        public static Guid NewGuid()
        {
            const int RPC_S_OK = 0;

            Guid guid;
            int result = UuidCreateSequential(out guid);
            if (result != RPC_S_OK)
            {
                throw new ApplicationException("Create sequential guid failed: " + result);
            }

            return guid;
        }
    }
}
