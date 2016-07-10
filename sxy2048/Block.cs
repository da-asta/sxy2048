using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sxy2048
{
    public class Block
    {
            private int x, y;
            public int X { get { return x; } set { x = value; } }
            public int Y { get { return y; } set { y = value; } }

            public pictureId Id;
            public PictureBox pict;

            public Random random;

            public enum pictureId { p2, p4, p8, p16, p32, p64, p128, p256, p512, p1024, p2048, p4096, p8192, p16384,p32768 };
            public void pictureAndId()
            {
                switch (Id)
                {
                    case pictureId.p2: pict.Image = Properties.Resources.block_2; break;
                    case pictureId.p4: pict.Image = Properties.Resources.block_4; break;
                    case pictureId.p8: pict.Image = Properties.Resources.block_8; break;
                    case pictureId.p16: pict.Image = Properties.Resources.block_16; break;
                    case pictureId.p32: pict.Image = Properties.Resources.block_32; break;
                    case pictureId.p64: pict.Image = Properties.Resources.block_64; break;
                    case pictureId.p128: pict.Image = Properties.Resources.block_128; break;
                    case pictureId.p256: pict.Image = Properties.Resources.block_256; break;
                    case pictureId.p512: pict.Image = Properties.Resources.block_512; break;
                    case pictureId.p1024: pict.Image = Properties.Resources.block_1024; break;
                    case pictureId.p2048: pict.Image = Properties.Resources.block_2048; break;
                    case pictureId.p4096: pict.Image = Properties.Resources.block_4096; break;
                    case pictureId.p8192: pict.Image = Properties.Resources.block_8192; break;
                    case pictureId.p16384: pict.Image = Properties.Resources.block_16384; break;
                    case pictureId.p32768: pict.Image = Properties.Resources.block_32768; break;
                }
                pict.Height = pict.Image.Size.Height;
                pict.Width = pict.Image.Size.Width;
            }
            public int AddScore()
            {
                switch (Id)
                {
                    case pictureId.p2: return 2;
                    case pictureId.p4: return 4;
                    case pictureId.p8: return 8;
                    case pictureId.p16: return 16;
                    case pictureId.p32: return 32;
                    case pictureId.p64: return 64;
                    case pictureId.p128: return 128;
                    case pictureId.p256: return 256;
                    case pictureId.p512: return 512;
                    case pictureId.p1024: return 1024;
                    case pictureId.p2048: return 2048;
                    case pictureId.p4096: return 4096;
                    case pictureId.p8192: return 8192;
                    case pictureId.p16384: return 16384;
                    case pictureId.p32768: return 32768;
                }
                return 0;
            }



            public void InitializeBlock(Map map)
            {
                int x0, y0;
                random = new Random();
                do
                {
                    x0 = random.Next(0, map.X);
                    y0 = random.Next(0, map.Y);
                } while (!map.EmptyBlock(x0, y0));
                x = x0; y = y0;
                map.place[x, y] = Map.BlockChar;
                int temp = random.Next(0, 10);
                if (temp == 0) Id = pictureId.p4;
                else Id = pictureId.p2;
                pict = new PictureBox();
                pictureAndId();
        }
    }
}
