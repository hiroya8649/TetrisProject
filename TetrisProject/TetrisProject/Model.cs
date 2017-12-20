using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace TetrisProject
{
    public enum Tetrominoes
    {
        NoShape, ZShape, SShape, LineShape,
        TShape, SquareShape, LShape, MirroredLShape
    };
    public class Model
    {
        public class Brick
        {
            private Tetrominoes pieceShape;
            private int[][] coords;//形狀 九宮格  中間那格當0,0  (之後再改)
            private int[][][] coordsTable;
            public Brick()
            {
                //coords = new int[4][2];
                //setShape(Tetrominoes.NoShape);
            }
            public void setShape(Tetrominoes shape)
            {
                //    coordsTable = new int[][][] {
                //{ { 0, 0 },   { 0, 0 },   { 0, 0 },   { 0, 0 } },
                //{ { 0, -1 },  { 0, 0 },   { -1, 0 },  { -1, 1 } },
                //{ { 0, -1 },  { 0, 0 },   { 1, 0 },   { 1, 1 } },
                //{ { 0, -1 },  { 0, 0 },   { 0, 1 },   { 0, 2 } },
                //{ { -1, 0 },  { 0, 0 },   { 1, 0 },   { 0, 1 } },
                //{ { 0, 0 },   { 1, 0 },   { 0, 1 },   { 1, 1 } },
                //{ { -1, -1 }, { 0, -1 },  { 0, 0 },   { 0, 1 } },
                //{ { 1, -1 },  { 0, -1 },  { 0, 0 },   { 0, 1 } }
                //};

                //    for (int i = 0; i < 4; i++)
                //    {
                //        for (int j = 0; j < 2; ++j)
                //        {
                //            coords[i][j] = coordsTable[shape.ordinal()][i][j];
                //        }
                //    }
                //    pieceShape = shape;
            }
            private void setShapeX(int index, int x) { coords[index][0] = x; }
            private void setShapeY(int index, int y) { coords[index][1] = y; }
            public int getShape_minY()//找出最低的Y
            {
                int m = coords[0][1];
                for (int i = 0; i < 4; i++)
                {
                    m = Math.Min(m, coords[i][1]);
                }
                return m;
            }
            public Brick rotate()//將方塊右轉 回傳結果
            {
                if (pieceShape == Tetrominoes.SquareShape)
                    return this;

                Brick result = new Brick();
                result.pieceShape = pieceShape;

                for (int i = 0; i < 4; ++i)
                {
                    result.setShapeX(i, -coords[i][1]);
                    result.setShapeY(i, coords[i][0]);
                }
                return result;
            }
            public Brick moveRight()//將方塊右移 回傳結果 
            {
                Brick result = new Brick();
                return result;
            }
            public Brick moveLeft()//將方塊左移 回傳結果 
            {
                Brick result = new Brick();
                return result;
            }
            public Brick moveDown(int y)//將方塊往下 回傳結果 
            {
                Brick result = new Brick();
                return result;
            }
            public Brick moveOneLineDown()//方塊慢慢落下 回傳結果 
            {
                Brick result = new Brick();
                return result;
            }

            /*21 add in 11/28*/
            public void changeSpeed() { }//變更level
        }
        public class Background
        {
            private int BoardW = 0;//主畫面的大小
            private int BoardH = 0;
            private int BrickBoardW = 0;//遊戲畫面的大小
            private int BrickBoardH = 0;
            private Point BrickBoard = new Point(0, 0);//遊戲畫面的位置
            private Point btnStart = new Point(0, 0);//開始按鈕的位置
            private Point btnPause = new Point(0, 0);//暫停按鈕的位置
            public Background()
            {
                BoardW = 0;
                BoardH = 0;
                BrickBoardW = 0;
                BrickBoardH = 0;
                BrickBoard.X = 0;
                BrickBoard.Y = 0;
                btnStart.X = 0;
                btnStart.Y = 0;
                btnPause.X = 0;
                btnPause.Y = 0;
            }
            
            public Background(int W,int H,int BrickW, int BrickH, int BrickX, int BrickY, int StartX, int StartY, int PauseX, int PauseY)
            {
                BoardW = W;
                BoardH = H;
                BrickBoardW = BrickW;
                BrickBoardH = BrickH;
                BrickBoard.X = BrickX;
                BrickBoard.Y = BrickY;
                btnStart.X = StartX;
                btnStart.Y = StartY;
                btnPause.X = PauseX;
                btnPause.Y = PauseY;
            }
            public void getBoardSize(ref int W,ref int H) { W = BoardW;H = BoardH; }
            public void getBrickBoardSize(ref int W, ref int H) { W = BrickBoardW; H = BrickBoardH; }
            public Point getBrickBoardPosition() { return BrickBoard; }
            public Point getbtnStartPosition() { return btnStart; }
            public Point getbtnPausePosition() { return btnPause; }
        }

    }
}
