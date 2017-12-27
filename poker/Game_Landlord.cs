using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using ControlExs;

namespace poker
{
    public partial class Game_Landlord : FormEx
    {
        /*变量命名处-start*/
        #region
        private string str = ""; 

        public string[] name = new string[4];       //四个人的姓名
        ManagePeoplelist managedeck;   //业务类，对四人队列进行操作
        PeopleCardPostion postion;    //控制卡牌的位置
        public PeopleList[] peoplelist = new PeopleList[4];        //四个人的队列
        public CardPile cardpile = new CardPile();                 //剩余的卡组

        public int begin_pos;   //起始位子
        public int end_pos;     //终止位子
        public int mark;        //叫的分
        int isFirstCallScoreFlag;  //是否为第一次叫分
        Random ran;             //随机种子
        public int whosLand;        //谁是地主
        public Boolean IsGameStart;     //游戏是否开始
        public Boolean isrobotflag;     //是否有机器人

        //public int person;   //指哪个人，0指down，1指right，2指up，3指left
        //public int clockmode;     //抢地主和出牌倒计时时长，10或35
        //public bool clockend;     //判断计时是否结束，true是结束，false是未结束
        //public Thread clockthread;  //时钟线程

        public List<Card>[] toOutCard = new List<Card>[4];//选择希望打出的牌数组
        public int who_out;      //当前是哪个人出的牌      

        public int integration;     //当前局数的积分
        public int multiple;        //当前局数的倍数
        public int[] lastScore = new int[4];    //最终得分

        Label[] people_card_count = new Label[4];  //四个人卡组的数量
        public Label[] poke = new Label[109];       //108张卡
        public Label[] downpoke = new Label[8];     //8张底牌
        public Label[] mes = new Label[4];          //四个人的信息      
        public Label[] score_pic = new Label[4];     //显示图片（1分、不叫）
        public Label[] clock_pic = new Label[4];    //时钟图片
        Label[] lbl_bottomscore_multiple = new Label[2];//底分倍数
        Label end_score_panel;        //结束的积分界面
        Label[] score_panel_score = new Label[12];  //计分按钮
        Label[][] end_panel_score = new Label[4][]; //结束计分按钮
        Label[] lbl_roboted = new Label[4];//托管后图标                                          
        Label[] fourchat = new Label[8];//创建四人说话

        public Boolean[] fourPeopleSex = new Boolean[] { true, true, true, false };//初始化四人的性别，性别true为男
        public Boolean backgroundMusicFlag; //是否可以播放背景音乐
        MusicPlay_Landlord soundEffect;  //音效播放
        RandomName randomName;      //初始化随机名字的类

        Boolean isCreaterMode = false;      //判断是否为开发者模式，true为是，false为否
        Boolean isCanRuse;       //判断下家是否可以不出牌
        Boolean isPokeDeal;     //判断牌有没有发
        Boolean isTurnOvering = false;  //这局是否正处于完结未关闭中
        #endregion

        /*界面构造函数处-start*/
        #region
        public Game_Landlord()
        {
            InitalGame_Landlord();
        }
        #endregion

        /* 线程委托主线程加控件出-start */
        #region
        private delegate void addPokeDelegate(int num); //委托主线程加卡牌
        private void addPoke(int num)
        {
            if (this.InvokeRequired == false) //如果调用该函数的线程和控件lstMain位于同一个线程内  
            {
                this.Controls.Add(poke[num]);
            }
            else  //如果调用该函数的线程和控件lstMain不在同一个线程  
            {
                //通过使用Invoke的方法，让子线程告诉窗体线程来完成相应的控件操作  
                addPokeDelegate DMSGD = new addPokeDelegate(addPoke);
                this.Invoke(DMSGD, num);
            }
        }
        private delegate void addFourchatDelegate(int wh); //委托主线程加说话气泡
        private void addFourchat(int wh)
        {
            if (this.InvokeRequired == false) //如果调用该函数的线程和控件lstMain位于同一个线程内  
            {
                this.Controls.Add(fourchat[wh]);
                this.Controls.Add(fourchat[wh + 4]);
            }
            else  //如果调用该函数的线程和控件lstMain不在同一个线程  
            {
                //通过使用Invoke的方法，让子线程告诉窗体线程来完成相应的控件操作  
                addFourchatDelegate DMSGD = new addFourchatDelegate(addFourchat);
                this.Invoke(DMSGD, wh);
            }
        }
        private delegate void addEnd_score_panelDelegate(); //委托主线程加结束面板
        private void addEnd_score_panel()
        {
            if (this.InvokeRequired == false) //如果调用该函数的线程和控件lstMain位于同一个线程内  
            {
                this.Controls.Add(end_score_panel);
            }
            else  //如果调用该函数的线程和控件lstMain不在同一个线程  
            {
                //通过使用Invoke的方法，让子线程告诉窗体线程来完成相应的控件操作  
                addEnd_score_panelDelegate DMSGD = new addEnd_score_panelDelegate(addEnd_score_panel);
                this.Invoke(DMSGD);
            }
        }
        private delegate void addCentrePokeDelegate(int num); //委托主线程加中间的牌堆
        private void addCentrePoke(int num)
        {
            if (this.InvokeRequired == false) //如果调用该函数的线程和控件lstMain位于同一个线程内  
            {
                this.Controls.Add(centre_poke[num]);
            }
            else  //如果调用该函数的线程和控件lstMain不在同一个线程  
            {
                //通过使用Invoke的方法，让子线程告诉窗体线程来完成相应的控件操作  
                addCentrePokeDelegate DMSGD = new addCentrePokeDelegate(addCentrePoke);
                this.Invoke(DMSGD, num);
            }
        }
        private delegate void addUnderPokeDelegate(int num); //委托主线程加底牌
        private void addUnderPoke(int num)
        {
            if (undercard.InvokeRequired == false) //如果调用该函数的线程和控件lstMain位于同一个线程内  
            {
                undercard.Controls.Add(downpoke[num]);
            }
            else  //如果调用该函数的线程和控件lstMain不在同一个线程  
            {
                //通过使用Invoke的方法，让子线程告诉窗体线程来完成相应的控件操作  
                addUnderPokeDelegate DMSGD = new addUnderPokeDelegate(addUnderPoke);
                undercard.Invoke(DMSGD, num);
            }
        }
        #endregion

