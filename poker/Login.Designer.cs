namespace poker
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.main_pb = new System.Windows.Forms.PictureBox();
            this.enter_btn = new System.Windows.Forms.PictureBox();
            this.exit_btn = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.main_pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enter_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exit_btn)).BeginInit();
            this.SuspendLayout();
            // 
            // main_pb
            // 
            this.main_pb.Image = ((System.Drawing.Image)(resources.GetObject("main_pb.Image")));
            this.main_pb.Location = new System.Drawing.Point(1, 34);
            this.main_pb.Name = "main_pb";
            this.main_pb.Size = new System.Drawing.Size(328, 462);
            this.main_pb.TabIndex = 0;
            this.main_pb.TabStop = false;
            // 
            // enter_btn
            // 
            this.enter_btn.Image = ((System.Drawing.Image)(resources.GetObject("enter_btn.Image")));
            this.enter_btn.Location = new System.Drawing.Point(72, 298);
            this.enter_btn.Name = "enter_btn";
            this.enter_btn.Size = new System.Drawing.Size(199, 50);
            this.enter_btn.TabIndex = 1;
            this.enter_btn.TabStop = false;
            this.enter_btn.Click += new System.EventHandler(this.enter_btn_Click);
            this.enter_btn.MouseLeave += new System.EventHandler(this.enter_btn_MouseLeave);
            this.enter_btn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.enter_btn_MouseMove);
            // 
            // exit_btn
            // 
            this.exit_btn.Image = ((System.Drawing.Image)(resources.GetObject("exit_btn.Image")));
            this.exit_btn.Location = new System.Drawing.Point(72, 354);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(197, 50);
            this.exit_btn.TabIndex = 2;
            this.exit_btn.TabStop = false;
            this.exit_btn.Click += new System.EventHandler(this.exit_btn_Click);
            this.exit_btn.MouseLeave += new System.EventHandler(this.exit_btn_MouseLeave);
            this.exit_btn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.exit_btn_MouseMove);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(334, 497);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.enter_btn);
            this.Controls.Add(this.main_pb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(334, 497);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(334, 497);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.main_pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enter_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exit_btn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox main_pb;
        private System.Windows.Forms.PictureBox enter_btn;
        private System.Windows.Forms.PictureBox exit_btn;
        private System.Windows.Forms.Timer timer1;
    }
}

