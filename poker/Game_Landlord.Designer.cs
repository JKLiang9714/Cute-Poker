using System.Drawing;
using System.Windows.Forms;

namespace poker
{
    partial class Game_Landlord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game_Landlord));
            this.undercard = new System.Windows.Forms.Panel();
            this.dipai = new System.Windows.Forms.Label();
            this.down = new System.Windows.Forms.PictureBox();
            this.right = new System.Windows.Forms.PictureBox();
            this.left = new System.Windows.Forms.PictureBox();
            this.up = new System.Windows.Forms.PictureBox();
            this.order = new System.Windows.Forms.PictureBox();
            this.robot = new System.Windows.Forms.PictureBox();
            this.upmes = new System.Windows.Forms.Label();
            this.leftmes = new System.Windows.Forms.Label();
            this.downmes = new System.Windows.Forms.Label();
            this.rightmes = new System.Windows.Forms.Label();
            this.refuse_button = new System.Windows.Forms.Label();
            this.outcard_button = new System.Windows.Forms.Label();
            this.hint_button = new System.Windows.Forms.Label();
            this.chat_panel = new System.Windows.Forms.Panel();
            this.chatlist = new System.Windows.Forms.Label();
            this.sent_button = new System.Windows.Forms.Label();
            this.chat_button = new System.Windows.Forms.Label();
            this.chat = new System.Windows.Forms.TextBox();
            this.start_button = new System.Windows.Forms.Label();
            this.onescore_button = new System.Windows.Forms.Label();
            this.twoscore_button = new System.Windows.Forms.Label();
            this.threescore_button = new System.Windows.Forms.Label();
            this.noscore_button = new System.Windows.Forms.Label();
            this.twoscore_pic = new System.Windows.Forms.Label();
            this.left_land = new System.Windows.Forms.Label();
            this.up_land = new System.Windows.Forms.Label();
            this.down_land = new System.Windows.Forms.Label();
            this.right_land = new System.Windows.Forms.Label();
            this.breakrule = new System.Windows.Forms.Label();
            this.down_clock = new System.Windows.Forms.Label();
            this.right_clock = new System.Windows.Forms.Label();
            this.up_clock = new System.Windows.Forms.Label();
            this.left_clock = new System.Windows.Forms.Label();
            this.up_card_mount = new System.Windows.Forms.Label();
            this.left_card_mount = new System.Windows.Forms.Label();
            this.right_card_mount = new System.Windows.Forms.Label();
            this.down_card_mount = new System.Windows.Forms.Label();
            this.background_music_btn = new System.Windows.Forms.Label();
            this.kuang = new System.Windows.Forms.PictureBox();
            this.score_button = new System.Windows.Forms.Label();
            this.score_panel = new System.Windows.Forms.Panel();
            this.backgound_music = new AxWMPLib.AxWindowsMediaPlayer();
            this.sound_effect_btn = new System.Windows.Forms.Label();
            this.undercard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.up)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.order)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.robot)).BeginInit();
            this.chat_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kuang)).BeginInit();
            this.score_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backgound_music)).BeginInit();
            this.SuspendLayout();
            // 
            // undercard
            // 
            this.undercard.BackColor = System.Drawing.Color.Transparent;
            this.undercard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("undercard.BackgroundImage")));
            this.undercard.Controls.Add(this.dipai);
            this.undercard.ForeColor = System.Drawing.Color.Navy;
            this.undercard.Location = new System.Drawing.Point(0, 28);
            this.undercard.Name = "undercard";
            this.undercard.Size = new System.Drawing.Size(184, 132);
            this.undercard.TabIndex = 0;
            // 
            // dipai
            // 
            this.dipai.ForeColor = System.Drawing.Color.Transparent;
            this.dipai.Image = ((System.Drawing.Image)(resources.GetObject("dipai.Image")));
            this.dipai.Location = new System.Drawing.Point(5, 4);
            this.dipai.MaximumSize = new System.Drawing.Size(200, 40);
            this.dipai.Name = "dipai";
            this.dipai.Size = new System.Drawing.Size(176, 30);
            this.dipai.TabIndex = 1;
            this.dipai.Text = "                                 ";
            // 
            // down
            // 
            this.down.BackColor = System.Drawing.Color.Transparent;
            this.down.Cursor = System.Windows.Forms.Cursors.Hand;
            this.down.Image = global::poker.Properties.Resources.man4;
            this.down.Location = new System.Drawing.Point(102, 466);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(100, 150);
            this.down.TabIndex = 1;
            this.down.TabStop = false;
            this.down.Click += new System.EventHandler(this.down_Click);
            // 
            // right
            // 
            this.right.BackColor = System.Drawing.Color.Transparent;
            this.right.Cursor = System.Windows.Forms.Cursors.Hand;
            this.right.Image = global::poker.Properties.Resources.man3;
            this.right.Location = new System.Drawing.Point(853, 223);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(100, 150);
            this.right.TabIndex = 2;
            this.right.TabStop = false;
            this.right.Click += new System.EventHandler(this.right_Click);
            // 
            // left
            // 
            this.left.BackColor = System.Drawing.Color.Transparent;
            this.left.Cursor = System.Windows.Forms.Cursors.Hand;
            this.left.Image = global::poker.Properties.Resources.man2;
            this.left.Location = new System.Drawing.Point(9, 223);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(100, 150);
            this.left.TabIndex = 3;
            this.left.TabStop = false;
            this.left.Click += new System.EventHandler(this.left_Click);
            // 
            // up
            // 
            this.up.BackColor = System.Drawing.Color.Transparent;
            this.up.Cursor = System.Windows.Forms.Cursors.Hand;
            this.up.Image = global::poker.Properties.Resources.man1;
            this.up.Location = new System.Drawing.Point(318, 38);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(100, 150);
            this.up.TabIndex = 4;
            this.up.TabStop = false;
            this.up.Click += new System.EventHandler(this.up_Click);
            // 
            // order
            // 
            this.order.BackColor = System.Drawing.Color.Transparent;
            this.order.Cursor = System.Windows.Forms.Cursors.Hand;
            this.order.Image = global::poker.Properties.Resources.order;
            this.order.Location = new System.Drawing.Point(15, 506);
            this.order.Name = "order";
            this.order.Size = new System.Drawing.Size(40, 40);
            this.order.TabIndex = 5;
            this.order.TabStop = false;
            this.order.Click += new System.EventHandler(this.order_Click);
            // 
            // robot
            // 
            this.robot.BackColor = System.Drawing.Color.Transparent;
            this.robot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.robot.Image = global::poker.Properties.Resources.robot;
            this.robot.Location = new System.Drawing.Point(20, 470);
            this.robot.Name = "robot";
            this.robot.Size = new System.Drawing.Size(32, 32);
            this.robot.TabIndex = 6;
            this.robot.TabStop = false;
            this.robot.Click += new System.EventHandler(this.robot_Click);
            // 
            // upmes
            // 
            this.upmes.AutoSize = true;
            this.upmes.BackColor = System.Drawing.Color.Transparent;
            this.upmes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.upmes.ForeColor = System.Drawing.SystemColors.Control;
            this.upmes.Location = new System.Drawing.Point(246, 119);
            this.upmes.Name = "upmes";
            this.upmes.Size = new System.Drawing.Size(0, 12);
            this.upmes.TabIndex = 7;
            this.upmes.Click += new System.EventHandler(this.upmes_Click);
            // 
            // leftmes
            // 
            this.leftmes.AutoSize = true;
            this.leftmes.BackColor = System.Drawing.Color.Transparent;
            this.leftmes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.leftmes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.leftmes.Location = new System.Drawing.Point(20, 376);
            this.leftmes.Name = "leftmes";
            this.leftmes.Size = new System.Drawing.Size(0, 12);
            this.leftmes.TabIndex = 8;
            this.leftmes.Click += new System.EventHandler(this.leftmes_Click);
            // 
            // downmes
            // 
            this.downmes.AutoSize = true;
            this.downmes.BackColor = System.Drawing.Color.Transparent;
            this.downmes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downmes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.downmes.Location = new System.Drawing.Point(116, 618);
            this.downmes.Name = "downmes";
            this.downmes.Size = new System.Drawing.Size(0, 12);
            this.downmes.TabIndex = 9;
            this.downmes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.downmes_MouseClick);
            // 
            // rightmes
            // 
            this.rightmes.AutoSize = true;
            this.rightmes.BackColor = System.Drawing.Color.Transparent;
            this.rightmes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rightmes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.rightmes.Location = new System.Drawing.Point(864, 376);
            this.rightmes.Name = "rightmes";
            this.rightmes.Size = new System.Drawing.Size(0, 12);
            this.rightmes.TabIndex = 10;
            this.rightmes.Click += new System.EventHandler(this.rightmes_Click);
            // 
            // refuse_button
            // 
            this.refuse_button.AllowDrop = true;
            this.refuse_button.BackColor = System.Drawing.Color.Transparent;
            this.refuse_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refuse_button.Image = global::poker.Properties.Resources.refuse;
            this.refuse_button.Location = new System.Drawing.Point(438, 477);
            this.refuse_button.Name = "refuse_button";
            this.refuse_button.Size = new System.Drawing.Size(60, 25);
            this.refuse_button.TabIndex = 11;
            this.refuse_button.Visible = false;
            this.refuse_button.Click += new System.EventHandler(this.refuse_button_Click);
            // 
            // outcard_button
            // 
            this.outcard_button.BackColor = System.Drawing.Color.Transparent;
            this.outcard_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.outcard_button.Image = global::poker.Properties.Resources.outcard_button;
            this.outcard_button.Location = new System.Drawing.Point(338, 477);
            this.outcard_button.Name = "outcard_button";
            this.outcard_button.Size = new System.Drawing.Size(60, 25);
            this.outcard_button.TabIndex = 12;
            this.outcard_button.Visible = false;
            this.outcard_button.Click += new System.EventHandler(this.outcard_Click);
            // 
            // hint_button
            // 
            this.hint_button.BackColor = System.Drawing.Color.Transparent;
            this.hint_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hint_button.Image = global::poker.Properties.Resources.hint_button;
            this.hint_button.Location = new System.Drawing.Point(538, 477);
            this.hint_button.Name = "hint_button";
            this.hint_button.Size = new System.Drawing.Size(60, 25);
            this.hint_button.TabIndex = 13;
            this.hint_button.Visible = false;
            this.hint_button.Click += new System.EventHandler(this.hint_button_Click);
            // 
            // chat_panel
            // 
            this.chat_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chat_panel.BackColor = System.Drawing.Color.Transparent;
            this.chat_panel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chat_panel.BackgroundImage")));
            this.chat_panel.Controls.Add(this.chatlist);
            this.chat_panel.Controls.Add(this.sent_button);
            this.chat_panel.Controls.Add(this.chat_button);
            this.chat_panel.Controls.Add(this.chat);
            this.chat_panel.Location = new System.Drawing.Point(761, 416);
            this.chat_panel.Name = "chat_panel";
            this.chat_panel.Size = new System.Drawing.Size(200, 265);
            this.chat_panel.TabIndex = 14;
            // 
            // chatlist
            // 
            this.chatlist.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chatlist.ForeColor = System.Drawing.Color.White;
            this.chatlist.Location = new System.Drawing.Point(5, 27);
            this.chatlist.Name = "chatlist";
            this.chatlist.Size = new System.Drawing.Size(190, 180);
            this.chatlist.TabIndex = 18;
            // 
            // sent_button
            // 
            this.sent_button.Image = ((System.Drawing.Image)(resources.GetObject("sent_button.Image")));
            this.sent_button.Location = new System.Drawing.Point(142, 204);
            this.sent_button.Name = "sent_button";
            this.sent_button.Size = new System.Drawing.Size(52, 28);
            this.sent_button.TabIndex = 17;
            this.sent_button.Text = " ";
            this.sent_button.Click += new System.EventHandler(this.label1_Click);
            // 
            // chat_button
            // 
            this.chat_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chat_button.Image = global::poker.Properties.Resources.chat_button;
            this.chat_button.Location = new System.Drawing.Point(68, 0);
            this.chat_button.Name = "chat_button";
            this.chat_button.Size = new System.Drawing.Size(70, 18);
            this.chat_button.TabIndex = 16;
            this.chat_button.Text = "          ";
            this.chat_button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chat_button_MouseMove);
            // 
            // chat
            // 
            this.chat.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chat.Location = new System.Drawing.Point(5, 207);
            this.chat.MaxLength = 25;
            this.chat.Multiline = true;
            this.chat.Name = "chat";
            this.chat.Size = new System.Drawing.Size(137, 30);
            this.chat.TabIndex = 15;
            // 
            // start_button
            // 
            this.start_button.BackColor = System.Drawing.Color.Transparent;
            this.start_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.start_button.ForeColor = System.Drawing.Color.Transparent;
            this.start_button.Image = global::poker.Properties.Resources.start_button;
            this.start_button.Location = new System.Drawing.Point(428, 421);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(80, 25);
            this.start_button.TabIndex = 16;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // onescore_button
            // 
            this.onescore_button.BackColor = System.Drawing.Color.Transparent;
            this.onescore_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.onescore_button.Image = global::poker.Properties.Resources.onescore_button;
            this.onescore_button.Location = new System.Drawing.Point(325, 477);
            this.onescore_button.Name = "onescore_button";
            this.onescore_button.Size = new System.Drawing.Size(60, 25);
            this.onescore_button.TabIndex = 17;
            this.onescore_button.Text = " ";
            this.onescore_button.Click += new System.EventHandler(this.onescore_button_Click);
            // 
            // twoscore_button
            // 
            this.twoscore_button.BackColor = System.Drawing.Color.Transparent;
            this.twoscore_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.twoscore_button.Image = global::poker.Properties.Resources.twoscore_button;
            this.twoscore_button.Location = new System.Drawing.Point(401, 477);
            this.twoscore_button.Name = "twoscore_button";
            this.twoscore_button.Size = new System.Drawing.Size(60, 25);
            this.twoscore_button.TabIndex = 18;
            this.twoscore_button.Text = " ";
            this.twoscore_button.Click += new System.EventHandler(this.twoscore_button_Click);
            // 
            // threescore_button
            // 
            this.threescore_button.BackColor = System.Drawing.Color.Transparent;
            this.threescore_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.threescore_button.Image = global::poker.Properties.Resources.threescore_button;
            this.threescore_button.Location = new System.Drawing.Point(477, 477);
            this.threescore_button.Name = "threescore_button";
            this.threescore_button.Size = new System.Drawing.Size(60, 25);
            this.threescore_button.TabIndex = 19;
            this.threescore_button.Text = " ";
            this.threescore_button.Click += new System.EventHandler(this.threescore_button_Click);
            // 
            // noscore_button
            // 
            this.noscore_button.BackColor = System.Drawing.Color.Transparent;
            this.noscore_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.noscore_button.Image = global::poker.Properties.Resources.noscore_button;
            this.noscore_button.Location = new System.Drawing.Point(553, 477);
            this.noscore_button.Name = "noscore_button";
            this.noscore_button.Size = new System.Drawing.Size(60, 25);
            this.noscore_button.TabIndex = 20;
            this.noscore_button.Text = " ";
            this.noscore_button.Click += new System.EventHandler(this.noscore_button_Click);
            // 
            // twoscore_pic
            // 
            this.twoscore_pic.BackColor = System.Drawing.Color.Transparent;
            this.twoscore_pic.Location = new System.Drawing.Point(428, 3736);
            this.twoscore_pic.Name = "twoscore_pic";
            this.twoscore_pic.Size = new System.Drawing.Size(80, 40);
            this.twoscore_pic.TabIndex = 23;
            this.twoscore_pic.Text = "33123";
            // 
            // left_land
            // 
            this.left_land.BackColor = System.Drawing.Color.Transparent;
            this.left_land.Image = global::poker.Properties.Resources.land1;
            this.left_land.Location = new System.Drawing.Point(36, 176);
            this.left_land.Name = "left_land";
            this.left_land.Size = new System.Drawing.Size(40, 40);
            this.left_land.TabIndex = 28;
            // 
            // up_land
            // 
            this.up_land.BackColor = System.Drawing.Color.Transparent;
            this.up_land.Image = global::poker.Properties.Resources.land2;
            this.up_land.Location = new System.Drawing.Point(263, 61);
            this.up_land.Name = "up_land";
            this.up_land.Size = new System.Drawing.Size(40, 40);
            this.up_land.TabIndex = 29;
            this.up_land.Text = " ";
            // 
            // down_land
            // 
            this.down_land.BackColor = System.Drawing.Color.Transparent;
            this.down_land.Image = global::poker.Properties.Resources.land;
            this.down_land.Location = new System.Drawing.Point(134, 416);
            this.down_land.Name = "down_land";
            this.down_land.Size = new System.Drawing.Size(40, 40);
            this.down_land.TabIndex = 30;
            this.down_land.Text = " ";
            // 
            // right_land
            // 
            this.right_land.BackColor = System.Drawing.Color.Transparent;
            this.right_land.Image = global::poker.Properties.Resources.land3;
            this.right_land.Location = new System.Drawing.Point(878, 176);
            this.right_land.Name = "right_land";
            this.right_land.Size = new System.Drawing.Size(40, 40);
            this.right_land.TabIndex = 31;
            // 
            // breakrule
            // 
            this.breakrule.BackColor = System.Drawing.Color.Transparent;
            this.breakrule.Location = new System.Drawing.Point(352, 397);
            this.breakrule.Name = "breakrule";
            this.breakrule.Size = new System.Drawing.Size(100, 80);
            this.breakrule.TabIndex = 18;
            // 
            // down_clock
            // 
            this.down_clock.BackColor = System.Drawing.Color.Transparent;
            this.down_clock.Location = new System.Drawing.Point(258, 446);
            this.down_clock.Name = "down_clock";
            this.down_clock.Size = new System.Drawing.Size(50, 50);
            this.down_clock.TabIndex = 32;
            this.down_clock.Text = " ";
            this.down_clock.Visible = false;
            // 
            // right_clock
            // 
            this.right_clock.BackColor = System.Drawing.Color.Transparent;
            this.right_clock.Location = new System.Drawing.Point(705, 198);
            this.right_clock.Name = "right_clock";
            this.right_clock.Size = new System.Drawing.Size(50, 50);
            this.right_clock.TabIndex = 32;
            this.right_clock.Text = " ";
            this.right_clock.Visible = false;
            // 
            // up_clock
            // 
            this.up_clock.BackColor = System.Drawing.Color.Transparent;
            this.up_clock.Location = new System.Drawing.Point(508, 119);
            this.up_clock.Name = "up_clock";
            this.up_clock.Size = new System.Drawing.Size(50, 50);
            this.up_clock.TabIndex = 32;
            this.up_clock.Text = " ";
            this.up_clock.Visible = false;
            // 
            // left_clock
            // 
            this.left_clock.BackColor = System.Drawing.Color.Transparent;
            this.left_clock.Location = new System.Drawing.Point(208, 198);
            this.left_clock.Name = "left_clock";
            this.left_clock.Size = new System.Drawing.Size(50, 50);
            this.left_clock.TabIndex = 32;
            this.left_clock.Text = " ";
            this.left_clock.Visible = false;
            // 
            // up_card_mount
            // 
            this.up_card_mount.BackColor = System.Drawing.Color.Transparent;
            this.up_card_mount.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.up_card_mount.Location = new System.Drawing.Point(302, 132);
            this.up_card_mount.Name = "up_card_mount";
            this.up_card_mount.Size = new System.Drawing.Size(17, 17);
            this.up_card_mount.TabIndex = 33;
            // 
            // left_card_mount
            // 
            this.left_card_mount.BackColor = System.Drawing.Color.Transparent;
            this.left_card_mount.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.left_card_mount.Location = new System.Drawing.Point(77, 389);
            this.left_card_mount.Name = "left_card_mount";
            this.left_card_mount.Size = new System.Drawing.Size(17, 17);
            this.left_card_mount.TabIndex = 33;
            // 
            // right_card_mount
            // 
            this.right_card_mount.BackColor = System.Drawing.Color.Transparent;
            this.right_card_mount.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.right_card_mount.Location = new System.Drawing.Point(919, 389);
            this.right_card_mount.Name = "right_card_mount";
            this.right_card_mount.Size = new System.Drawing.Size(18, 14);
            this.right_card_mount.TabIndex = 33;
            // 
            // down_card_mount
            // 
            this.down_card_mount.BackColor = System.Drawing.Color.Transparent;
            this.down_card_mount.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.down_card_mount.Location = new System.Drawing.Point(173, 631);
            this.down_card_mount.Name = "down_card_mount";
            this.down_card_mount.Size = new System.Drawing.Size(17, 17);
            this.down_card_mount.TabIndex = 33;
            // 
            // background_music_btn
            // 
            this.background_music_btn.BackColor = System.Drawing.Color.Transparent;
            this.background_music_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.background_music_btn.Image = global::poker.Properties.Resources.background_music_btn;
            this.background_music_btn.Location = new System.Drawing.Point(20, 551);
            this.background_music_btn.Name = "background_music_btn";
            this.background_music_btn.Size = new System.Drawing.Size(32, 32);
            this.background_music_btn.TabIndex = 34;
            this.background_music_btn.Click += new System.EventHandler(this.background_music_btn_Click);
            // 
            // kuang
            // 
            this.kuang.Image = ((System.Drawing.Image)(resources.GetObject("kuang.Image")));
            this.kuang.Location = new System.Drawing.Point(3, 2);
            this.kuang.Name = "kuang";
            this.kuang.Size = new System.Drawing.Size(188, 108);
            this.kuang.TabIndex = 1;
            this.kuang.TabStop = false;
            // 
            // score_button
            // 
            this.score_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.score_button.Image = global::poker.Properties.Resources.score_button;
            this.score_button.Location = new System.Drawing.Point(62, 110);
            this.score_button.Name = "score_button";
            this.score_button.Size = new System.Drawing.Size(72, 23);
            this.score_button.TabIndex = 0;
            this.score_button.Text = "      ";
            this.score_button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.score_button_MouseMove);
            // 
            // score_panel
            // 
            this.score_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.score_panel.BackColor = System.Drawing.Color.Transparent;
            this.score_panel.Controls.Add(this.score_button);
            this.score_panel.Controls.Add(this.kuang);
            this.score_panel.Location = new System.Drawing.Point(770, 28);
            this.score_panel.Name = "score_panel";
            this.score_panel.Size = new System.Drawing.Size(192, 137);
            this.score_panel.TabIndex = 15;
            // 
            // backgound_music
            // 
            this.backgound_music.Enabled = true;
            this.backgound_music.Location = new System.Drawing.Point(15, 433);
            this.backgound_music.Name = "backgound_music";
            this.backgound_music.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("backgound_music.OcxState")));
            this.backgound_music.Size = new System.Drawing.Size(75, 23);
            this.backgound_music.TabIndex = 35;
            this.backgound_music.Visible = false;
            // 
            // sound_effect_btn
            // 
            this.sound_effect_btn.BackColor = System.Drawing.Color.Transparent;
            this.sound_effect_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sound_effect_btn.Image = ((System.Drawing.Image)(resources.GetObject("sound_effect_btn.Image")));
            this.sound_effect_btn.Location = new System.Drawing.Point(20, 591);
            this.sound_effect_btn.Name = "sound_effect_btn";
            this.sound_effect_btn.Size = new System.Drawing.Size(32, 32);
            this.sound_effect_btn.TabIndex = 34;
            this.sound_effect_btn.Click += new System.EventHandler(this.sound_effect_btn_Click);
            // 
            // Game_Landlord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(962, 646);
            this.Controls.Add(this.backgound_music);
            this.Controls.Add(this.sound_effect_btn);
            this.Controls.Add(this.background_music_btn);
            this.Controls.Add(this.down_card_mount);
            this.Controls.Add(this.right_card_mount);
            this.Controls.Add(this.left_card_mount);
            this.Controls.Add(this.up_card_mount);
            this.Controls.Add(this.left_clock);
            this.Controls.Add(this.up_clock);
            this.Controls.Add(this.right_clock);
            this.Controls.Add(this.down_clock);
            this.Controls.Add(this.breakrule);
            this.Controls.Add(this.right_land);
            this.Controls.Add(this.down_land);
            this.Controls.Add(this.up_land);
            this.Controls.Add(this.left_land);
            this.Controls.Add(this.twoscore_pic);
            this.Controls.Add(this.noscore_button);
            this.Controls.Add(this.threescore_button);
            this.Controls.Add(this.twoscore_button);
            this.Controls.Add(this.onescore_button);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.chat_panel);
            this.Controls.Add(this.hint_button);
            this.Controls.Add(this.outcard_button);
            this.Controls.Add(this.refuse_button);
            this.Controls.Add(this.rightmes);
            this.Controls.Add(this.downmes);
            this.Controls.Add(this.leftmes);
            this.Controls.Add(this.upmes);
            this.Controls.Add(this.robot);
            this.Controls.Add(this.order);
            this.Controls.Add(this.up);
            this.Controls.Add(this.left);
            this.Controls.Add(this.right);
            this.Controls.Add(this.down);
            this.Controls.Add(this.undercard);
            this.Controls.Add(this.score_panel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(962, 646);
            this.MinimumSize = new System.Drawing.Size(962, 646);
            this.Name = "Game_Landlord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "四人斗地主";
            this.Load += new System.EventHandler(this.Game_Landlord_Load);
            this.DoubleClick += new System.EventHandler(this.Game_Landlord_DoubleClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDoubleClick);
            this.undercard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.up)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.order)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.robot)).EndInit();
            this.chat_panel.ResumeLayout(false);
            this.chat_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kuang)).EndInit();
            this.score_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.backgound_music)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel undercard;
        private PictureBox down;
        private PictureBox right;
        private PictureBox left;
        private PictureBox up;
        private PictureBox order;
        private PictureBox robot;
        private Label upmes;
        private Label leftmes;
        private Label downmes;
        private Label rightmes;
        private Label refuse_button;
        private Label outcard_button;
        private Label hint_button;
        private Panel chat_panel;
        private TextBox chat;
        private Label chat_button;
        private Label start_button;
        private Label onescore_button;
        private Label twoscore_button;
        private Label threescore_button;
        private Label noscore_button;
        private Label twoscore_pic;
        private Label left_land;
        private Label right_land;
        private Label up_land;
        private Label down_land;
        private Label sent_button;
        private Label breakrule;
        private Label down_clock;
        private Label right_clock;
        private Label up_clock;
        private Label left_clock;
        private Label up_card_mount;
        private Label left_card_mount;
        private Label right_card_mount;
        private Label down_card_mount;
        private Label dipai;
        private Label background_music_btn;
        private PictureBox kuang;
        private Label score_button;
        private Panel score_panel;
        private AxWMPLib.AxWindowsMediaPlayer backgound_music;
        private Label sound_effect_btn;
        private Label chatlist;
    }
}

