using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sxy2048
{
    public partial class Form1 : Form
    {
        public Game game;
        public Form1()
        {
            InitializeComponent();
        }
        public void Start()
        {
            game = new Game();
            panel1.Controls.Clear();
            labelNum.Text = "0";
            for (int i = 0; i < 2; i++)
                UpdateBlock(game.block[i]);
        }


        public void UpdateBlock(Block b)
        {
            panel1.Controls.Add(b.pict);
            b.pict.Top = panel1.Height / game.map.X * b.X;
            b.pict.Left = panel1.Width / game.map.Y * b.Y;
        }

        public void LevelUpBlock(Block b)
        {
            b.Id++;
            game.score+=b.AddScore();
            b.pictureAndId();
            UpdateBlock(b);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Start();
        }
        public bool checkAlive()
        {
            int a, b, x0, y0, k;
            for (a = 0; a < game.map.X; a++)
                for (b = 0; b < game.map.Y; b++)
                    if (game.map.EmptyBlock(a, b)) return true;
            for (int i = 0; i < game.map.X * game.map.Y; i++)
            {
                x0 = game.block[i].X; y0 = game.block[i].Y;
                if ((k = game.searchBlock(x0 + 1, y0)) != -1 && game.block[k].Id == game.block[i].Id) return true;
                if ((k = game.searchBlock(x0 - 1, y0)) != -1 && game.block[k].Id == game.block[i].Id) return true;
                if ((k = game.searchBlock(x0, y0 + 1)) != -1 && game.block[k].Id == game.block[i].Id) return true;
                if ((k = game.searchBlock(x0, y0 - 1)) != -1 && game.block[k].Id == game.block[i].Id) return true;
            }
            return false;
        }
        public bool checkWin()
        {
            int i = 0;
            for (; i < game.map.X * game.map.Y; i++)
                if (game.block[i] != null && game.block[i].Id == Block.pictureId.p2048) return true;
            return false;
        }
        public bool checkEnd()
        {
            int i = 0;
            for (; i < game.map.X * game.map.Y; i++)
                if (game.block[i] != null && game.block[i].Id == Block.pictureId.p32768) return true;
            return false;
        }
        public bool leftMove()
        {
            int i, j, k, current;int[] a=new int[game.map.Y]; bool flagMove = false;
            for (i = 0; i < game.map.X; i++)
            {
                for (j = 0; j < game.map.Y; j++)
                    a[j] = game.searchBlock(i, j);
                for (j = 0, k = j + 1; j < game.map.Y - 1; j++, k = j + 1)
                {
                    if (a[j] == -1) continue;
                    for (; k < game.map.Y && a[k] == -1; k++) ;
                    if (k == game.map.Y) break;
                    if (game.block[a[j]].Id == game.block[a[k]].Id)
                    {
                        panel1.Controls.Remove(game.block[a[k]].pict);
                        game.map.place[i, game.block[a[k]].Y] = Map.NoneChar;
                        game.block[a[k]] = null;
                        LevelUpBlock(game.block[a[j]]);
                        flagMove = true;
                        j = k;
                    }
                }

                for (j = 0; j < game.map.Y-1; j++)
                {
                    if (game.map.EmptyBlock(i, j))
                    {
                        for (k = j + 1; game.map.EmptyBlock(i, k)&&k<game.map.Y; k++) ;
                        if (k < game.map.Y)
                        {
                            current = game.searchBlock(i, k);
                            panel1.Controls.Remove(game.block[current].pict);
                            game.map.place[i, k] = Map.NoneChar;
                            game.block[current].Y = j;
                            game.map.place[i, j] = Map.BlockChar;
                            UpdateBlock(game.block[current]);
                            flagMove = true;
                        }
                    }
                }

            }
            return flagMove;
        }
        public bool rightMove()
        {
            int i, j, k, current; int[] a = new int[game.map.Y]; bool flagMove = false;
            for (i = 0; i < game.map.X; i++)
            {
                for (j = 0,k=game.map.Y; j < game.map.Y; j++)
                    a[--k] = game.searchBlock(i, j);
                for (j = 0, k = j + 1; j < game.map.Y - 1; j++, k = j + 1)
                {
                    if (a[j] == -1) continue;
                    for (; k < game.map.Y && a[k] == -1; k++) ;
                    if (k == game.map.Y) break;
                    if (game.block[a[j]].Id == game.block[a[k]].Id)
                    {
                        panel1.Controls.Remove(game.block[a[k]].pict);
                        game.map.place[i, game.block[a[k]].Y] = Map.NoneChar;
                        game.block[a[k]] = null;
                        LevelUpBlock(game.block[a[j]]);
                        flagMove = true;
                        j = k;
                    }
                }

                for (j = game.map.Y-1; j>0; j--)
                {
                    if (game.map.EmptyBlock(i, j))
                    {
                        for (k = j - 1; game.map.EmptyBlock(i, k) && k >=0; k--) ;
                        if (k >=0)
                        {
                            current = game.searchBlock(i, k);
                            panel1.Controls.Remove(game.block[current].pict);
                            game.map.place[i, k] = Map.NoneChar;
                            game.block[current].Y = j;
                            game.map.place[i, j] = Map.BlockChar;
                            UpdateBlock(game.block[current]);
                            flagMove = true;
                        }
                    }
                }

            }
            return flagMove;
        }
        public bool upMove()
        {
            int i, j, k, current; int[] a = new int[game.map.X]; bool flagMove = false;
            for (j = 0; j < game.map.Y; j++)
            {
                for (i = 0; i < game.map.X; i++)
                    a[i] = game.searchBlock(i, j);
                for (i = 0, k = i + 1; i < game.map.X - 1; i++, k = i + 1)
                {
                    if (a[i] == -1) continue;
                    for (; k < game.map.X && a[k] == -1; k++) ;
                    if (k == game.map.X) break;
                    if (game.block[a[i]].Id == game.block[a[k]].Id)
                    {
                        panel1.Controls.Remove(game.block[a[k]].pict);
                        game.map.place[game.block[a[k]].X,j] = Map.NoneChar;
                        game.block[a[k]] = null;
                        LevelUpBlock(game.block[a[i]]);
                        flagMove = true;
                        i = k;
                    }
                }

                for (i = 0; i < game.map.X - 1; i++)
                {
                    if (game.map.EmptyBlock(i, j))
                    {
                        for (k = i + 1; game.map.EmptyBlock(k,j) && k < game.map.X; k++) ;
                        if (k < game.map.X)
                        {
                            current = game.searchBlock(k,j);
                            panel1.Controls.Remove(game.block[current].pict);
                            game.map.place[k,j] = Map.NoneChar;
                            game.block[current].X = i;
                            game.map.place[i,j] = Map.BlockChar;
                            UpdateBlock(game.block[current]);
                            flagMove = true;
                        }
                    }
                }

            }
            return flagMove;
        }
        public bool downMove()
        {
            int i, j, k, current; int[] a = new int[game.map.X]; bool flagMove = false;
            for (j = 0; j < game.map.Y; j++)
            {
                for (i = 0,k=game.map.X; i < game.map.X; i++)
                    a[--k] = game.searchBlock(i, j);
                for (i = 0, k = i + 1; i < game.map.X - 1; i++, k = i + 1)
                {
                    if (a[i] == -1) continue;
                    for (; k < game.map.X && a[k] == -1; k++) ;
                    if (k == game.map.X) break;
                    if (game.block[a[i]].Id == game.block[a[k]].Id)
                    {
                        panel1.Controls.Remove(game.block[a[k]].pict);
                        game.map.place[game.block[a[k]].X, j] = Map.NoneChar;
                        game.block[a[k]] = null;
                        LevelUpBlock(game.block[a[i]]);
                        flagMove = true;
                        i = k;
                    }
                }

                for (i = game.map.X-1; i >0; i--)
                {
                    if (game.map.EmptyBlock(i, j))
                    {
                        for (k = i - 1; game.map.EmptyBlock(k, j) && k >=0; k--) ;
                        if (k >=0)
                        {
                            current = game.searchBlock(k, j);
                            panel1.Controls.Remove(game.block[current].pict);
                            game.map.place[k, j] = Map.NoneChar;
                            game.block[current].X = i;
                            game.map.place[i, j] = Map.BlockChar;
                            UpdateBlock(game.block[current]);
                            flagMove = true;
                        }
                    }
                }

            }
            return flagMove;
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void instructorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press '↑''↓''←''→'to move the blocks", "HELP");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                bool flagMove = false;
                switch (e.KeyCode)
                {
                    case Keys.Left: flagMove = leftMove(); break;
                    case Keys.Right: flagMove = rightMove(); break;
                    case Keys.Up: flagMove = upMove(); break;
                    case Keys.Down: flagMove = downMove(); break;
                }
                if (!checkAlive())
                {
                    MessageBox.Show("Can't move,you lose!\r\n" +
                        "Your score is" + labelNum.Text, "LOSE");
                    Start();
                }

                if (!game.haveSucceeded && checkWin())
                {
                    MessageBox.Show("You've got the 2048 block and win!" +
                                    "Now you can strive for the bigger blocks", "WIN");
                    game.haveSucceeded = true;
                }
                if (game.haveSucceeded && checkEnd())
                {
                    MessageBox.Show("It's Unbeliveable\r\n" +
                        "that you can get the ultimate block!\r\n" +
                        "Congutulations,and your final score is:" + labelNum.Text, "END");
                    Application.Exit();
                }
                if (flagMove)
                {
                    for (int i = 0; i < game.map.X * game.map.Y; i++)
                        if (game.block[i] == null)
                        {
                            game.block[i] = new Block();
                            game.block[i].InitializeBlock(game.map);
                            UpdateBlock(game.block[i]);
                            break;
                        }
                    labelNum.Text = game.score.ToString();
                }
            }
        }
    }
}