        /*初始化窗体，包括各个控件和变量的初始化-start*/
        #region
        //初始化Game_Landlord窗口
        public void InitalGame_Landlord()
        {
            InitializeComponent();
            this.DoubleClick += new EventHandler(Game_Landlord_DoubleClick);
            this.KeyDown += new KeyEventHandler(sent_KeyDown);
            //跨线程调用窗体控件
            CheckForIllegalCrossThreadCalls = false;
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
            str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
        }
        //刚进入窗口，初始化各种控件和变量
        private void Game_Landlord_Load(object sender, EventArgs e)
        {
            soundEffect = new MusicPlay_Landlord(); //音效初始化
            //姓名初始化//四个人的姓名
            randomName = new RandomName();
            name[0] = randomName.getRandomName();
            name[1] = randomName.getRandomName();
            name[2] = randomName.getRandomName();
            name[3] = randomName.getRandomName();

            //初始化四人的信息
            for (int i = 0; i < 4; i++)
                mes[i] = new Label();
            mes[0] = downmes;
            mes[1] = rightmes;
            mes[2] = upmes;
            mes[3] = leftmes;

            ////初始化时钟的信息
            //for (int i = 0; i < 4; i++)
            //{
            //    clock_pic[i] = new Label();
            //}
            //clock_pic[0] = down_clock;
            //clock_pic[1] = right_clock;
            //clock_pic[2] = up_clock;
            //clock_pic[3] = left_clock;

            //初始化四人卡组的数量
            people_card_count = new Label[4] { down_card_mount, right_card_mount, up_card_mount, left_card_mount };

            //初始化四人、牌堆、出牌的队列
            GameMode landlordgame = new LandLord_GameMode();
            managedeck = new ManagePeoplelist(landlordgame);
            for (int i = 0; i < 4; i++)
            {
                peoplelist[i] = new PeopleList();
                toOutCard[i] = new List<Card>();
            }

            isrobotflag = false;    //机器人托管初始化
            ran = new Random();    //初始化随机种子

            InitAllVariable();  //初始化所有的变量
            Inital_Backgound_Music();   //初始化背景音乐

            InitalPoke(); //初始化108张牌的标签
            InitialLabelPicture(); //初始化各标签图片            
            InitFour_score_pic();   //初始化4张显示图片（1分、不出）
            initroboted();//初始化机器人

            InitalScore_panel();    //初始化积分榜 
            UpdateScore_panel();    //更新积分榜          
            bottomscore_multiple(); //初始化底分和倍数  
            init_fourpeoplechat();  //初始化四人说话         
            InitEnd_Score_panel();  //初始化结束时的积分榜
            InitalHintButton();     //托管、更换排序、背景音乐的提示
            Inital_CenterCard();    //初始化中间卡堆
            Show_CenterCard();      //显示中间牌堆

            breakrule.Visible = false;//提示框隐藏         
            OutCardThreebtnVisibility(false);//出牌三按钮已经隐藏           
            CallScoreFourbtnVisibility(false);//叫分四按钮隐藏           
            FourLandPicbtnVisibility(false);//四个地主的头像隐藏
        }
        //初始化108张牌
        private void InitalPoke()
        {
            //初始化108个标签
            for (int i = 1; i < 109; i++)
            {
                poke[i] = new Label();
                poke[i].Name = i.ToString();
                poke[i].Click += new System.EventHandler(lb_Click);
                poke[i].MouseDown += new MouseEventHandler(this.lb_MouseDown);
                poke[i].MouseUp += new MouseEventHandler(this.lb_MouseUp);
                poke[i].Width = Constants.CARD_WEIGHT;
                poke[i].Height = Constants.CARD_HEIGHT;
            }
        }
        //初始化各标签图片
        private void InitialLabelPicture()
        {
            //初始化各标签图片
            //up.Image = Image.FromFile(str + "image\\man1.png"); //下面头像
            //left.Image = Image.FromFile(str + "image\\man2.png");//左边头像
            //right.Image = Image.FromFile(str + "image\\man3.png");//右边头像
            //down.Image = Image.FromFile(str + "image\\man4.png");//下面头像
            //robot.Image = Image.FromFile(str + "image\\robot.png");//托管图标
            //order.Image = Image.FromFile(str + "image\\order.png");//排序图标
            //refuse_button.Image = Image.FromFile(str + "image\\refuse.png");//不出
            //score_button.Image = Image.FromFile(str + "image\\score_button.png");//积分按钮
            //chat_button.Image = Image.FromFile(str + "image\\chat_button.png");//聊天按钮
            //start_button.Image = Image.FromFile(str + "image\\start_button.png");//开始按钮
            //onescore_button.Image = Image.FromFile(str + "image\\onescore_button.png");//一分按钮
            //twoscore_button.Image = Image.FromFile(str + "image\\twoscore_button.png");//两分按钮
            //threescore_button.Image = Image.FromFile(str + "image\\threescore_button.png");//三分按钮
            //noscore_button.Image = Image.FromFile(str + "image\\noscore_button.png");//不叫按钮
            //outcard_button.Image = Image.FromFile(str + "image\\outcard_button.png");//出牌图片
            //hint_button.Image = Image.FromFile(str + "image\\hint_button.png");//提示图片
            //breakrule.Image = Image.FromFile(str + "image\\breakrule.png");//提示图片
            //四个地主按钮
            //left_land.Image = Image.FromFile(str + "image\\Land.png");
            //up_land.Image = Image.FromFile(str + "image\\Land.png");
            //down_land.Image = Image.FromFile(str + "image\\Land.png");
            //right_land.Image = Image.FromFile(str + "image\\Land.png");
            //四个人时钟按钮
            //down_clock.Image = Image.FromFile(str + "image\\clock\\time0.png");
            //right_clock.Image = Image.FromFile(str + "image\\clock\\time0.png");
            //up_clock.Image = Image.FromFile(str + "image\\clock\\time0.png");
            //left_clock.Image = Image.FromFile(str + "image\\clock\\time0.png");
        }
        //初始化4张显示图片（1分、不出）
        private void InitFour_score_pic()
        {
            Point[] p = new Point[4];
            p[0] = new Point(420, 412);
            p[1] = new Point(667, 289);
            p[2] = new Point(420, 200);
            p[3] = new Point(209, 289);
            string[] s = new string[4] { "down", "right", "up", "left" };
            for (int i = 0; i < 4; i++)
            {
                score_pic[i] = new Label();
                score_pic[i].Name = s[i] + "_pic";
                score_pic[i].Width = 80;
                score_pic[i].Height = 40;
                score_pic[i].Location = p[i];
                score_pic[i].BackColor = Color.Transparent;
                if (i > 0)
                    score_pic[i].Image = Image.FromFile(str + "image\\start_pic.png");
                else
                    score_pic[i].Image = Image.FromFile(str + "image\\null.png");
                this.Controls.Add(score_pic[i]);
            }
        }
        //背景音乐的初始化
        private void Inital_Backgound_Music()
        {
            backgroundMusicFlag = true;     //背景音乐开始播放
            Constants.soundEffectMusicFlag = true;    //音效播放
            string[] s = new string[200];
            string backgound = str + "\\sound\\背景";
            for (int i = 0; i < 200; i++)
            {
                s[i] = backgound + ran.Next(1, 9).ToString() + ".mp3";
                backgound_music.currentPlaylist.appendItem(backgound_music.newMedia(s[i]));
            }
            backgound_music.Ctlcontrols.play();
        }
        //初始化所有的变量
        private void InitAllVariable()
        {
            isPokeDeal = false;     //一开始没发牌
            centre_poke_num = 0;    //中间牌个数为0
            postion = new PeopleCardPostion(946, 607, 38);  //初始化计算位置的对象
            whosLand = -1;       //初始化地主没有人
            mark = 0;            //当前叫分  
            multiple = 1;        //初始化当前局数的倍数
            integration = 0;     //初始化当前局数的积分
            isFirstCallScoreFlag = 1;   //初始化是否为第一次叫分
            IsGameStart = false;        //初始化游戏是否开始   
            for (int i = 0; i < 4; i++) //初始化每个人的信息
            {
                mes[i].Text = name[i];
                peoplelist[i].Init_AfterTurn();//对4个队列进行初始化操作
                people_card_count[i].Text = "";//将四个人的卡组数量置空
                toOutCard[i].Clear();//清空出牌的队列
            }
            cardpile.deck.Clear();  //清空逻辑底牌 
        }
        //初始化结束时的积分榜 
        private Point movePoint;    //移动积分榜坐标
        private void InitEnd_Score_panel()
        {
            end_score_panel = new Label();
            end_score_panel.MouseDown += new MouseEventHandler(this.endScore_MouseDown);
            end_score_panel.MouseMove += new MouseEventHandler(this.endScore_MouseMove);

            Point p = new Point(370, 218);
            end_score_panel.Location = p;
            end_score_panel.Height = 260;
            end_score_panel.Width = 200;
            end_score_panel.BackgroundImage = Image.FromFile(str + "image\\endscorepanel.png");//结束积分板
            Label close = new Label();  //在左上角添加差的按钮
            close.Click += new System.EventHandler(start_new_turn_click);
            Point pclose = new Point(176, 5);
            close.Location = pclose;
            close.Image = Image.FromFile(str + "image\\close.png");
            close.Height = 17;
            close.Width = 17;
            close.BackColor = Color.Transparent;
            end_score_panel.Controls.Add(close);

            Point pp = new Point(8, 81);
            for (int i = 0; i < 4; i++)
            {
                end_panel_score[i] = new Label[3];
            }
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    end_panel_score[i][j] = new Label();
                    end_panel_score[i][j].BackColor = Color.Transparent;
                    end_panel_score[i][j].Height = 40;
                    end_panel_score[i][j].Width = 64;
                    end_panel_score[i][j].Location = pp;
                    end_panel_score[i][j].ForeColor = Color.White;
                    end_panel_score[i][j].TextAlign = ContentAlignment.MiddleCenter;
                    end_score_panel.Controls.Add(end_panel_score[i][j]);
                    if (j == 0)
                    {
                        end_panel_score[i][j].Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        end_panel_score[i][j].Text = name[i];
                        end_panel_score[i][j].Name = "wax" + i.ToString();
                    }
                    else if (j == 1)
                    {
                        if (lastScore[i] > 0)         //判断胜利还是失败的图标
                            end_panel_score[i][j].Image = Image.FromFile(str + "image\\win.png");
                        else
                            end_panel_score[i][j].Image = Image.FromFile(str + "image\\lose.png");
                        end_score_panel.Controls.Add(end_panel_score[i][j]);
                    }
                    else
                    {
                        end_panel_score[i][j].Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        end_panel_score[i][j].Text = lastScore[i].ToString();     //赋给对应获得的积分
                    }
                    pp.Y += 44;
                }
                pp.X += 60;
                pp.Y = 81;
            }
        }
        //托管、更换排序、背景音乐等所有控件的提示
        private void InitalHintButton()
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(background_music_btn, "背景音乐");
            p.SetToolTip(robot, "托管");
            p.SetToolTip(order, "更换牌序");
            p.SetToolTip(sound_effect_btn, "音效");
            p.SetToolTip(up, "单击更换人物");
            p.SetToolTip(right, "单击更换人物");
            p.SetToolTip(left, "单击更换人物");
            p.SetToolTip(down, "单击更换人物");
            p.SetToolTip(mes[0], "单击更换昵称");
            p.SetToolTip(mes[1], "单击更换昵称");
            p.SetToolTip(mes[2], "单击更换昵称");
            p.SetToolTip(mes[3], "单击更换昵称");
            p.SetToolTip(left_land, "该玩家是地主");
            p.SetToolTip(up_land, "该玩家是地主");
            p.SetToolTip(right_land, "该玩家是地主");
            p.SetToolTip(down_land, "该玩家是地主");
        }
        //初始化底分倍数
        public void bottomscore_multiple()
        {
            Point p = new Point(380, 623);
            for (int i = 0; i < 2; i++)
            {
                lbl_bottomscore_multiple[i] = new Label();
                lbl_bottomscore_multiple[i].Location = p;
                lbl_bottomscore_multiple[i].Width = 100;
                lbl_bottomscore_multiple[i].Height = 35;
                lbl_bottomscore_multiple[i].Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl_bottomscore_multiple[i].BackColor = Color.Transparent;
                lbl_bottomscore_multiple[i].ForeColor = Color.White;
                this.Controls.Add(lbl_bottomscore_multiple[i]);
                p.X += 120;
            }
            lbl_bottomscore_multiple[0].Text = "底分：";
            lbl_bottomscore_multiple[1].Text = "倍数：";
        }
        //初始化积分榜        
        public void InitalScore_panel()
        {
            int px = 6, py = 25;
            for (int i = 0; i < 12; i++)
            {
                Point p = new Point(px, py);
                score_panel_score[i] = new Label();
                score_panel_score[i].ForeColor = Color.White;
                score_panel_score[i].Width = 60;
                score_panel_score[i].Height = 20;
                score_panel_score[i].Location = p;
                score_panel_score[i].TextAlign = ContentAlignment.MiddleCenter;
                kuang.Controls.Add(score_panel_score[i]);
                switch (i % 3)              //更新四人积分榜的信息
                {
                    case 0: px = px + 60; score_panel_score[i].Name = "wax" + i.ToString(); break;
                    case 1: px = px + 60; break;
                    case 2: py += 20; px = 6; break;
                }
            }
        }
        //初始化托管后图标
        public void initroboted()
        {
            int[] pp = new int[] { 755, 183, 96, 421, 510, 98, 125, 183 };
            Point p = new Point();
            ToolTip ppp = new ToolTip();
            ppp.ShowAlways = true;
            for (int i = 0; i < 4; i++)
            {
                p.X = pp[i * 2];
                p.Y = pp[i * 2 + 1];
                lbl_roboted[i] = new Label();
                lbl_roboted[i].Location = p;
                lbl_roboted[i].Width = 35;
                lbl_roboted[i].Height = 35;
                lbl_roboted[i].BackColor = Color.Transparent;
                ppp.SetToolTip(this.lbl_roboted[i], "托管中...");
                lbl_roboted[i].Image = Image.FromFile(str + "image\\roboted.png");
                lbl_roboted[i].Visible = true;
            }
        }
        //初始化四人说话气泡框
        public void init_fourpeoplechat()
        {
            Point[] p = new Point[4];
            p[0] = new Point(180, 388);
            p[1] = new Point(752, 168);
            p[2] = new Point(216, 32);
            p[3] = new Point(104, 163);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    fourchat[i + j * 4] = new Label();
                    fourchat[i + j * 4].Text = null;
                    p[i] = new Point(p[i].X, p[i].Y + j * 64);
                    fourchat[i + j * 4].Location = p[i];
                    fourchat[i + j * 4].Width = 100;
                    fourchat[i + j * 4].Height = 64 + j * -48;
                    fourchat[i + j * 4].Font = new System.Drawing.Font("黑体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    //fourchat[i].Padding = new Padding(3, 10, 3, 10);
                    fourchat[i + j * 4].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    fourchat[i + j * 4].BackColor = Color.Transparent;
                    fourchat[i + j * 4].ForeColor = Color.Black;
                }                
                if (i == 1 || i == 2)
                {
                    fourchat[i].Image = Image.FromFile(str + "image\\chat1_up.png");
                    fourchat[i + 4].Image = Image.FromFile(str + "image\\chat1_down.png");
                }                   
                else
                {
                    fourchat[i].Image = Image.FromFile(str + "image\\chat2_up.png");
                    fourchat[i + 4].Image = Image.FromFile(str + "image\\chat2_down.png");
                }                 
            }
        }
        #endregion

        /*开始按钮和叫地主的环节-start */
        #region
        //开始按钮的事件
        private void start_button_Click(object sender, EventArgs e)
        {
            if (Constants.IsAutoTest == false && isrobotflag == true)   //如果在托管状态且不是自动测试状态
                robot_Click(null, null);    //关闭托管
            soundEffect.StartGame();    //音效开始游戏                           
            for (int i = 0; i < 4; i++)         //更新四个人的动作显示
            {
                score_pic[i].Image = Image.FromFile(str + "image\\null.png");
            }
            this.Controls.Remove(start_button);     //移除开始按钮
            IsGameStart = false;        //初始化游戏是否开始
            managedeck.Shuffle(ref peoplelist, ref cardpile);   //卡组洗牌
            Show_DealCard();             //屏幕显示发牌                
        }
        //开始叫分
        public void callscore()
        {                      
            if (isFirstCallScoreFlag == 1)          //如果是第一轮叫分，先随机第一个出的人
            {
                begin_pos = ran.Next(0, 4);           //从哪个人开始叫分，ran.Next随机0~3的数
                end_pos = begin_pos + 3;
                isFirstCallScoreFlag = 2;           //不是第一次进入该函数     
                showCallResult(begin_pos);
            }
            else
            {
                begin_pos++;
                CallScoreFourbtnVisibility(false);  //如果第二轮叫分，先隐藏叫分四按钮
                t = new System.Timers.Timer(Constants.COMPUTER_THINK_TIME);   //第一次停1秒，第二次
                t.AutoReset = false;
                t.Enabled = true;
                t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
                {
                    showCallResult(begin_pos);
                });
            }                                      
        }
        //显示叫分的结果
        string[] pic = new string[] { "noscore_pic", "onescore_pic", "twoscore_pic", "threescore_pic" };
        public void showCallResult(int i)        
        {
            if (i > end_pos || integration >= 3) //一轮循环完
            {
                endCallScore();
            }
            else if (i % 4 == 0 && Constants.IsAutoTest == false)    //如果轮到玩家出牌且不处于测试状态
            {
                CallScoreFourbtnVisibility(true);   //显示叫分按钮
            }
            else
            {
                begin_pos++;
                mark = ran.Next(integration + 1, Constants.RandomCallLandlord); //随机叫几分，随机(integration+1,4)的数，4为不叫
                if (mark >= 4)
                {
                    score_pic[i % 4].Image = Image.FromFile(str + "image\\noscore_pic.png");
                    soundEffect.NoCallScore_NoRobLandlord(integration, fourPeopleSex[i % 4]);
                    mark = 0;
                }
                else
                {
                    score_pic[i % 4].Image = Image.FromFile(str + "image\\" + pic[mark] + ".png");//提示图片
                    if (mark >= 1)
                    {
                        onescore_button.Image = Image.FromFile(str + "image\\onescore_button_gray.png");
                        onescore_button.Cursor = Cursors.Arrow;
                    }
                    if (mark >= 2)
                    {
                        twoscore_button.Image = Image.FromFile(str + "image\\twoscore_button_gray.png");
                        twoscore_button.Cursor = Cursors.Arrow;
                    }
                    soundEffect.CallScore_RobLandlord(integration, fourPeopleSex[i % 4]);//叫分或抢地主
                }
                if (mark > integration)         //如果牌比当前大，则目前他是地主
                {
                    whosLand = i % 4;
                    integration = mark;
                }
                t = new System.Timers.Timer(Constants.COMPUTER_THINK_TIME);   //第一次停1秒，第二次
                t.AutoReset = false;
                t.Enabled = true;
                t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
                {
                    showCallResult(i + 1);
                });
            }//else             
        }
        //叫分结束,移除四张图片，叫分四按钮隐藏，开始游戏
        public void endCallScore()
        {
            CallScoreFourbtnVisibility(false);
            if (whosLand != -1)         //如果有人叫分
            {
                for (int i = 0; i < 4; i++)         //先移除四人的动作图片
                {
                    this.Controls.Remove(score_pic[i]);
                }
                IsGameStart = true;
                multiple = integration;     //保存当前的积分
                isTurnOvering = false;      //游戏开始后处于完结未关闭中
                Count_bottomscore_multiple();   //更新当前的底分和倍数
                GameStart();            //游戏开始
            }
            else
            {
                ClearAll();         //清空所有变量，并进行初始化
                start_button_Click(null, null);     //没人叫分重新开始
            }
        }
        //游戏开始！（发8张牌）（地主出牌）
        private void GameStart()
        {
            managedeck.DealLandlord(ref peoplelist, cardpile, whosLand);    //给逻辑地主发牌
            who_out = whosLand;          //目前先出牌的是地主

            //给玩家或电脑发地主的8张牌
            GiveLandCard();

            //将底牌进行显示
            for (int i = cardpile.deck.Count - 1; i >= 0; i--)
            {
                downpoke[i].Image = Image.FromFile(str + "image\\poker\\" + cardpile.deck[i].num + ".jpg");
            }
            //显示地主的图标
            switch (whosLand)
            {
                case 0: down_land.Visible = true; break;
                case 1: right_land.Visible = true; break;
                case 2: up_land.Visible = true; break;
                case 3: left_land.Visible = true; break;
            }
            people_card_count[whosLand].Text = peoplelist[whosLand].deck.Count.ToString();  //更新卡组数量                   
            if (whosLand != 0)          //地主先出牌
            {
                t = new System.Timers.Timer(Constants.COMPUTER_THINK_TIME);   //停1秒
                t.AutoReset = false;
                t.Enabled = true;
                t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
                {
                    OutCard(whosLand);
                });
            }
            else
            {
                OutCardThreebtnVisibility(true);    //如果轮到玩家，则让玩家出牌按钮显示
                                                    //如果当前为下家必须出牌状态
                isCanRuse = false;
                refuse_button.Cursor = Cursors.Arrow;
                refuse_button.Image = Image.FromFile(str + "image\\refuse_gray.png");
            }           
        }       
        //屏幕显示给玩家或电脑发地主的8张牌
        private void GiveLandCard()
        {
            Point[] p = new Point[4];
            for (int i = 0; i < 4; i++)
            {
                p[i] = postion.DisplayCardPostion(0, peoplelist[i].deck.Count, false);
            }
            if (whosLand == 0)          //如果当前下家为地主
            {
                for (int i = cardpile.deck.Count - 1; i >= 0; i--)  //为下家队列发8张牌
                {
                    int num = cardpile.deck[i].num;
                    addPoke(num);
                }
                int len = peoplelist[0].deck.Count;
                Point pp = new Point(p[0].X, p[0].Y - Constants.CARD_INTERVAL);
                List<Card> tCard = Card.CopyListCard(peoplelist[0].deck);   //保存原本的卡组
                for (int i = len - 1; i >= 0; i--)
                {
                    this.Controls.Remove(poke[peoplelist[0].deck[i].num]);
                }
                for (int i = peoplelist[0].deck.Count - 1; i >= 0; i--) //显示为下家发8张牌
                {
                    int num = peoplelist[0].deck[i].num;
                    int j;
                    for (j = 0; j < cardpile.deck.Count; j++)   //找到peoplelist[0]中加底牌的位置，将底牌上移
                    {
                        if (cardpile.deck[j].num == peoplelist[0].deck[i].num)
                        {
                            break;
                        }
                    }
                    if (j == cardpile.deck.Count)
                        poke[num].Location = p[0];
                    else
                    {
                        pp.X = p[0].X;
                        poke[num].Location = pp;
                    }
                    addPoke(num);
                    p[0].X = p[0].X - Constants.CARD_INTERVAL;
                }
            }
            else        //给其他三家的队列发牌
            {
                for (int i = cardpile.deck.Count - 1; i >= 0; i--)  //先将8张卡加入到界面中
                {
                    int num = cardpile.deck[i].num;
                    addPoke(num);
                    poke[num].Image = Image.FromFile(str + "image\\cardback.jpg");
                }
                Show_ThreeComputer_Card(whosLand);
            }
        }        
        #endregion

        /*斗地主功能函数实现处-start */
        #region
        //对出牌三按钮进行隐藏或者显示（true为显示，false为隐藏）
        private void OutCardThreebtnVisibility(bool IsTrueVisible)
        {
            outcard_button.Visible = IsTrueVisible;
            refuse_button.Visible = IsTrueVisible;
            hint_button.Visible = IsTrueVisible;
        }
        //对叫分四按钮进行隐藏或者显示（true为显示，false为隐藏）
        private void CallScoreFourbtnVisibility(bool IsTrueVisible)
        {
            onescore_button.Visible = IsTrueVisible;
            twoscore_button.Visible = IsTrueVisible;
            threescore_button.Visible = IsTrueVisible;
            noscore_button.Visible = IsTrueVisible;
        }
        //对四人地主的头像进行隐藏或者显示（true为显示，false为隐藏）
        private void FourLandPicbtnVisibility(bool IsTrueVisible)
        {
            down_land.Visible = IsTrueVisible;
            right_land.Visible = IsTrueVisible;
            up_land.Visible = IsTrueVisible;
            left_land.Visible = IsTrueVisible;
        }
        //清空所有变量和扑克牌和底牌，并进行初始化
        private void ClearAll()
        {
            InitAllVariable();      //初始化所有变量
            for (int i = 1; i < 109; i++)       //移除108张poke牌
            {
                this.Controls.Remove(poke[i]);
            }
            for (int i = 0; i < 8; i++)     //移除8张底牌
            {
                undercard.Controls.Remove(downpoke[i]);
            }
            Show_CenterCard();      //显示中间的牌堆
        }
        //计算当前的底分和倍数
        private void Count_bottomscore_multiple()
        {
            lbl_bottomscore_multiple[0].Text = "底分：" + integration.ToString();
            lbl_bottomscore_multiple[1].Text = "倍数：" + (integration / multiple).ToString();
        }
        //计算四个人最终得分，并将得分存放到lastScore数组里面
        private void CountFourPeopleScore()
        {
            int[] OrignIntegration = new int[4];
            for (int i = 0; i < 4; i++)         //保存上一局四人原本的积分
            {
                OrignIntegration[i] = peoplelist[i].Integration;
            }
            managedeck.SettlementScore(ref peoplelist, integration);     //结算上一局原本的积分
            for (int i = 0; i < 4; i++)
            {
                lastScore[i] = peoplelist[i].Integration - OrignIntegration[i];
            }
        }
        #endregion

        /*斗地主显示卡组函数实现处-start */
        #region
        Label[] centre_poke;    //显示中间的牌堆
        int centre_poke_num;      
        //显示四家发牌的动画
        public void Show_DealCard()
        {
            soundEffect.DealCard();     //播放开始发牌的音效    
            Point[] p = new Point[4];
            Point[] op = new Point[4];
            for (int j = 0; j < 4; j++)     //计算四个人牌的坐标
            {
                p[j] = postion.DisplayCardPostion(j, peoplelist[j].deck.Count);
                op[j] = postion.OverTurnCardPostion(j, peoplelist[j].deck.Count);
            }
            Show_Cartoon_Card(0, 0, p, op);    //显示发牌动画
        }
        //发一张接一张的牌，i代表是卡组的第几张，j表示是哪个人
        public void Show_Cartoon_Card(int i, int j, Point[] p, Point[] op) 
        {
            int num;
            if (j != 0)     //如果是其余三家
                num = peoplelist[j].deck[i].num;
            else        //如果是玩家则要逆序发牌
                num = peoplelist[j].deck[24 - i].num;
            if (isCreaterMode == false || j == 0)     //如果不是明牌模式
                poke[num].Location = p[j];
            else
                poke[num].Location = op[j];
            this.Controls.Remove(centre_poke[centre_poke_num++]);   //移除中间牌堆的一张牌
            if (j == 0)
            {
                p[j].X = p[j].X - Constants.CARD_INTERVAL;
                poke[num].Image = Image.FromFile(str + "image\\poker\\" + num + ".jpg");
            }
            else
            {
                if (isCreaterMode == false)     //如果不是明牌模式
                {
                    p[j].Y = p[j].Y + Constants.CARD_DECK_INTERVAL;
                    poke[num].Image = Image.FromFile(str + "image\\cardback.jpg");
                }
                else
                {
                    op[j].X = op[j].X - Constants.CARD_INTERVAL;
                    poke[num].Image = Image.FromFile(str + "image\\poker\\" + num + ".jpg");
                }
            }
            addPoke(num);
            j++;        //下一位玩家
            if (j > 3)  //四位玩家都发完了，发下一张牌
            {
                j = 0;
                i++;
            }
            if (i < 25)     //如果是25张牌内
            {
                t = new System.Timers.Timer(Constants.DEALCARD_DURATION_TIME);   //停0.01秒
                t.AutoReset = false;
                t.Enabled = true;
                t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
                {
                    Show_Cartoon_Card(i, j, p, op);
                });
            }  
            else    //四个人的牌都发完了，开始发底牌
            {
                for (int cardnum = 0; cardnum < 4; cardnum++) //更新卡组信息
                {
                    mes[cardnum].Text = name[cardnum] + "\n剩余张数：";
                    people_card_count[cardnum].Text = peoplelist[cardnum].deck.Count.ToString();
                }
                Show_UnderCard();
            }          
        }
        //底牌动画显示
        public void Show_UnderCard()
        {
            Point p = new Point(112, 34);
            Show_Cartoon_UnderCard(cardpile.deck.Count - 1, p);
        }
        public void Show_Cartoon_UnderCard(int thread_i, Point p)
        {
            downpoke[thread_i] = new Label();
            int num = cardpile.deck[thread_i].num;
            poke[num].Image = Image.FromFile(str + "image\\poker\\" + num + ".jpg");  //移除了八张底牌
            poke[num].Location = p;
            downpoke[thread_i].Name = thread_i.ToString();
            downpoke[thread_i].Width = Constants.CARD_WEIGHT;
            downpoke[thread_i].Height = Constants.CARD_HEIGHT;
            downpoke[thread_i].Location = p;
            downpoke[thread_i].Image = Image.FromFile(str + "image\\cardback.jpg");      //一开始底牌为遮蔽状态
            this.Controls.Remove(centre_poke[centre_poke_num++]);//移除中间牌堆的一张牌
            p.X = p.X - Constants.CARD_INTERVAL;
            addUnderPoke(thread_i);
            thread_i--;
            if (thread_i >= 0)
            {
                t = new System.Timers.Timer(Constants.DEALCARD_DURATION_TIME);   //停1秒
                t.AutoReset = false;
                t.Enabled = true;
                t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
                {
                    Show_Cartoon_UnderCard(thread_i, p);
                });
            }   
            else        //卡牌底牌全部发完，开始叫分
            {
                isPokeDeal = true;
                callscore();                //开始叫分
            }        
        }
        //Down牌显示     
        public void Show_DownCard()
        {
            Point p = postion.DisplayCardPostion(0, peoplelist[0].deck.Count);
            for (int i = peoplelist[0].deck.Count - 1; i >= 0; i--)
            {
                int num = peoplelist[0].deck[i].num;
                this.Controls.Remove(poke[num]);
                poke[num].Location = p;
                addPoke(num);
                p.X = p.X - Constants.CARD_INTERVAL;
            }
        }
        //初始化中间牌堆
        public void Inital_CenterCard()
        {
            centre_poke = new Label[108];
            Point pl = new Point(406, 249);
            for (int i = 0; i < 54; i++)
            {
                centre_poke[i] = new Label();
                centre_poke[i].Width = Constants.CARD_WEIGHT;
                centre_poke[i].Height = Constants.CARD_HEIGHT;
                centre_poke[i].Location = pl;
                centre_poke[i].Image = Image.FromFile(str + "image\\cardback.jpg");                
                pl.Y = pl.Y + Constants.CARD_DECK_INTERVAL;
            }
            pl = new Point(471, 249);
            for (int i = 54; i < 108; i++)
            {
                centre_poke[i] = new Label();
                centre_poke[i].Width = Constants.CARD_WEIGHT;
                centre_poke[i].Height = Constants.CARD_HEIGHT;
                centre_poke[i].Location = pl;
                centre_poke[i].Image = Image.FromFile(str + "image\\cardback.jpg");
                pl.Y = pl.Y + Constants.CARD_DECK_INTERVAL;
            }
        }
        //中间牌堆显示
        public void Show_CenterCard()
        {
            for (int i = 0; i < 108; i++)
            {
                addCentrePoke(i);
            }
        }
        //显示其余三家中一家的牌
        private void Show_ThreeComputer_Card(int who)
        {
            Point p = postion.DisplayCardPostion(who);
            for (int i = 0; i < peoplelist[who].deck.Count; i++)   //显示为其他三家发8张牌
            {
                int num = peoplelist[who].deck[i].num;
                this.Controls.Remove(poke[num]);
                poke[num].Image = Image.FromFile(str + "image\\cardback.jpg");
                poke[num].Location = p;
                p.Y = p.Y + Constants.CARD_DECK_INTERVAL;
                addPoke(num);
            }
        }
        //结束时翻转
        public void Show_OverTurnThree(int who)
        {
            //将剩余的三家卡牌翻转
            Point p = postion.OverTurnCardPostion(who, peoplelist[who].deck.Count);
            for (int i = peoplelist[who].deck.Count - 1; i >= 0; i--)
            {
                int num = peoplelist[who].deck[i].num;
                this.Controls.Remove(poke[num]);
                poke[num].Image = Image.FromFile(str + "image\\poker\\" + num + ".jpg");
                poke[num].Location = p;
                addPoke(num);
                p.X = p.X - Constants.CARD_INTERVAL;
            }
            if (who == 2)
            {
                //将上家的信息置于底层，不要遮住牌
                this.Controls.Remove(up);
                this.Controls.Remove(people_card_count[2]);
                this.Controls.Remove(mes[2]);
                this.Controls.Add(up);
                this.Controls.Add(people_card_count[2]);
                this.Controls.Add(mes[2]);
            }
        }
        #endregion

        /* 玩家出牌和电脑出牌、玩家按钮事件实现处-start */
        #region
        //扑克牌的点击事件，选择牌
        private void lb_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            if (lbl.Location.Y == postion.getPlayerCardY_F())
            {
                Point p = new Point(lbl.Location.X, postion.getPlayerCardY_T());
                lbl.Location = p;
            }
            else if (lbl.Location.Y == postion.getPlayerCardY_T())
            {
                Point p = new Point(lbl.Location.X, postion.getPlayerCardY_F());
                lbl.Location = p;
            }
        }
        //叫1分、2分、3分、不叫按钮的事件
        private void onescore_button_Click(object sender, EventArgs e)
        {
            if (integration < 1)
            {
                score_pic[0].Image = Image.FromFile(str + "image\\onescore_pic.png");//提示图片
                soundEffect.CallScore_RobLandlord(integration, fourPeopleSex[0]);//叫分或抢地主         
                integration = 1;
                mark = 1;
                whosLand = 0;
                callscore();
            }
        }
        private void twoscore_button_Click(object sender, EventArgs e)
        {
            if (integration < 2)
            {
                score_pic[0].Image = Image.FromFile(str + "image\\twoscore_pic.png");//提示图片
                soundEffect.CallScore_RobLandlord(integration, fourPeopleSex[0]);//叫分或抢地主          
                integration = 2;
                mark = 2;
                whosLand = 0;
                callscore();
            }
        }
        private void threescore_button_Click(object sender, EventArgs e)
        {
            score_pic[0].Image = Image.FromFile(str + "image\\threescore_pic.png");//提示图片
            soundEffect.CallScore_RobLandlord(integration, fourPeopleSex[0]);//叫分或抢地主        
            integration = 3;
            mark = 3;
            whosLand = 0;
            callscore();
        }
        private void noscore_button_Click(object sender, EventArgs e)
        {
            score_pic[0].Image = Image.FromFile(str + "image\\noscore_pic.png");//提示图片
            soundEffect.NoCallScore_NoRobLandlord(integration, fourPeopleSex[0]);//不叫分或不抢地主      
            mark = 0;
            callscore();
        }
        //down玩家出牌
        private void outcard_Click(object sender, EventArgs e)
        {
            toOutCard[0].Clear();
            for (int ii = 0; ii < peoplelist[0].deck.Count; ii++)   //找在目前移动的牌
            {
                int num = peoplelist[0].deck[ii].num;
                if (poke[num].Location.Y == postion.getPlayerCardY_T())
                {
                    toOutCard[0].Add(LandLord_GameMode.ConvertCard(num));
                }
            }
            //如果牌可以出
            if (managedeck.OutCard(who_out == 0 ? null : toOutCard[who_out], toOutCard[0]) == true)
            {
                Point p = postion.OutCardPostion(0, toOutCard[0].Count);
                for (int i = toOutCard[0].Count - 1; i >= 0; i--)  //移动出的牌位置
                {
                    this.Controls.Remove(poke[toOutCard[0][i].num]);
                    poke[toOutCard[0][i].num].Location = p;
                    p.X -= Constants.CARD_INTERVAL;
                    addPoke(toOutCard[0][i].num);
                }

                soundEffect.Player_OutorFollowCard_Voice(who_out, 0, toOutCard, fourPeopleSex[0]);//出牌的声音，判断是出牌还是抢牌还是炸弹
                int orign_integration = integration;    //记录当前积分
                bool tflag = managedeck.TrueOutCard(ref peoplelist[0], toOutCard[0], ref integration);  //逻辑卡组更新牌
                p = postion.DisplayCardPostion(0, peoplelist[0].deck.Count);
                Show_DownCard();     //显示下家的手牌
                who_out = 0;         //当前为下家出的牌  
                people_card_count[0].Text = peoplelist[0].deck.Count.ToString();    //更新下家卡牌数量
                Count_bottomscore_multiple();       //更新当前的底分和倍数
                soundEffect.Update_Integration_DeckCount(orign_integration, integration, peoplelist[0].deck.Count, fourPeopleSex[0]);//播放更新积分和卡组数量

                if (tflag == true)//如果出完牌，进入结算画面
                {
                    End_ThisTurnGame();
                    return;
                }
                OutCard(1);  //出牌成功到下一位出牌                                
            }
            else        //如果当前牌不能出，提示
            {
                if (breakrule.Visible == false)
                {
                    //提示没有比当前大的牌
                    breakrule.Location = new Point(344, 394);
                    breakrule.Image = Image.FromFile(str + "image\\breakrule.png");
                    breakrule.Visible = true;
                    timeClock();    // 过两秒钟隐藏
                }
                soundEffect.BreakRule();
            }
        }
        //限制出牌不符合规则的显示时间
        public void timeClock()
        {
            System.Timers.Timer t = new System.Timers.Timer(2000);//实例化Timer类，设置间隔时间为2000毫秒
            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件
            t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件
        }
        private void theout(object sender, ElapsedEventArgs e)
        {
            breakrule.Visible = false;
        }
        //判断出牌逻辑
        public void OutCard(int who)
        {          
            OutCardThreebtnVisibility(false);//其他三家出牌期间，下家不允许出牌
            peoplelist[0].UpdateHintResult();   //更新提示结果
            if (who == 0 && isrobotflag == false)
            {
                //其他三家出完牌，允许下家出牌
                OutCardThreebtnVisibility(true);
                //如果当前为下家必须出牌状态
                if (who_out == 0)
                {
                    isCanRuse = false;
                    refuse_button.Cursor = Cursors.Arrow;
                    refuse_button.Image = Image.FromFile(str + "image\\refuse_gray.png");
                }
                else
                {
                    isCanRuse = true;
                    refuse_button.Cursor = Cursors.Hand;
                    refuse_button.Image = Image.FromFile(str + "image\\refuse.png");
                }
                //到下家出牌时，当前下家出的牌或者是“不出”图片删掉，并将队列清空
                for (int i = 0; i < toOutCard[0].Count; i++)
                {
                    this.Controls.Remove(poke[toOutCard[0][i].num]);
                }
                this.Controls.Remove(score_pic[0]);
                toOutCard[0].Clear();
            }
            else
            {
                int i = who;
                for (int j = 0; j < toOutCard[i].Count; j++)    //移除当前已出牌
                {
                    this.Controls.Remove(poke[toOutCard[i][j].num]);
                }
                this.Controls.Remove(score_pic[i]);         //移除当前家的动作信息
                t = new System.Timers.Timer(Constants.COMPUTER_THINK_TIME);   //停1秒
                t.AutoReset = false;
                t.Enabled = true;
                t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
                {
                    displayPlayerOutCard(who);
                });
            }                                
        }
        //显示玩家出牌
        private void displayPlayerOutCard(int who)
        {
            int i = who;
            Point p_outCard = new Point();  //出牌的位置坐标
            Point p_cardDeck = new Point();  //卡组的位置坐标
            List<Card> TCard = new List<Card>();
            TCard = managedeck.IntelligentOutCard(ref peoplelist, whosLand, who_out, i, who_out == i ? null : toOutCard[who_out]);
            if (TCard.Count != 0)   //如果牌可以出
            {
                toOutCard[i] = TCard;
                p_outCard = postion.OutCardPostion(i, TCard.Count);
                for (int j = TCard.Count - 1; j >= 0; j--)   //移动出的牌位置
                {
                    int num = TCard[j].num;
                    this.Controls.Remove(poke[num]);
                    poke[num].Image = Image.FromFile(str + "image\\poker\\" + num + ".jpg");
                    poke[num].Location = p_outCard;
                    addPoke(num);
                    p_outCard.X = p_outCard.X - Constants.CARD_INTERVAL;
                }
                soundEffect.Player_OutorFollowCard_Voice(who_out, i, toOutCard, fourPeopleSex[i]);//出牌的声音，判断是出牌还是抢牌还是炸弹
                int orign_integration = integration;
                bool tflag = managedeck.TrueOutCard(ref peoplelist[i], toOutCard[i], ref integration);//逻辑更新牌，并播放相应音效
                p_cardDeck = postion.DisplayCardPostion(i, peoplelist[i].deck.Count);//计算显示牌位置的坐标
                if (i == 0)     //玩家更新牌
                {
                    Show_DownCard();
                }
                else    //电脑更新牌
                {
                    if (isCreaterMode == false)     //如果开发者模式关闭
                    {
                        for (int j = 0; j < peoplelist[i].deck.Count; j++)
                        {
                            int num = peoplelist[i].deck[j].num;
                            poke[num].Location = p_cardDeck;
                            p_cardDeck.Y = p_cardDeck.Y + Constants.CARD_DECK_INTERVAL;
                        }
                    }
                    else
                    {
                        Show_OverTurnThree(i);
                    }
                }
                who_out = i;    //现在为该玩家出的牌
                people_card_count[i].Text = peoplelist[i].deck.Count.ToString();//更新卡组数量
                Count_bottomscore_multiple();       //更新当前的底分和倍数
                                                    //播放更新积分和卡组数量
                soundEffect.Update_Integration_DeckCount(orign_integration, integration, peoplelist[i].deck.Count, fourPeopleSex[i]);
                if (tflag == true)//如果出完牌，进入结算画面
                {
                    End_ThisTurnGame();
                    return;
                }
            }//if (TCard.Count != 0)
            else //如果该家不出牌
            {
                soundEffect.Player_Refuse_Voice(fourPeopleSex[i]);
                score_pic[i].Image = Image.FromFile(str + "image\\refuse_pic.png");
                this.Controls.Add(score_pic[i]);
            }// if-else（三家是否能出牌）
            OutCard((who + 1) % 4);
        }
        //提示出牌按钮
        private void hint_button_Click(object sender, EventArgs e)
        {
            //所有的牌都拉下来，显示下家的牌
            Show_DownCard();
            //移动出的牌位置
            List<Card> TCard = managedeck.Remind(ref peoplelist[0], who_out == 0 ? null : toOutCard[who_out]);//上家打出的牌
            if (TCard.Count != 0)
            {
                for (int j = TCard.Count - 1; j >= 0; j--)   //移动出的牌位置
                {
                    int num = TCard[j].num;
                    Point pp = poke[num].Location;
                    pp.Y -= Constants.CARD_INTERVAL;
                    poke[num].Location = pp;
                }
            }
            else
            {
                if (peoplelist[0].JudgeFirstRemind() && breakrule.Visible == false && peoplelist[0].remindStyle == 1)
                {
                    //提示没有比当前大的牌
                    breakrule.Location = new Point(544, 394);
                    breakrule.Image = Image.FromFile(str + "image\\cannotout.png");
                    breakrule.Visible = true;
                    timeClock();
                }
                peoplelist[0].UpdateHintResult();
                soundEffect.BreakRule();
            }
        }
        //不出的按钮
        private void refuse_button_Click(object sender, EventArgs e)
        {
            if (isCanRuse == true)
            {
                score_pic[0].Image = Image.FromFile(str + "image\\refuse_pic.png");//提示图片
                this.Controls.Add(score_pic[0]);         //提示不出
                soundEffect.Player_Refuse_Voice(fourPeopleSex[0]);
                //把玩家的牌回归原样
                Point p = postion.DisplayCardPostion(0, peoplelist[0].deck.Count);
                for (int i = peoplelist[0].deck.Count - 1; i >= 0; i--) //调整剩余牌的位置
                {
                    int num = peoplelist[0].deck[i].num;
                    poke[num].Location = p;
                    p.X = p.X - Constants.CARD_INTERVAL;
                }
                breakrule.Visible = false;
                toOutCard[0].Clear();       //因为是不出，把当前玩家的牌清空
                OutCard(1);         //下一家出牌
            }            
        }
        #endregion

        /* 游戏结束结算画面实现处-start */
        #region
        //获胜结算，显示结束计分板
        private void End_ThisTurnGame()
        {
            OutCardThreebtnVisibility(false);   //隐藏出牌三按钮
            CountFourPeopleScore();    //计算最终计分                 
            soundEffect.Player_EndGame_Voice(lastScore[0] > 0 ? true : false);  //播放胜利或者失败的音效     
            isTurnOvering = true;   //目前处于完结未关闭中，单击托管会无效
            string[] sad = new string[]{ "( ≧Д≦)", "o(╥﹏╥)o", "o(〒﹏〒)o" };
            string[] happy = new string[] { "<(￣︶￣)>", "~(￣▽￣)~", "≧▽≦" };
            for (int i = 0; i < 4; i++)
            {
                if (lastScore[i] > 0)
                    show_fourpeoplechat(i, "哈哈还是我最厉害" +"\r\n"+ happy[ran.Next(3)]);
                else
                    show_fourpeoplechat(i, "呜呜呜" + "\r\n" + sad[ran.Next(3)]);
            }
            Clearwidget();          //暂时移除对应的控件
            UpdateEnd_Score_panel();    //调用结束的积分界面  
            for (int i = 1; i < 4; i++)
                Show_OverTurnThree(i);         //结束后翻转图片     
            if (Constants.IsAutoTest == true)   //如果在自动测试状态，直接自动开始
            {
                start_new_turn_click(null, null);
                start_button_Click(null, null);
            }                 
        }
        //暂时移除对应的控件
        private void Clearwidget()
        {
            for (int i = 0; i < 4; i++)     //移除不叫图标
            {
                this.Controls.Remove(score_pic[i]);
            }
            for (int i = 0; i < 4; i++)     //移除时钟图标
            {
                this.Controls.Remove(clock_pic[i]);
            }
        }
        //更新和显示结束的积分界面
        private void UpdateEnd_Score_panel()
        {
            Point p = new Point(370, 218);
            end_score_panel.Location = p;
            addEnd_score_panel();
            end_score_panel.BringToFront(); //将该控件置为顶层
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (j == 0)
                    {
                        end_panel_score[i][j].Text = name[i];
                    }
                    else if (j == 1)
                    {
                        if (lastScore[i] > 0)         //判断胜利还是失败的图标
                            end_panel_score[i][j].Image = Image.FromFile(str + "image\\win.png");
                        else
                            end_panel_score[i][j].Image = Image.FromFile(str + "image\\lose.png");
                    }
                    else
                    {
                        end_panel_score[i][j].Text = lastScore[i].ToString();     //赋给对应获得的积分
                    }
                }
            }
        }       
        //结束积分“差”按钮的事件，删除结束的积分界面，初始化各种变量，显示开始按钮
        private void start_new_turn_click(object sender, EventArgs e)
        {
            switch (whosLand)   //隐藏地主头像
            {
                case 0: down_land.Visible = false; break;
                case 1: right_land.Visible = false; break;
                case 2: up_land.Visible = false; break;
                case 3: left_land.Visible = false; break;
            }
            lbl_bottomscore_multiple[0].Text = "";  //将底分删除
            lbl_bottomscore_multiple[1].Text = "";  //将倍数删除
            this.Controls.Remove(end_score_panel);//移除结束时的积分版
            UpdateScore_panel();//对积分榜进行更新
            ClearAll(); //初始化所有的变量,并移除所有的牌和底牌
            for (int i = 0; i < 4; i++)     //初始化四个人的
            {
                if (i > 0)
                    score_pic[i].Image = Image.FromFile(str + "image\\start_pic.png");
                else
                    score_pic[i].Image = Image.FromFile(str + "image\\null.png");
            }
            for (int i = 0; i < 4; i++)
            {
                this.Controls.Add(score_pic[i]);//添加score图片
            }            
            onescore_button.Image = Image.FromFile(str + "image\\onescore_button.png");//一分按钮回归
            twoscore_button.Image = Image.FromFile(str + "image\\twoscore_button.png");//两分按钮回归
            this.Controls.Add(start_button);    //显示开始按钮     
            start_button.BringToFront();    //将开始按钮置在顶层
            this.Controls.Remove(lbl_roboted[1]);//移除托管图标
            Show_CenterCard();   //初始化中间牌堆
        }
        //对积分榜的数据进行更新
        public void UpdateScore_panel()
        {
            for (int i = 0; i < 12; i++)
            {
                switch (i % 3)              //更新四人积分榜的信息
                {
                    case 0: score_panel_score[i].Text = name[i / 3]; break;
                    case 1: score_panel_score[i].Text = lastScore[(i - 1) / 3].ToString(); break;
                    case 2:
                        score_panel_score[i].Text = peoplelist[(i - 2) / 3].Integration.ToString();
                        break;
                }
            }
        }
        #endregion       

        /* 界面特效实现处-start */
        #region
        //聊天框和积分框两个鼠标移动事件
        private void chat_button_MouseMove(object sender, MouseEventArgs e)
        {
            if (chat_panel.Location.Y <= 416)
            {
                Point p = new Point(chat_panel.Location.X, chat_panel.Location.Y + 202);
                chat_panel.Location = p;
            }
            else
            {
                Point p = new Point(chat_panel.Location.X, chat_panel.Location.Y - 202);
                chat_panel.Location = p;
            }
        }
        private void score_button_MouseMove(object sender, MouseEventArgs e)
        {
            if (score_button.Location.Y == 110)
            {
                kuang.Location = new Point(kuang.Location.X, kuang.Location.Y - 105);
                score_button.Location = new Point(score_button.Location.X, score_button.Location.Y - 105);
            }
            else
            {
                kuang.Location = new Point(kuang.Location.X, kuang.Location.Y + 105);
                score_button.Location = new Point(score_button.Location.X, score_button.Location.Y + 105);
            }
        }
        //实现拖拽积分榜
        private void endScore_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                movePoint = e.Location;
            }
        }
        private void endScore_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                end_score_panel.Location = new Point(end_score_panel.Location.X + e.X - movePoint.X, end_score_panel.Location.Y + e.Y - movePoint.Y);
            }
        }
        //连续选牌
        Point mouseOff1, mouseOff2;            //鼠标移动位置变量
        bool leftFlag;            //标签是否为左键
        int mouse_x, mouse_y;   //单机鼠标时的坐标
        Thread thread;
        int firstShadowCardNum;
        Boolean isFirstCardShadow;
        private void lb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && peoplelist[0] != null && peoplelist[0].deck.Count > 0)
            {
                Label lbl = (Label)sender;
                Point p = new Point(lbl.Location.X, lbl.Location.Y);
                mouseOff1 = new Point(e.X + p.X, e.Y + p.Y); //得到变量1的值
                leftFlag = true;                  //点击左键按下时标注为true;

                thread = new Thread(new ThreadStart(poke_change_shadow));
                Point p_mouse = this.PointToClient(Control.MousePosition);   //获取相对于窗口的鼠标坐标
                mouse_x = p_mouse.X;
                mouse_y = p_mouse.Y;
                //找到第一张变成阴影的卡
                int count = peoplelist[0].deck.Count;
                int num = peoplelist[0].deck[count - 1].num;
                if (mouse_x >= poke[num].Location.X && mouse_x <= poke[num].Location.X + Constants.CARD_WEIGHT)
                {
                    firstShadowCardNum = num;
                }
                else
                {
                    for (int i = peoplelist[0].deck.Count - 2; i >= 0; i--)
                    {
                        num = peoplelist[0].deck[i].num;
                        if (mouse_x >= poke[num].Location.X && mouse_x <= poke[num].Location.X + Constants.CARD_INTERVAL)
                        {
                            firstShadowCardNum = num;
                            break;
                        }
                    }
                }
                isFirstCardShadow = false;
                thread.Start();  //开启变阴影的线程
            }
        }
        private void lb_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag && mouse_x >= poke[peoplelist[0].deck[0].num].Location.X && mouse_y >= poke[peoplelist[0].deck[peoplelist[0].deck.Count - 1].num].Location.Y)
            {
                //结束将牌改成阴影的线程
                thread.Abort();
                //将所有的手牌重新刷成原牌
                int count = peoplelist[0].deck.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    int num = peoplelist[0].deck[i].num;
                    string str = this.GetType().Assembly.Location;
                    str = str.Remove(str.IndexOf("poker.exe"));
                    poke[num].Image = Image.FromFile(str + "image\\poker\\" + num + ".jpg");
                }

                Label lbl = (Label)sender;
                Point q = new Point(lbl.Location.X, lbl.Location.Y);
                leftFlag = false;//释放鼠标后标注为false;
                mouseOff2 = new Point(e.X + q.X, e.Y + q.Y); //得到变量2的值
                if (mouseOff1.X > mouseOff2.X)
                {
                    Point temp = mouseOff1;
                    mouseOff1 = mouseOff2;
                    mouseOff2 = temp;
                }
                else if (mouseOff1.X == mouseOff2.X)
                    return;
                for (int i = count - 1; i >= 0; i--)
                {
                    int num = peoplelist[0].deck[i].num;
                    if (poke[num].Location.X < mouseOff2.X && poke[num].Location.X > mouseOff1.X || poke[num].Location.X < mouseOff1.X && mouseOff1.X - poke[num].Location.X < Constants.CARD_INTERVAL)
                    {
                        if (poke[num].Location.Y == postion.getPlayerCardY_F())
                        {
                            Point p = new Point(poke[num].Location.X, postion.getPlayerCardY_T());
                            poke[num].Location = p;
                        }
                        else if (poke[num].Location.Y == postion.getPlayerCardY_T())
                        {
                            Point p = new Point(poke[num].Location.X, postion.getPlayerCardY_F());
                            poke[num].Location = p;
                        }
                    }
                }
            }
        }
        //使牌变成阴影
        public void poke_change_shadow()
        {
            while (leftFlag == true)
            {
                //鼠标在手牌有效范围内
                if (mouse_x >= poke[peoplelist[0].deck[0].num].Location.X && mouse_x <= poke[peoplelist[0].deck[peoplelist[0].deck.Count - 1].num].Location.X + Constants.CARD_WEIGHT
                    && mouse_y <= postion.getPlayerCardY_F() + Constants.CARD_HEIGHT && mouse_y >= postion.getPlayerCardY_T())
                {
                    int count = peoplelist[0].deck.Count;
                    Point p_mouse = this.PointToClient(Control.MousePosition);   //获取相对于窗口的鼠标坐标
                    for (int i = count - 1; i >= 0; i--)
                    {
                        string str = this.GetType().Assembly.Location;
                        str = str.Remove(str.IndexOf("poker.exe"));
                        int num = peoplelist[0].deck[i].num;
                        if (poke[num].Location.X + Constants.CARD_INTERVAL > mouse_x && poke[num].Location.X < p_mouse.X
                            || poke[num].Location.X < mouse_x && poke[num].Location.X + Constants.CARD_INTERVAL > p_mouse.X)
                        {
                            if (num != firstShadowCardNum)
                            {
                                if (isFirstCardShadow == false)
                                {
                                    poke[firstShadowCardNum].Image = Image.FromFile(str + "image\\poker\\shadow_poke\\" + firstShadowCardNum + ".jpg");
                                    isFirstCardShadow = true;
                                }                        
                                poke[num].Image = Image.FromFile(str + "image\\poker\\shadow_poke\\" + num + ".jpg");                                
                            }                             
                        }
                        else
                        {
                            poke[num].Image = Image.FromFile(str + "image\\poker\\" + num + ".jpg");
                        }
                    }
                }
            }
        }
        //双击，牌全部下去
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = peoplelist[0].deck.Count - 1; i >= 0; i--)
            {
                int num = peoplelist[0].deck[i].num;
                if (poke[num].Location.Y == postion.getPlayerCardY_T())
                {
                    Point p = new Point(poke[num].Location.X, postion.getPlayerCardY_F());
                    poke[num].Location = p;
                }
            }
        }
        private void Game_Landlord_DoubleClick(object sender, EventArgs e)
        {
            for (int i = peoplelist[0].deck.Count - 1; i >= 0; i--)
            {
                int num = peoplelist[0].deck[i].num;
                if (poke[num].Location.Y == postion.getPlayerCardY_T())
                {
                    Point p = new Point(poke[num].Location.X, postion.getPlayerCardY_F());
                    poke[num].Location = p;
                }
            }
        }
        //重写双击事件
        public new event EventHandler DoubleClick;
        DateTime clickTime;
        bool isClicked = false;
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (isClicked)
            {
                TimeSpan span = DateTime.Now - clickTime;
                if (span.Milliseconds < SystemInformation.DoubleClickTime)
                {
                    DoubleClick(this, e);
                    isClicked = false;
                }
            }
            else
            {
                isClicked = true;
                clickTime = DateTime.Now;
            }
        }
        //四个人的图片更换
        private void down_Click(object sender, EventArgs e)
        {
            int a = ran.Next(1, 7);
            if (a == 2)
                fourPeopleSex[0] = false;
            else
                fourPeopleSex[0] = true;
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            down.Image = Image.FromFile(str + "image\\man" + a + ".png");
        }
        private void right_Click(object sender, EventArgs e)
        {
            int a = ran.Next(1, 7);
            if (a == 2)
                fourPeopleSex[1] = false;
            else
                fourPeopleSex[1] = true;
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            right.Image = Image.FromFile(str + "image\\man" + a + ".png");
        }
        private void up_Click(object sender, EventArgs e)
        {
            int a = ran.Next(1, 7);
            if (a == 2)
                fourPeopleSex[2] = false;
            else
                fourPeopleSex[2] = true;
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            up.Image = Image.FromFile(str + "image\\man" + a + ".png");
        }
        private void left_Click(object sender, EventArgs e)
        {
            int a = ran.Next(1, 7);
            if (a == 2)
                fourPeopleSex[3] = false;
            else
                fourPeopleSex[3] = true;
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            left.Image = Image.FromFile(str + "image\\man" + a + ".png");
        }
        //四个人的姓名更换
        private void downmes_MouseClick(object sender, MouseEventArgs e)
        {
            name[0] = randomName.getRandomName();
            if (isPokeDeal == true)
                mes[0].Text = name[0] + "\n剩余张数：\n";
            else
                mes[0].Text = name[0] + "\n";
            Label lablename = kuang.Controls[score_panel_score[0].Name] as Label;
            lablename.Text = name[0];
            lablename = end_score_panel.Controls[end_panel_score[0][0].Name] as Label;
            lablename.Text = name[0];
        }
        private void upmes_Click(object sender, EventArgs e)
        {
            name[2] = randomName.getRandomName();
            if (isPokeDeal == true)
                mes[2].Text = name[2] + "\n剩余张数：\n";
            else
                mes[2].Text = name[2] + "\n";
            Label lablename = kuang.Controls[score_panel_score[6].Name] as Label;
            lablename.Text = name[2];
            lablename = end_score_panel.Controls[end_panel_score[2][0].Name] as Label;
            lablename.Text = name[2];
        }
        private void rightmes_Click(object sender, EventArgs e)
        {
            name[1] = randomName.getRandomName();
            if (isPokeDeal == true)
                mes[1].Text = name[1] + "\n剩余张数：\n";
            else
                mes[1].Text = name[1] + "\n";
            Label lablename = kuang.Controls[score_panel_score[3].Name] as Label;
            lablename.Text = name[1];
            lablename = end_score_panel.Controls[end_panel_score[1][0].Name] as Label;
            lablename.Text = name[1];
        }
        private void leftmes_Click(object sender, EventArgs e)
        {
            name[3] = randomName.getRandomName();
            if (isPokeDeal == true)
                mes[3].Text = name[3] + "\n剩余张数：\n";
            else
                mes[3].Text = name[3] + "\n";
            Label lablename = kuang.Controls[score_panel_score[9].Name] as Label;
            lablename.Text = name[3];
            lablename = end_score_panel.Controls[end_panel_score[3][0].Name] as Label;
            lablename.Text = name[3];
        }
        #endregion

        /* 四个功能按钮实现-start */
        #region
        //机器人的点击事件
        private void robot_Click(object sender, EventArgs e)
        {
            if (isrobotflag == false && IsGameStart == true)
            {
                this.Controls.Add(lbl_roboted[1]);
                isrobotflag = true;
                show_fourpeoplechat(0, "宝宝要托管啦...");
                if (isTurnOvering == false) //不处于最后的完结关闭中
                    OutCard(0);
            }
            else
            {
                this.Controls.Remove(lbl_roboted[1]);
                isrobotflag = false;
            }
        }
        //排序按钮的点击事件
        private void order_Click(object sender, EventArgs e)
        {
            managedeck.ChangeOrder(ref peoplelist[0]);
            Point p = postion.DisplayCardPostion(0, peoplelist[0].deck.Count);
            for (int i = peoplelist[0].deck.Count - 1; i >= 0; i--)
            {
                int num = peoplelist[0].deck[i].num;
                this.Controls.Remove(poke[num]);
                poke[num].Location = p;
                this.Controls.Add(poke[num]);
                p.X = p.X - Constants.CARD_INTERVAL;
            }
        }
        //背景音乐播放的点击事件
        private void background_music_btn_Click(object sender, EventArgs e)
        {
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            if (backgroundMusicFlag == true)
            {
                backgound_music.Ctlcontrols.pause();
                backgroundMusicFlag = false;
                background_music_btn.Image = Image.FromFile(str + "image\\background_music_not_btn.png");
            }
            else
            {
                backgound_music.Ctlcontrols.play();
                backgroundMusicFlag = true;
                background_music_btn.Image = Image.FromFile(str + "image\\background_music_btn.png");
            }
        }
        //音效播放的点击事件
        private void sound_effect_btn_Click(object sender, EventArgs e)
        {
            string str = this.GetType().Assembly.Location;
            str = str.Remove(str.IndexOf("poker.exe"));
            if (Constants.soundEffectMusicFlag == true)
            {
                Constants.soundEffectMusicFlag = false;
                sound_effect_btn.Image = Image.FromFile(str + "image\\sound_effect_not_btn.png");
            }
            else
            {
                Constants.soundEffectMusicFlag = true;
                sound_effect_btn.Image = Image.FromFile(str + "image\\sound_effect_btn.png");
            }
        }
        #endregion

        /* 聊天框实现处-start */
        #region
        Boolean isTalk = false;//判断聊天框是否处于显示状态
        ToolTip tp_chat = new ToolTip();
        //聊天发送鼠标
        private void label1_Click(object sender, EventArgs e)
        {
            if (chat.Text.Trim() != String.Empty)
            {
                if (isTalk == false)    //如果当前不在说话
                {
                    String st = chat.Text;
                    st = st.Replace("\n", string.Empty).Replace("\r", string.Empty);
                    if (st.Trim() == "开启开发者模式")
                    {
                        isCreaterMode = true;
                        for (int i = 1; i < 4; i++)
                            Show_OverTurnThree(i);
                    }
                    else if (st.Trim() == "关闭开发者模式")
                    {
                        isCreaterMode = false;
                        for (int i = 1; i < 4; i++)
                            Show_ThreeComputer_Card(i);
                    }
                    if (st.Trim() == "开启自动测试")
                    {
                        Constants.IsAutoTest = true;
                    }
                    else if (st.Trim() == "关闭自动测试")
                    {
                        Constants.IsAutoTest = false;
                    }
                    show_fourpeoplechat(0, st);
                    chateach(st);
                    chat.Clear();
                }              
            }
        }
        //聊天发送键盘
        private void sent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                label1_Click(null, null);
            }
        }
        //互相聊天
        public void chateach(String talk)
        {
            switch (talk)
            {
                case "打的不错":
                    show_fourpeoplechat(ran.Next(1, 4), "是么？天真！");
                    break;
                case "赢定了":
                    show_fourpeoplechat(ran.Next(1, 4), "哼！天真！");
                    break;
                default:
                    show_fourpeoplechat(ran.Next(1, 4), "你好！");
                    break;
            }
        }

        //显示四人聊天
        System.Timers.Timer t;
        public void show_fourpeoplechat(int wh, String st)
        {
            Point[] p = new Point[4];
            p[0] = new Point(180, 388);
            p[1] = new Point(752, 168);
            p[2] = new Point(216, 32);
            p[3] = new Point(104, 163);
            for (int i = 0; i < 4; i++)
            {
                fourchat[i].Location = p[i];
            }       
            //显示说话气泡       
            fourchat[wh].Text = st;
            addFourchat(wh);
            //删除多余字符
            string s = chatlist.Text + name[wh] + "：" + st + "\r\n", ss = s;
            int rn_num = 0;
            int[] rn_pos = new int[100];
            while (true)
            {
                int x = s.IndexOf("\r\n");                   
                if (x < 0)
                    break;
                rn_pos[rn_num++] = x + rn_pos[rn_num] + 2;
                s = s.Substring(x + 2);                    
            }               
            if (rn_num > 12)
            {
                chatlist.Text = ss.Remove(0, rn_pos[rn_num - 12]);
            }
            else
            {
                chatlist.Text = ss;
            }              
            this.chat.Focus();
            int time_of_duration = st.Length * 200 > 3000 ? st.Length * 200 : 3000;
            t = new System.Timers.Timer(time_of_duration);
            if (wh == 0)
            {
                isTalk = true;
                tp_chat.ShowAlways = true;
                tp_chat.SetToolTip(chat, "说话过于频繁，请稍后重试！");
                tp_chat.SetToolTip(sent_button, "说话过于频繁，请稍后重试！");
            }
            t.AutoReset = false;
            t.Enabled = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                this.Controls.Remove(fourchat[wh]);
                this.Controls.Remove(fourchat[wh + 4]);
                if (wh == 0)
                    isTalk = false;
                tp_chat.ShowAlways = false;
            });
        }
        #endregion

        //关闭斗地主
        protected override void OnClosing(CancelEventArgs e)
        {
            //platform.Show();      
            sound_effect_btn = null;    
            this.Controls.Clear();
            Application.Exit();
        }
    }
}
