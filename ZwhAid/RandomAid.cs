using System;

namespace ZwhAid
{
    public class RandomAid : ZwhBase
    {
        public int GetRandom(int min, int max)
        {
            try
            {
                Random r = new Random(min);
                zInt = r.Next(max);
            }
            catch { }

            return ZInt;
        }
    }
}
