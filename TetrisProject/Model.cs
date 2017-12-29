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
            private Color pieceColor;
            private int width, height, angle, x, y;
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
            public int[][] getShape() { return shapeNow; }
            public Tetrominoes getShapeType() { return pieceShape; }
            public Color getColor() { return pieceColor; }

            public Brick()
            {
                width = 0; height = 0; angle = 0;
                x = 5; y = 0;
                pieceShape = Tetrominoes.NoShape;
            }

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

            public void setShape(Tetrominoes shape, Color color)
            {
                pieceShape = shape;
                pieceColor = color;
                width = lenTable[(int)shape][0];
                height = lenTable[(int)shape][1];
                shapeNow = shapeTable[(int)shape][angle];
            }

            public void setX(int xIn) { x = xIn; }

            public void setY(int yIn) { y = yIn; }

            public int getShape_minY()//找出最低的Y
            {
                return 0;
            }
            public Brick rotate()//將方塊右轉 回傳結果
            {
                int tmp = width;
                width = height;
                height = tmp;

                angle += 1;
                if (angle > 3) angle = 0;
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

            public void changeSpeed() { }//變更level
        }

        public class Board
        {
            private int[,] boardTable;
            private Color[,] boardColor;
            private int gapRow, gapCol;
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

            public bool isLeftEmpty(Brick b)
            {
                if (b.getX() == 0)
                    return false;
                for (int i = 0; i < b.getWidth(); i++)
                {
                    for (int j = 0; j < b.getHeight(); j++)
                    {
                        if (b.getShape()[j][i] == 1)
                        {
                            if (boardTable[b.getY() + j, b.getX() + i - 1] == 2)
                                return false;
                            else
                                break;
                        }
                    }
                }
                return true;
            }

            public bool isRightEmpty(Brick b)
            {
                if (b.getX()+b.getWidth() == rowSize)
                    return false;
                for (int i = b.getWidth() - 1 ; i >=0 ; i-- )
                {
                    for (int j = b.getHeight() - 1; j >=0 ; j--)
                    {
                        if (b.getShape()[j][i] == 1)
                        {
                            if (boardTable[b.getY() + j, b.getX() + i + 1] == 2)
                                return false;
                            else
                                break;
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

            public bool fullCheck()//若有消掉任何一列會回傳true
            {
                bool flag = false;
                for (int i=0;i<colSize;i++)
                    for(int j=0;j<rowSize;j++)
                    {
                        if (boardTable[i, j] != 2)
                            break;
                        else
                        {
                            if (j == rowSize - 1)
                            {
                                flag = true;
                                for (int k = i; k > 0; k--)
                                    for (int l = 0; l < rowSize; l++)
                                    {
                                        boardTable[k, l] = boardTable[k - 1, l];
                                        boardColor[k, l] = boardColor[k - 1, l];
                                    }
                                for (int l = 0; l < rowSize; l++)
                                {
                                    boardTable[0, l] = 0;
                                    boardColor[0, l] = Color.White;
                                }
                            }
                        }

                    }

                return flag;
            }
            
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

            public Background(int W, int H, int BrickW, int BrickH, int BrickX, int BrickY, int StartX, int StartY, int PauseX, int PauseY)
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
            public void getBoardSize(ref int W, ref int H) { W = BoardW; H = BoardH; }
            public void getBrickBoardSize(ref int W, ref int H) { W = BrickBoardW; H = BrickBoardH; }
            public Point getBrickBoardPosition() { return BrickBoard; }
            public Point getbtnStartPosition() { return btnStart; }
            public Point getbtnPausePosition() { return btnPause; }
        }

    }
}
