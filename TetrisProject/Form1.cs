using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TetrisProject.Model;

namespace TetrisProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        View GameView;
        Controller GameController;
        BufferedGraphicsContext bufferedGraphicsContext;//buffer
        BufferedGraphics g;//
        SolidBrush b;
        Pen pen = new Pen(Color.Black, 1);//格線
        int speed = 1;//若S鍵按下則加速

        private void Form1_Load(object sender, EventArgs e)
        {
            //這行改遊戲界面的格數，前者為row後者為col
            Size boardSize = new Size(10, 25);
            //四種不同界面
           // setForm(new Size(700, 550), new Size(350, 450), new Point(25, 30), new Size(80, 48), new Point(475, 198), new Size(173, 44), new Point(475, 265), new Size(80, 48), new Point(568, 198), new Point(469, 119), new Point(577, 121), new Point(475, 30));
           setForm(new Size(700, 550), new Size(350, 450), new Point(275, 30), new Size(80, 48), new Point(25, 198), new Size(173, 44), new Point(25, 265), new Size(80, 48), new Point(118, 198), new Point(19, 119), new Point(127, 121), new Point(25, 30));
            //setForm(new Size(450, 700), new Size(350, 450), new Point(36, 89), new Size(147, 48), new Point(239, 20), new Size(80, 44), new Point(304, 562), new Size(80, 44), new Point(209, 562), new Point(42, 569), new Point(123, 573), new Point(36, 26));
            //setForm(new Size(450, 700), new Size(350, 450), new Point(30, 156), new Size(147, 48), new Point(36, 33), new Size(80, 44), new Point(298, 33), new Size(80, 44), new Point(210, 33), new Point(29, 98), new Point(112, 102), new Point(206, 89));
            this.KeyPreview = true;
            GameView = new View();
            GameController = new Controller(this, GameView, boardSize);
            bufferedGraphicsContext = BufferedGraphicsManager.Current;
            g = bufferedGraphicsContext.Allocate(pictureBox1.CreateGraphics(), pictureBox1.DisplayRectangle);

        }

        //設定界面
        public void setForm(Size formSize, Size boardSize, Point boardLoc,
            Size startSize, Point startLoc, Size pauseSize, Point pauseLoc,
            Size resumeSize, Point resumeLoc,
            Point labelLoc, Point levelLoc, Point scoreLoc)
        {
            this.Size = formSize;
            pictureBox1.Size = boardSize;
            pictureBox1.Location = boardLoc;
            btn_start.Size = startSize;
            btn_start.Location = startLoc;
            btn_pause.Size = pauseSize;
            btn_pause.Location = pauseLoc;
            btn_resume.Size = resumeSize;
            btn_resume.Location = resumeLoc;
            label1.Location = labelLoc;
            Level.Location = levelLoc;
            scoreBox.Location = scoreLoc;
        }

        //遊戲開始及難度
        private void btn_start_Click(object sender, EventArgs e)
        {
            GameView.btnStart();
            btn_pause.Enabled = true;
            btn_start.Enabled = false;
            gameTimer.Start();
            speed = 1;
            gameTimer.Interval = 600 / (int)Level.Value / speed;//調整讓遊戲下降的間隔，所以level越大速度會越快，speed是用來S鍵加速
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            btn_pause.Enabled = false;
            btn_resume.Enabled = true;
            gameTimer.Stop();
        }

        //每次計時器時間到就讓遊戲內容更新＝方塊下移（非View更新）
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            GameController.update();
            changeScore();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameTimer.Enabled == true)
                GameView.keyEvent(e);

            //Application.OpenForms[this.Name].Focus();
            //this.WindowState = FormWindowState.Normal;
            //this.BringToFront();

            //this.Activate();
        }

        private void btn_resume_Click(object sender, EventArgs e)
        {
            btn_pause.Enabled = true;
            btn_resume.Enabled = false;
            gameTimer.Start();
        }

        public Size getBoardSize()
        {
            return pictureBox1.Size;
        }

        public void changeScore()
        {
            string result = GameView.getScore();
            scoreBox.Text = result;
        }

        private void Level_ValueChanged(object sender, EventArgs e)
        {
            gameTimer.Interval = 600 / (int)Level.Value / speed;
            textBox1.Focus();
        }

        //繪製用
        public void gameDraw(Board board, Brick brick, int width, int height)
        {
            //棋盤大小除以格數等於每格畫多大
            int gapRaw = width / board.getRowSize();
            int gapCol = height / board.getColSize();

            g.Graphics.Clear(Color.White);

            //先畫棋盤每格每格畫
            for (int i = 0; i < board.getColSize(); i++)
                for (int j = 0; j < board.getRowSize(); j++)
                {
                    cubeDraw(j, i, gapRaw, gapCol, board.getBoardColor()[i, j]);
                }
            //再畫現在在移動的那顆方塊
            for (int i = 0; i < brick.getWidth(); i++)
                for (int j = 0; j < brick.getHeight(); j++)
                {
                    if (brick.getShape()[j][i] == 1)
                        cubeDraw(brick.getX() + i, brick.getY() + j, gapRaw, gapCol, brick.getColor());
                }
            g.Render(pictureBox1.CreateGraphics());
        }
        //第幾格（x,y）那格的長寬（gapR,GapC）及顏色c
        public void cubeDraw(int x, int y, int gapR, int gapC, Color c)
        {
            b = new SolidBrush(c);
            Rectangle r = new Rectangle(x * gapR, y * gapC, gapR, gapC);
            g.Graphics.FillRectangle(b, r);
            g.Graphics.DrawRectangle(pen, r);//棋盤的黑線，其實也是一格一格畫
            b.Dispose();
        }

        public void gameOver()
        {
            gameTimer.Enabled = false;
            btn_pause.Enabled = false;
            btn_start.Enabled = true;
            MessageBox.Show("Game Over");
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (gameTimer.Enabled == true)
                GameView.keyup(e);
        }

        //S鍵按下和放開的加速用
        public void speedUp() { speed = 10; gameTimer.Interval = 600 / (int)Level.Value / speed; }
        public void speedDn() { speed = 1; gameTimer.Interval = 600 / (int)Level.Value / speed; }
    }
}
