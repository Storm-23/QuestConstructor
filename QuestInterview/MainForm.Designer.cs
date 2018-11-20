namespace QuestInterviewNS
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnAnswers = new System.Windows.Forms.FlowLayoutPanel();
            this.btNext = new System.Windows.Forms.Button();
            this.btFinish = new System.Windows.Forms.Button();
            this.btBack = new System.Windows.Forms.Button();
            this.pnAnswers.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnAnswers
            // 
            this.pnAnswers.AutoScroll = true;
            this.pnAnswers.Controls.Add(this.btBack);
            this.pnAnswers.Controls.Add(this.btNext);
            this.pnAnswers.Controls.Add(this.btFinish);
            this.pnAnswers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnAnswers.Location = new System.Drawing.Point(0, 0);
            this.pnAnswers.Name = "pnAnswers";
            this.pnAnswers.Size = new System.Drawing.Size(612, 471);
            this.pnAnswers.TabIndex = 0;
            // 
            // btNext
            // 
            this.btNext.Image = global::QuestInterviewNS.Properties.Resources.control_right;
            this.btNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btNext.Location = new System.Drawing.Point(234, 3);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(225, 32);
            this.btNext.TabIndex = 0;
            this.btNext.Text = "Далее";
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btFinish
            // 
            this.btFinish.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btFinish.Location = new System.Drawing.Point(3, 41);
            this.btFinish.Name = "btFinish";
            this.btFinish.Size = new System.Drawing.Size(225, 32);
            this.btFinish.TabIndex = 1;
            this.btFinish.Text = "Завершить Интервью";
            this.btFinish.UseVisualStyleBackColor = true;
            this.btFinish.Click += new System.EventHandler(this.btFinish_Click);
            // 
            // btBack
            // 
            this.btBack.Image = global::QuestInterviewNS.Properties.Resources.control_left;
            this.btBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btBack.Location = new System.Drawing.Point(3, 3);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(225, 32);
            this.btBack.TabIndex = 2;
            this.btBack.Text = "Назад";
            this.btBack.UseVisualStyleBackColor = true;
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 471);
            this.Controls.Add(this.pnAnswers);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quest Interview";
            this.pnAnswers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnAnswers;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btFinish;
        private System.Windows.Forms.Button btBack;
    }
}

