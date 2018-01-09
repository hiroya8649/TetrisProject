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
    //遊戲寫法
    //整個PictureBox內的圖片分為兩個部份
    //第一個是正在移動的那個方塊（brick）
    //第二個是board，是一個x*y的陣列（就是棋盤大小）
    //用來紀錄哪格上面已經被方塊佔據
    public class Model
    {
        //Brick 現正移動中的方塊
        public class Brick
        {
            private Tetrominoes pieceShape;
            private Color pieceColor;
            private int width, height, angle, x, y;
            //每種方塊的長度，順序同Tetrominoes
            static int[][] lenTable = new int[][]
            {
                new int[]{ 2,2 },
                new int[]{ 2,3 },
                new int[]{ 2,3 },
                new int[]{ 1,4 },
                new int[]{ 2,3 },
                new int[]{ 2,2 },
                new int[]{ 2,3 },
                new int[]{ 2,3 }
            };
            //每個方塊他在不同角度下的長相
            //例如Z的第一行
            //new int[][]{ new int[]{ 0,1 }, new int[] { 1,1 }, new int[] { 1,0 } },
            //實際上長
            //01
            //11
            //10
            static int[][][][] shapeTable = new int[][][][]
            {
               new int[][][]{//No
                   new int[][]{ new int[]{ 0,0 }, new int[] { 0,0 } },
                   new int[][]{ new int[]{ 0,0 }, new int[] { 0,0 } },
                   new int[][]{ new int[]{ 0,0 }, new int[] { 0,0 } },
                   new int[][]{ new int[]{ 0,0 }, new int[] { 0,0 } }
               },
               new int[][][]{//Z
                   new int[][]{ new int[]{ 0,1 }, new int[] { 1,1 }, new int[] { 1,0 } },
                   new int[][]{ new int[]{ 1,1,0 }, new int[] { 0,1,1 } },
                   new int[][]{ new int[]{ 0,1 }, new int[] { 1,1 }, new int[] { 1,0 } },
                   new int[][]{ new int[]{ 1,1,0 }, new int[] { 0,1,1 } }
               },
               new int[][][]{//S
                   new int[][]{ new int[]{ 1,0 }, new int[] { 1,1 }, new int[] { 0,1 } },
                   new int[][]{ new int[]{ 0,1,1 }, new int[] { 1,1,0 } },
                   new int[][]{ new int[]{ 1,0 }, new int[] { 1,1 }, new int[] { 0,1 } },
                   new int[][]{ new int[]{ 0,1,1 }, new int[] { 1,1,0 } }
               },
               new int[][][]{//I
                   new int[][]{ new int[]{ 1 }, new int[] { 1 }, new int[] { 1 }, new int[] { 1 } },
                   new int[][]{ new int[]{ 1,1,1,1 }},
                   new int[][]{ new int[]{ 1 }, new int[] { 1 }, new int[] { 1 }, new int[] { 1 } },
                   new int[][]{ new int[]{ 1,1,1,1 }}
               },
               new int[][][]{//T
                   new int[][]{ new int[]{ 1,0 }, new int[] { 1,1 }, new int[] { 1,0 } },
                   new int[][]{ new int[]{ 1,1,1 }, new int[] { 0,1,0 } },
                   new int[][]{ new int[]{ 0,1 }, new int[] { 1,1 }, new int[] { 0,1 } },
                   new int[][]{ new int[]{ 0,1,0 }, new int[] { 1,1,1 } }
               },
               new int[][][]{//SQ
                   new int[][]{ new int[]{ 1,1 }, new int[] { 1,1 } },
                   new int[][]{ new int[]{ 1,1 }, new int[] { 1,1 } },
                   new int[][]{ new int[]{ 1,1 }, new int[] { 1,1 } },
                   new int[][]{ new int[]{ 1,1 }, new int[] { 1,1 } }
               },
               new int[][][]{//L
                   new int[][]{ new int[]{ 1,0 }, new int[] { 1,0 }, new int[] { 1,1 } },
                   new int[][]{ new int[]{ 1,1,1 }, new int[] { 1,0,0 } },
                   new int[][]{ new int[]{ 1,1 }, new int[] { 0,1 }, new int[] { 0,1 } },
                   new int[][]{ new int[]{ 0,0,1 }, new int[] { 1,1,1 } }
               },
               new int[][][]{//ML
                   new int[][]{ new int[]{ 0,1 }, new int[] { 0,1 }, new int[] { 1,1 } },
                   new int[][]{ new int[]{ 1,0,0 }, new int[] { 1,1,1 } },
                   new int[][]{ new int[]{ 1,1 }, new int[] { 1,0 }, new int[] { 1,0 } },
                   new int[][]{ new int[]{ 1,1,1 }, new int[] { 0,0,1 } }
               }
            };
            private int[][] shapeNow;

            public int getWidth() { return width; }
            public int getHeight() { return height; }
            public int getX() { return x; }
            public int getY() { return y; }
            public int getAngle() { return angle; }
            //getshape是拿到方塊實際的長相（01的陣列）
            public int[][] getShape() { return shapeNow; }
            public Tetrominoes getShapeType() { return pieceShape; }
            public Color getColor() { return pieceColor; }

            public Brick()
            {
                width = 0; height = 0; angle = 0;
                x = 5; y = 0;
                pieceShape = Tetrominoes.NoShape;
            }

            //用Brick來初始化一個Brick
            //主要用在方塊向左、右、下移動時，先創一個新方塊偵測有沒有重疊
            public Brick(Brick b)
            {
                pieceShape = b.getShapeType();
                pieceColor = b.pieceColor;
                this.setX(b.getX()); this.setY(b.getY());
                angle = b.getAngle();
                width = b.getWidth();
                height = b.getHeight();
                shapeNow = b.getShape();
            }
            //設定方塊的形狀
            public void setShape(Tetrominoes shape, Color color)
            {
                pieceShape = shape;
                pieceColor = color;
                width = lenTable[(int)shape][0];
                height = lenTable[(int)shape][1];
                shapeNow = shapeTable[(int)shape][angle];
            }
            //設定方塊座標
            public void setX(int xIn) { x = xIn; }

            public void setY(int yIn) { y = yIn; }

            //將方塊旋轉 回傳結果
            public Brick rotate()
            {
                int tmp = width;
                width = height;
                height = tmp;

                angle += 1;//因為每個角度都寫在陣列了
                if (angle > 3) angle = 0;//所以每一次旋轉只要去陣列抓他現在長怎樣就好
                shapeNow = shapeTable[(int)pieceShape][angle];
                return this;
            }
            public Brick moveRight()//將方塊右移 回傳結果 
            {
                x += 1;
                return this;
            }
            public Brick moveLeft()//將方塊左移 回傳結果 
            {
                x -= 1;
                return this;
            }
            public Brick moveDown()//將方塊往下 回傳結果 
            {
                y += 1;
                return this;
            }
        }

        //遊戲棋盤
        public class Board
        {
            //棋盤每一格的資訊，他是一格一格看的，沒有Brick的概念
            //0是空的，2是有方塊存在
            private int[,] boardTable;
            //每一格該有的顏色，若該格沒方塊就是白色
            private Color[,] boardColor;
            //棋盤大小
            private int rowSize,colSize;

            public int[,] getBoard() { return boardTable; }
            public Color[,] getBoardColor() { return boardColor; }
            public int getRowSize() { return rowSize; }
            public int getColSize() { return colSize; }

            public Board()
            {

            }

            public void setBoard( int row, int col)
            {
                rowSize = row;
                colSize = col;
                boardTable = new int[col, row];
                boardColor = new Color[col, row];
                for (int i = 0; i < col; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        boardTable[i, j] = 0;
                        boardColor[i, j] = Color.White;
                    }
                }
            }

            //某一個方塊結束移動（到底）的時候，將他的資訊貼到棋盤上的對應格子
            public void pasteBrick(Brick b)
            {
                for (int i = 0; i < b.getWidth(); i++)
                {
                    for (int j = 0; j < b.getHeight(); j++)
                    {
                        if (b.getShape()[j][i] == 1)
                        {
                            boardTable[b.getY() + j, b.getX() + i] = 2;
                            boardColor[b.getY() + j, b.getX() + i] = b.getColor();
                        }
                    }
                }
            }
            //判斷方塊b左、右、下、旋轉後的位置是否皆為空（可否左移、右移、下移、旋轉）
            //直接用方塊b新增一個test用方塊，若他跟棋盤上的方塊有重疊則不可移
            public bool isLeftEmpty(Brick b)
            {
                if (b.getX() == 0)
                    return false;
                Brick testB = new Brick(b);
                testB.moveLeft();
                for (int i = 0; i < testB.getWidth(); i++)
                {
                    for (int j = 0; j < testB.getHeight(); j++)
                    {
                        if (testB.getShape()[j][i] == 1)
                        {
                            if (boardTable[testB.getY() + j, testB.getX() + i] == 2)
                                return false;
                        }
                    }
                }
                return true;
            }

            public bool isRightEmpty(Brick b)
            {
                if (b.getX()+b.getWidth() == rowSize)
                    return false;
                Brick testB = new Brick(b);
                testB.moveRight();
                for (int i = 0; i < testB.getWidth(); i++)
                {
                    for (int j = 0; j < testB.getHeight(); j++)
                    {
                        if (testB.getShape()[j][i] == 1)
                        {
                            if (boardTable[testB.getY() + j, testB.getX() + i] == 2)
                                return false;
                        }
                    }
                }
                return true;
            }

            public bool isDownEmpty(Brick b)
            {
                if (b.getY() + b.getHeight() == colSize)
                    return false;
                for (int i = 0; i < b.getWidth(); i++)
                {
                    for (int j = b.getHeight() - 1 ; j >= 0 ; j--)
                    {
                        if (b.getShape()[j][i] == 1)
                        {
                            if (boardTable[b.getY() + j + 1, b.getX() + i] == 2)
                                return false;
                            else
                                break;
                        }
                    }
                }
                return true;
            }
            
            public bool isRotatible(Brick b)
            {
                if (b.getX() + b.getHeight() > rowSize)
                    return false;
                Brick testB = new Brick(b);
                testB.rotate();
                for (int i = 0; i < testB.getWidth(); i++)
                {
                    for (int j = 0; j < testB.getHeight(); j++)
                    {
                        if (testB.getShape()[j][i] == 1)
                        {
                            if (boardTable[testB.getY() + j, testB.getX() + i] == 2)
                                return false;
                        }
                    }
                }
                return true;
            }

            //是否有滿列
            //會回傳總共滿了幾列
            public int fullCheck()
            {
                int flag = 0;
                for (int i=0;i<colSize;i++)
                    for(int j=0;j<rowSize;j++)
                    {
                        if (boardTable[i, j] != 2)
                            break;
                        else
                        {
                            if (j == rowSize - 1)
                            {
                                flag += 1;
                                for (int k = i; k > 0; k--)
                                    for (int l = 0; l < rowSize; l++)//把滿列的那一列上方全部往下拉，除了最上面那一列（因為他沒有再上一列所以要特別處理）
                                    {
                                        boardTable[k, l] = boardTable[k - 1, l];
                                        boardColor[k, l] = boardColor[k - 1, l];
                                    }
                                for (int l = 0; l < rowSize; l++)//最上面那一列直接清空
                                {
                                    boardTable[0, l] = 0;
                                    boardColor[0, l] = Color.White;
                                }
                            }
                        }

                    }

                return flag;
            }
            
            //是否遊戲結束了，當新Brick一產生的時候就已經重疊代表遊戲結束
            public bool deadCheck(Brick next)
            {
                for (int i = 0; i < next.getWidth(); i++)
                {
                    for (int j = 0; j < next.getHeight(); j++)
                    {
                        if (next.getShape()[j][i] == 1)
                        {
                            if (boardTable[next.getY() + j, next.getX() + i] == 2)
                                return true;
                        }
                    }
                }
                return false;
            }
        }

        //public class Background
        //{
        //    private int BoardW = 0;//主畫面的大小
        //    private int BoardH = 0;
        //    private int BrickBoardW = 0;//遊戲畫面的大小
        //    private int BrickBoardH = 0;
        //    private Point BrickBoard = new Point(0, 0);//遊戲畫面的位置
        //    private Point btnStart = new Point(0, 0);//開始按鈕的位置
        //    private Point btnPause = new Point(0, 0);//暫停按鈕的位置
        //    public Background()
        //    {
        //        BoardW = 0;
        //        BoardH = 0;
        //        BrickBoardW = 0;
        //        BrickBoardH = 0;
        //        BrickBoard.X = 0;
        //        BrickBoard.Y = 0;
        //        btnStart.X = 0;
        //        btnStart.Y = 0;
        //        btnPause.X = 0;
        //        btnPause.Y = 0;
        //    }

        //    public Background(int W, int H, int BrickW, int BrickH, int BrickX, int BrickY, int StartX, int StartY, int PauseX, int PauseY)
        //    {
        //        BoardW = W;
        //        BoardH = H;
        //        BrickBoardW = BrickW;
        //        BrickBoardH = BrickH;
        //        BrickBoard.X = BrickX;
        //        BrickBoard.Y = BrickY;
        //        btnStart.X = StartX;
        //        btnStart.Y = StartY;
        //        btnPause.X = PauseX;
        //        btnPause.Y = PauseY;
        //    }
        //    public void getBoardSize(ref int W, ref int H) { W = BoardW; H = BoardH; }
        //    public void getBrickBoardSize(ref int W, ref int H) { W = BrickBoardW; H = BrickBoardH; }
        //    public Point getBrickBoardPosition() { return BrickBoard; }
        //    public Point getbtnStartPosition() { return btnStart; }
        //    public Point getbtnPausePosition() { return btnPause; }
        //}

    }
}
