using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sxy2048
{
    public class Game
    {
        public Map map;
        public Block[] block;
        public int score;
        public bool haveSucceeded;


        public Game()
        {
            map = new Map();
            block = new Block[map.X * map.Y];
            score = 0;
            haveSucceeded = false;
            for (int i = 0; i < 2; i++)
            {
                block[i] = new Block();
                block[i].InitializeBlock(map);
            }
        }

        public int searchBlock(int x0, int y0)
        {
            for (int i = 0; i < map.X * map.Y; i++)
            {
                if (block[i] == null) continue;
                if (block[i].X == x0 && block[i].Y == y0) return i;
            }
            return -1;
        }


    }
}
