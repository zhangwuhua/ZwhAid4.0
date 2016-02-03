using System;

namespace ZwhAid
{
    public static class SequentialId
    {
        [System.Runtime.InteropServices.DllImport("rpcrt4.dll", SetLastError = true)]
        static extern int UuidCreateSequential(out Guid guid);

        public static Guid NewGuid()
        {
            const int RPC_S_OK = 0;

            Guid guid;
            int result = UuidCreateSequential(out guid);
            if (result != RPC_S_OK)
            {
                guid = Guid.Empty;
            }

            return guid;
        }
    }
}
