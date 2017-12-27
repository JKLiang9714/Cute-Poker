using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlExs;

namespace poker
{
    public partial class Login : FormEx
    {
        string[] s = new string[4] { "bg_1", "bg_2", "bg_3", "bg_4" };
        Game_Landlord landlord = new Game_Landlord();
        public Login()
        {
            landlord.Owner = this;
            landlord.Opacity = 0;
            InitializeComponent();
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            Timer t = new Timer();
            t.Interval = 1200;
            t.Tick += new EventHandler(changeBackground);
            t.Start();
        }

        void changeBackground(object sender, EventArgs e)
        {
            int i = DateTime.Now.Second;
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            main_pb.Image = Image.FromFile(str + "image\\" + s[i % 4] + ".jpg");
        }

        public void exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enter_btn_Click(object sender, EventArgs e)
        {
            //Platform form = new Platform();
            //this.Hide();
            //form.Show();
            //Game_Landlord landlord = new Game_Landlord();
            //landlord.Owner = this;
            //landlord.Show();   
            
            landlord.Show();
            System.Timers.Timer t = new System.Timers.Timer(1000);   //实例化Timer类，设置间隔时间为10000毫秒； 
            this.Hide();
            t.Elapsed += new System.Timers.ElapsedEventHandler(ShowTime); //到达时间的时候执行事件；   
            t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件；           
        }

        void ShowTime(object sender, EventArgs e)
        {
            landlord.Opacity = 1;
        }

        private void enter_btn_MouseMove(object sender, MouseEventArgs e)
        {
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            enter_btn.Image = System.Drawing.Image.FromFile(str + @"\image\enter2.jpg");
            this.Cursor = Cursors.Hand;
        }

        private void exit_btn_MouseMove(object sender, MouseEventArgs e)
        {
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            exit_btn.Image = System.Drawing.Image.FromFile(str + @"\image\exit2.jpg");
            this.Cursor = Cursors.Hand;
        }

        private void enter_btn_MouseLeave(object sender, EventArgs e)
        {
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            enter_btn.Image = System.Drawing.Image.FromFile(str + @"\image\enter1.jpg");
            this.Cursor = Cursors.Default;
        }

        private void exit_btn_MouseLeave(object sender, EventArgs e)
        {
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            exit_btn.Image = System.Drawing.Image.FromFile(str + @"\image\exit1.jpg");
            this.Cursor = Cursors.Default;
        }

        protected override void OnClosing(CancelEventArgs e)
        {  
            this.Controls.Clear();
        }
    }
}
