using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Minesweeper : Form
    {
        Board board = new Board();

        public Minesweeper()
        {
            InitializeComponent();

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            bool Canplace=board.CanBePlace(e.X, e.Y);
            if (Canplace == true) 
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;

        }

        private void Minesweeper_MouseDown(object sender, MouseEventArgs e)
        {
            Mine addMine=board.SweepMine(e.X, e.Y);

            if (addMine != null)                    //判斷是否有踩到地雷
            {
                this.Controls.Add(addMine);
                if (addMine.whetherTheMine == true)
                {
                    MessageBox.Show("game over");
                    for (int i = 0; i < 8; i++)         //若有踩到地雷，最後顯示所有地雷位置
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (board.ArrayClass[i, j].whetherMine == true)
                                this.Controls.Add(board.whereTheMine(i, j));
                        }
                    }
                }
                else
                {
                    int noMineCount = 0;
                    for (int i = 0; i < 8; i++)         //計算目前踩了多少沒地雷的位置
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (board.ArrayClass[i, j].whetherMine == false && board.ArrayClass[i, j].whetherClick == true)
                                noMineCount++;
                        }
                    }
                    this.Text = noMineCount.ToString();
                    if (noMineCount==64-board.MineCount)
                        MessageBox.Show("You Win!!!!!!");
                }
            }
           
        }

        private void Minesweeper_Load(object sender, EventArgs e)
        {
            board.setMine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Restart();
        }

        #region 自董重啟程式
        private void Restart()
        {
            System.Threading.Thread thtmp = new System.Threading.Thread(new
            System.Threading.ParameterizedThreadStart(run));

            object appName = Application.ExecutablePath;
            System.Threading.Thread.Sleep(2000);
            thtmp.Start(appName);
        }

        private void run(Object obj)
        {
            System.Diagnostics.Process ps = new System.Diagnostics.Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.Start();
        }
        #endregion
    }
}
