using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Minesweeper
{
    class Board
    {
        public int MineCount;
        private static readonly int offset = 75;
        private static readonly int square_distance = 75;
        private static readonly Point nullPoint = new Point(-1, -1);  //方格外的位置，都修正成此點
        public BoardArrayClass[,] ArrayClass = new BoardArrayClass[8, 8];

        private Random mineRandom = new Random();

        public bool CanBePlace(int Mouse_x, int Mouse_y)  //放置的位置接近哪，是否可踩，用游標提示使用者
        {        
            Point BoardPoint = CheckThePoint(Mouse_x, Mouse_y);
            if (BoardPoint == nullPoint)
                return false;

            return true;
        }

        public Mine SweepMine(int Mouse_x, int Mouse_y)  //判斷"現在"踩下後，是否有地雷
        {
            int minecount;

            Point BoardPoint = CheckThePoint(Mouse_x, Mouse_y);
            int BoardPoint_X = transformNode(BoardPoint.X);
            int BoardPoint_Y = transformNode(BoardPoint.Y);

            if (BoardPoint != nullPoint)  
                ArrayClass[BoardPoint.X, BoardPoint.Y].whetherClick = true;  //如果該位置已踩過，則truef 

            if (BoardPoint == nullPoint)
                return null;
            else if (ArrayClass[BoardPoint.X, BoardPoint.Y].whetherMine == true)
                return new Mine(BoardPoint_X, BoardPoint_Y);
            else if (ArrayClass[BoardPoint.X, BoardPoint.Y].whetherMine != true)
            {
                minecount = MineNumberNear(BoardPoint.X, BoardPoint.Y);
                return new Mine(BoardPoint_X, BoardPoint_Y, minecount);
            }
            else
                return null;
        }

        public int MineNumberNear(int x, int y) //若該位置不是地雷，則計算周圍有多少地雷
        {
            int mineCount = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0|| x + i<0|| x + i>7|| y + j<0|| y + j>7)
                        continue;
                    else if (ArrayClass[x+i, y+j].whetherMine == true)
                        mineCount++;
                }
            }

            return mineCount;
        }

        public Point CheckThePoint(int X_coordinate, int Y_coordinate) //不讓把方格外的位置，影響遊戲
        {
            int X_node = ReviseThePoint(X_coordinate);
            int Y_node = ReviseThePoint(Y_coordinate);
            if (X_node == -1 || Y_node == -1)
                return new Point(-1, -1);  //在此時體化
            else
                return new Point(X_node, Y_node);
        }

        public int ReviseThePoint(int node) // 判斷游標位置靠近哪個方格點
        {
            if (node < offset || node > (offset + 8 * square_distance))
                return -1;

            int quotient = (node - offset - (square_distance / 2)) / square_distance;
            int remainder = (node - offset - (square_distance / 2)) % square_distance;
            if (remainder < (square_distance / 2))
                return quotient;
            else if (remainder > (square_distance / 2))
                return quotient + 1;
            else
                return -1;
        }

        public int transformNode(int node) //將矩陣座標轉回xy平面座標
        {
            int outnode;
            outnode = (offset + square_distance / 2) + square_distance * node;

            return outnode;

        }

        public void setMine()  //用於開啟遊戲後，先建立所有方格物件，再建立地雷
        {
            for (int i = 0; i < 8; i++)  //建立所有方格物件
            {
                for (int j = 0; j < 8; j++)
                {
                    ArrayClass[i,j] = new BoardArrayClass(i, j);
                }
            }
            MineCount = 10;//預設地雷有10個
            int count = 0;
            bool CheckNum = false;
            int[] sotrerandNum = new int[MineCount];

            while (count < MineCount)    //預設地雷有10個，此段用於建立10個不同位置的地雷，不能重複
            {
                int randNum = mineRandom.Next(0, 64);
                for (int i = 0; i < MineCount; i++)
                {
                    if (sotrerandNum[i] == randNum)
                    {
                        CheckNum = false;
                        break;
                    }
                    else
                        CheckNum = true;
                }

                if (CheckNum == true)
                {
                    sotrerandNum[count] = randNum;
                    count++;
                }
            }

            for (int k = 0; k < MineCount; k++)  //有了地雷位置後，一個一個放入該方格物件裡
            {
                int quotient_Mine = sotrerandNum[k] / 8;
                int remainder_Mine = sotrerandNum[k] % 8;

                ArrayClass[remainder_Mine, quotient_Mine].whetherMine = true;
            }

        }

        public Mine whereTheMine(int boardarray_x, int boardarray_y)  //用於遊戲結束前，幫忙顯示所有地雷位置
        {
            boardarray_x = transformNode(boardarray_x);   //將矩陣座標轉回xy平面座標
            boardarray_y = transformNode(boardarray_y);

            return new Mine(boardarray_x, boardarray_y);
        }

    }
}
