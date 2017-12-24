using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TetrisProject.Model;
using static TetrisProject.View;

namespace TetrisProject
{
    public class Controller
    {
        /*21 add in 11/28*/
        private int level = 0;
        private int score = 0;

        private Model.Brick curBrick;
        private Model.Board curBoard;
        private View GameView;
        private Form1 form;
        private Random r;

        private Color[] colorTable = new Color[]
        {
            Color.Red, Color.Purple, Color.Black, Color.Blue, Color.Yellow, Color.Green, Color.Orange
        };
        
        public Controller()//初始化
        {
        }
        public Controller(Form1 f)//初始化
        {
            form = f;
        }

        public Controller(Form1 f, View gameView)
        {
            form = f;
            GameView = gameView;
            GameView.passForm(f);
            GameView.passController(this);
        }

        public void start()//開始按鈕
        {
            //初始化一些狀態
            r = new Random();
            curBoard = new Board();
            curBoard.setBoard(10, 20);
            newBrick();
            
        }

        public void pause()//暫停按鈕
        {
            //設定一些狀態
        }

        public void resume()//繼續按鈕
        {
            //設定一些狀態
        }

        public void update()//每秒更新一次狀態
        {
            if (curBoard.isDownEmpty(curBrick))//方塊有沒有到底
            {
                BrickOneLineDown();
            }
            else
            {
                curBoard.pasteBrick(curBrick);
                curBoard.fullCheck();
                newBrick();
            }
        }
        
        //方塊移動類  先檢查有沒有到邊界 沒有就執行動作 最後檢查有沒有滿行
        //public void BrickDropDown()
        //{
        //    int newY = 0;//設定掉下後的座標
        //    Brick result = curBrick.moveDown(newY);
        //    if (MoveCheck(result))
        //    {
        //        curBrick = result;
        //    }
        //    removeFullLines();
        //    GameView.update();
        //}

        public void BrickMoveRight()
        {
            if (curBoard.isRightEmpty(curBrick))
                curBrick.moveRight();
            GameView.update(curBoard,curBrick);
        }

        public void BrickMoveLeft()
        {
            if (curBoard.isLeftEmpty(curBrick))
                curBrick.moveLeft();
            GameView.update(curBoard, curBrick);
        }

        public void BrickRotate()
        {
            if (curBoard.isRotatible(curBrick))
                curBrick.rotate();
            GameView.update(curBoard, curBrick);
        }

        private void BrickOneLineDown()//慢慢落下
        {
            Brick result = curBrick.moveDown();
            //removeFullLines();
            GameView.update(curBoard, curBrick);
        }

        private void newBrick()//產生新的方塊
        {
            int type = r.Next(1, 8);
            int color = r.Next(1, 7);
            curBrick = new Brick();
            curBrick.setShape((Tetrominoes)type, colorTable[color]);
            if (curBoard.deadCheck(curBrick))
            {
                GameView.gameOver();
            }
            else
            {
                GameView.update(curBoard, curBrick);
            }
        }

        //private void removeFullLines()//如果有行數滿時就清除
        //{
        //    //這邊放 如果有行數滿時就清除的code
        //    curBoard.fullCheck();
        //    //isFallingFinished = true;//表示方塊已經落下了
        //    GameView.update();
        //}

        //private bool MoveCheck(Brick result)//看有沒有碰到邊界
        //{
        //    return true;
        //}

        //private bool GameOverCheck()//看有沒有GameOver
        //{
        //    return true;
        //}

        /*21 add in 11/28*/
        public void changeLevel(int level)
        {
            //curBrick.changeSpeed();

        }

        public int getScore() { return score; }

    }

}

