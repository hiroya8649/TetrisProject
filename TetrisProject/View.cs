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
        bool isGamePause = false;

        public View()
        {

        }
        
        public void passForm(Form1 f)
        {
            form = f;
        }
        
        public void passController(Controller  controller)
        {
            GameController = controller;
        }

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

        public void btnPause()
        {
            if (!isGamePause)
                GameController.pause();
            //else
            //    GameController.resume();
        }

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
                case Keys.S:
                    GameController.BrickDropDown();
                    break;
                case Keys.Space:
                    GameController.BrickDropDown();
                    break;
                case Keys.W:
                    GameController.BrickRotate();
                    break;
            }
        }

        /*21 add function in 11/27*/

        public string getScore() {return GameController.getScore().ToString();}

        public void gameOver()
        {
            form.gameOver();
        }
        
        private void drawSquare(Graphics g, int x, int y, Tetrominoes shape)
        {
            //    Color colors[] = { new Color(0, 0, 0), new Color(204, 102, 102),
            //    new Color(102, 204, 102), new Color(102, 102, 204),
            //    new Color(204, 204, 102), new Color(204, 102, 204),
            //    new Color(102, 204, 204), new Color(218, 170, 0)
            //};


            //Color color = colors[shape.ordinal()];

            //g.setColor(color);
            //g.fillRect(x + 1, y + 1, squareWidth() - 2, squareHeight() - 2);

            //g.setColor(color.brighter());
            //g.drawLine(x, y + squareHeight() - 1, x, y);
            //g.drawLine(x, y, x + squareWidth() - 1, y);

            //g.setColor(color.darker());
            //g.drawLine(x + 1, y + squareHeight() - 1,
            //                 x + squareWidth() - 1, y + squareHeight() - 1);
            //g.drawLine(x + squareWidth() - 1, y + squareHeight() - 1,
            //                 x + squareWidth() - 1, y + 1);
        }

    }
}
