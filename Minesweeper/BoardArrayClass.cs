using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    class BoardArrayClass
    {
        public bool whetherMine = false;  //是否是地雷，預設為False
        public bool whetherClick = false; //判斷是否已被踩過
        private int x_boardArray;
        private int y_boardArray;

        public BoardArrayClass(int x,int y)
        {
            x_boardArray = x;
            y_boardArray = y;
        }
    }
}
