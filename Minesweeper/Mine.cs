using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    class Mine : PictureBox
    {
        private static readonly int reviselength_Mine = 50;
        private static readonly int reviselength_Num = 64;
        public bool whetherTheMine;

        public Mine(int x, int y) //地雷
        {
            this.Location = new Point(x - (reviselength_Mine / 2), y - (reviselength_Mine / 2));
            this.Size = new Size(50, 50);
            this.Image = Properties.Resources.Mine;
            whetherTheMine = true;
        }

        public Mine(int x, int y, int num) //無地雷，顯示周圍有多地雷
        {
            this.Location = new Point(x - (reviselength_Num / 2), y - (reviselength_Num / 2));
            this.Size = new Size(64, 64);
            numMine(num);
            whetherTheMine = false;
        }

        public void numMine(int num)
        {
            num++;
            switch (num)
            {
                case 1:
                    this.Image = Properties.Resources.zero;
                    break;
                case 2:
                    this.Image = Properties.Resources.one;
                    break;
                case 3:
                    this.Image = Properties.Resources.two;
                    break;
                case 4:
                    this.Image = Properties.Resources.three;
                    break;
                case 5:
                    this.Image = Properties.Resources.four;
                    break;
                case 6:
                    this.Image = Properties.Resources.five;
                    break;
                case 7:
                    this.Image = Properties.Resources.six;
                    break;
                case 8:
                    this.Image = Properties.Resources.seven;
                    break;
                case 9:
                    this.Image = Properties.Resources.eight;
                    break;

            }
        }
    }
}
