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
        BufferedGraphics g;///
        SolidBrush b;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Focus();
            this.KeyPreview = true;
            GameView = new View();
            GameController = new Controller(this, GameView);
            int form1_width = this.Width;
            int form1_height = this.Height;
            Point btnStartPosition;
            Point btnPausePosition;
            GameView.setForm(out form1_width,out form1_height,out btnStartPosition,out btnPausePosition);
            this.Width = form1_width;
            this.Height = form1_height;
            btn_start.Location = btnStartPosition;
            btn_pause.Location = btnPausePosition;

            bufferedGraphicsContext = BufferedGraphicsManager.Current;
            g = bufferedGraphicsContext.Allocate(pictureBox1.CreateGraphics(), pictureBox1.DisplayRectangle);

        }

        private void btn_start_Click(object sender, EventArgs e)
        {

            GameView.btnStart();
            btn_pause.Enabled = true;
            gameTimer.Start();
            
        }
        
        private void btn_pause_Click(object sender, EventArgs e)
        {
            GameView.btnPause();
            btn_pause.Enabled = false;
            btn_resume.Enabled = true;
            gameTimer.Stop();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            GameController.update();
            changeScore();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameView.keyEvent(e);
            this.Focus();
        }

        private void btn_resume_Click(object sender, EventArgs e)
        {
            btn_pause.Enabled = true;
            btn_resume.Enabled = false;
            gameTimer.Start();
        }
        private void changeScore()
        {
            string result = GameView.getScore();
            scoreBox.Text = result;
        }

        private void Level_ValueChanged(object sender, EventArgs e)
        {
            GameView.changeLevel(Convert.ToInt32(Level.Value));
        }

        public void gameDraw(Board board, Brick brick, int width, int height)
        {
            int gapRaw = width / board.getRowSize();
            int gapCol = height / board.getColSize();

            g.Graphics.Clear(Color.White);

            for (int i = 0; i < brick.getWidth(); i++)
                for (int j = 0; j < brick.getHeight(); j++)
                {
                    if (brick.getShape()[j][i] == 1)
                        cubeDraw(brick.getX() + i, brick.getY() + j, gapRaw, gapCol, brick.getColor());
                }
            for (int i = 0; i < board.getColSize(); i++)
                for (int j = 0; j < board.getRowSize(); j++)
                {
                    if (board.getBoard()[i, j] == 2)
                        cubeDraw(j, i, gapRaw, gapCol, board.getBoardColor()[i, j]);
                }

            g.Render(pictureBox1.CreateGraphics());
        }

        public void cubeDraw(int x, int y, int gapR, int gapC, Color c)
        {
            b = new SolidBrush(c);
            Rectangle r = new Rectangle(x * gapR, y * gapC, gapR, gapC);
            g.Graphics.FillRectangle(b, r);
            b.Dispose();
        }

        public void gameOver()
        {
            gameTimer.Enabled = false;
            MessageBox.Show("Game Over");
        }
    }
}
