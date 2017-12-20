namespace TetrisProject
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_pause = new System.Windows.Forms.Button();
            this.Level = new System.Windows.Forms.NumericUpDown();
            this.scoreBox = new System.Windows.Forms.TextBox();
            this.btn_resume = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Level)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(356, 158);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(60, 38);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_pause
            // 
            this.btn_pause.Location = new System.Drawing.Point(356, 212);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(130, 35);
            this.btn_pause.TabIndex = 1;
            this.btn_pause.Text = "Pause";
            this.btn_pause.UseVisualStyleBackColor = true;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // Level
            // 
            this.Level.Location = new System.Drawing.Point(433, 97);
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            this.Level.Size = new System.Drawing.Size(53, 22);
            this.Level.TabIndex = 2;
            this.Level.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Level.ValueChanged += new System.EventHandler(this.Level_ValueChanged);
            // 
            // scoreBox
            // 
            this.scoreBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.scoreBox.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.scoreBox.Location = new System.Drawing.Point(356, 24);
            this.scoreBox.Multiline = true;
            this.scoreBox.Name = "scoreBox";
            this.scoreBox.ReadOnly = true;
            this.scoreBox.Size = new System.Drawing.Size(130, 34);
            this.scoreBox.TabIndex = 3;
            this.scoreBox.Text = "0";
            this.scoreBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_resume
            // 
            this.btn_resume.Location = new System.Drawing.Point(426, 158);
            this.btn_resume.Name = "btn_resume";
            this.btn_resume.Size = new System.Drawing.Size(60, 38);
            this.btn_resume.TabIndex = 4;
            this.btn_resume.Text = "Resume";
            this.btn_resume.UseVisualStyleBackColor = true;
            this.btn_resume.Click += new System.EventHandler(this.btn_resume_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.Location = new System.Drawing.Point(18, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(321, 510);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(352, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Level";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(504, 550);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_resume);
            this.Controls.Add(this.scoreBox);
            this.Controls.Add(this.Level);
            this.Controls.Add(this.btn_pause);
            this.Controls.Add(this.btn_start);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Level)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_pause;
        private System.Windows.Forms.NumericUpDown Level;
        private System.Windows.Forms.TextBox scoreBox;
        private System.Windows.Forms.Button btn_resume;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}

