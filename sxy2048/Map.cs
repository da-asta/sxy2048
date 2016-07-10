using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sxy2048
{
    public class Map
    {
        private int x, y;
        public int X { get { return x; } }
        public int Y { get { return y; } }

        public const char
            NoneChar = '.',
            BlockChar = '%';

        public char[,] place;

        public Map(int x0 = 4, int y0 = 4)
        {
            x = x0; y = y0;
            place = new char[x, y];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    place[i, j] = NoneChar;
        }

        public bool EmptyBlock(int xx, int yy)
        {
            if (xx >= x || xx < 0) return false;
            if (yy >= y || yy < 0) return false;
            if (place[xx, yy] != NoneChar) return false;
            return true;
        }
    }
}
