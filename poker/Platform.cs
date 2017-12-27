using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace poker
{
    public partial class Platform : Form
    {
        private string str = "";
        bool imageFlag = true;
        public Platform()
        {
            InitializeComponent();
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer2.IsSplitterFixed = true;

            Random ran = new Random();
            int RandKey = ran.Next(100000, 999999);
            name.Text = "游客" + RandKey.ToString();

            str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            pictureBox1.Image = Image.FromFile(str + "image\\1.jpg");
            pictureBox2.Image = Image.FromFile(str + "image\\2.jpg");
            pictureBox3.Image = Image.FromFile(str + "image\\3.jpg");
            pictureBox4.Image = Image.FromFile(str + "image\\4.jpg");
            pictureBox5.Image = Image.FromFile(str + "image\\5.jpg");
            pictureBox6.Image = Image.FromFile(str + "image\\touxiang.png");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;
            panel5.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
            panel5.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (imageFlag)
            {
                pictureBox6.Image = Image.FromFile(str + "image\\touxiang2.png");
                imageFlag = false;
            }
            else
            {
                pictureBox6.Image = Image.FromFile(str + "image\\touxiang.png");
                imageFlag = true;
            }
        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            if (imageFlag)
                pictureBox6.Image = Image.FromFile(str + "image\\touxiang-fu.png");
            else
                pictureBox6.Image = Image.FromFile(str + "image\\touxiang2-fu.png");
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            if (imageFlag)
                pictureBox6.Image = Image.FromFile(str + "image\\touxiang.png");
            else
                pictureBox6.Image = Image.FromFile(str + "image\\touxiang2.png");
            this.Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Image.FromFile(str + "image\\1-fu.jpg");
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(str + "image\\1.jpg");
            this.Cursor = Cursors.Default;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = Image.FromFile(str + "image\\2-fu.jpg");
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Image.FromFile(str + "image\\2.jpg");
            this.Cursor = Cursors.Default;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Image.FromFile(str + "image\\3-fu.jpg");
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Image.FromFile(str + "image\\3.jpg");
            this.Cursor = Cursors.Default;
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Image.FromFile(str + "image\\4-fu.jpg");
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Image.FromFile(str + "image\\4.jpg");
            this.Cursor = Cursors.Default;
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox5.Image = Image.FromFile(str + "image\\5-fu.jpg");
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = Image.FromFile(str + "image\\5.jpg");
            this.Cursor = Cursors.Default;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void EnterGame_Click(object sender, EventArgs e)
        {          
            this.Hide();
        }
    }
}
