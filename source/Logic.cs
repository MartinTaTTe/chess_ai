using System.Collections.Generic;

namespace source
{
    class Logic
    {
        public bool IsTile(int x, int y)
        {
            return !(x < 0 || x > 7 || y < 0 || y > 7);
        }
    }
}