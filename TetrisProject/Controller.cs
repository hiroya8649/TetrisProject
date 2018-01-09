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
        private int score = 0;

        private Model.Brick curBrick;
        private Model.Board curBoard;
        private View GameView;
        private Form1 form;
        private Random r;
        private Size boardSize;

        private Color[] colorTable = new Color[]
        {
            Color.Red, Color.Purple, Color.Black, Color.Blue, Color.Yellow, Color.Green, Color.Orange
        };
        
        public Controller()
        {
        }

        //讓他認識form1
        public Controller(Form1 f)
        {
            form = f;
        }

        //讓他認識form1跟view
        public Controller(Form1 f, View gameView,Size s)
        {
            form = f;
            GameView = gameView;
            GameView.passForm(f);
            GameView.passController(this);
            boardSize = s;
        }

        public void start()
        {
            r = new Random();
            curBoard = new Board();
            curBoard.setBoard(boardSize.Width, boardSize.Height);
            newBrick();
            score = 0;
        }

        //遊戲內容更新（即方塊下移）
        public void update()
        {
            if (curBoard.isDownEmpty(curBrick))//方塊下面是否為空
            {
                BrickOneLineDown();//是就往下
            }
            else
            {
                curBoard.pasteBrick(curBrick);//不是，就把他貼到棋盤上
                int tmp = curBoard.fullCheck();
                score += tmp * 100;
                newBrick();
            }
        }

        //方塊移動類，先檢查有沒有到邊界，沒有就執行動作，如果是往下，最後檢查有沒有滿行
        public void BrickDropDown()
        {
            while (curBoard.isDownEmpty(curBrick))
            {
                curBrick.moveDown();
            }
            GameView.update(curBoard, curBrick);
            curBoard.pasteBrick(curBrick);
            int tmp = curBoard.fullCheck();
            score += tmp * 100;
            newBrick();

        }

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
            curBrick.moveDown();
            GameView.update(curBoard, curBrick);
        }

        private void newBrick()//產生新的方塊
        {
            int type = r.Next(1, 8);
            int color = r.Next(1, 7);
            curBrick = new Brick();
            curBrick.setShape((Tetrominoes)type, colorTable[color]);
            GameView.update(curBoard, curBrick);
            if (curBoard.deadCheck(curBrick))//測試遊戲有沒有結束
            {
                GameView.gameOver();
            }
            else
            {
                GameView.update(curBoard, curBrick);
            }
        }

        public int getScore() { return score; }

    }

}

