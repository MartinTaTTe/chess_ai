using System.Collections.Generic;

namespace source
{
    class Logic
    {
        /*private static Dictionary<Type, int> typeMap = new Dictionary<Type, int>();
        public Logic()
        {
            typeMap.Add(typeof(Pawn), 0);
        }*/
        public bool IsTile(int x, int y)
        {
            return !(x < 0 || x > 7 || y < 0 || y > 7);
        }
    }
}