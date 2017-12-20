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
        View GameView = new View();
        private void Form1_Load(object sender, EventArgs e)
        {
            int form1_width = this.Width;
            int form1_height = this.Height;
            Point btnStartPosition;
            Point btnPausePosition;
            GameView.setForm(out form1_width,out form1_height,out btnStartPosition,out btnPausePosition);

            this.Width = form1_width;
            this.Height = form1_height;
            btn_start.Location = btnStartPosition;
            btn_pause.Location = btnPausePosition;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            GameView.btnStart();
            gameTimer.Start();
        }
        

        private void btn_pause_Click(object sender, EventArgs e)
        {
            GameView.btnPause();
            gameTimer.Stop();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            GameView.update();
            changeScore();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameView.keyEvent(e);
        }

        private void btn_resume_Click(object sender, EventArgs e)
        {
           
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
    }
}
