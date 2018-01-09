using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TetrisProject.Model;


namespace TetrisProject
{
    public class View : Form 
    {
        //主畫面大小WH 遊戲畫面大小WH 遊戲畫面位置XY 開始按鈕位置XY 暫停按鈕位置XY
        
        //private Background bg1 = new Background(500, 500, 200, 300, 0, 0, 1, 1, 50, 50);
        private Controller GameController;
        private Form1 form;

        public View()
        {

        }
        //用來讓view知道form1
        public void passForm(Form1 f)
        {
            form = f;
        }
        //用來讓view知道controller
        public void passController(Controller  controller)
        {
            GameController = controller;
        }
        //場景繪製刷新
        public void update(Board board,Brick brick)
        {
            int w = 0, h = 0;
            w = form.getBoardSize().Width;
            h = form.getBoardSize().Height;
            form.gameDraw(board, brick, w, h);
            form.changeScore();
        }
        
        public void btnStart()
        {
            GameController.start();
        }
        
        //操控方式WASD 空白鍵
        public void keyEvent(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    GameController.BrickMoveRight();
                    break;
                case Keys.A:
                    GameController.BrickMoveLeft();
                    break;
                case Keys.Space:
                    GameController.BrickDropDown();
                    break;
                case Keys.W:
                    GameController.BrickRotate();
                    break;
                case Keys.S:
                    form.speedUp();
                    break;
            }
        }
        //讓S放開時回復正常速度
        public void keyup(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
                    form.speedDn();
                    break;

            }
        }

        public string getScore() {return GameController.getScore().ToString();}

        public void gameOver()
        {
            form.gameOver();
        }
    }
}
