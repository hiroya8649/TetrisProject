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
        
        private Background bg1 = new Background(500, 500, 200, 300, 0, 0, 1, 1, 50, 50);
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
            bg1.getBrickBoardSize(ref w, ref h);
            form.gameDraw(board, brick, w, h);

            //picture update
            //GameController.update();
        }

        public void setForm(out int formWidth, out int formHeight, out Point btnStartPosition, out Point btnPausePosition)
        {
            formWidth = 0;
            formHeight = 0;
            bg1.getBoardSize(ref formWidth, ref formHeight);
            btnStartPosition = bg1.getbtnStartPosition();
            btnPausePosition = bg1.getbtnPausePosition();
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
                case Keys.Right:
                    GameController.BrickMoveRight();
                    break;
                case Keys.Left:
                    GameController.BrickMoveLeft();
                    break;
                //case Keys.Down:
                //    GameController.BrickDropDown();
                //    break;
                case Keys.Z:
                    GameController.BrickRotate();
                    break;
            }
        }

        /*21 add function in 11/27*/
        public void btnResume() { }

        public string getScore() {return GameController.getScore().ToString();}

        public void changeLevel(int level)
        {
            GameController.changeLevel(level);
        }

        public void gameOver()
        {

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
