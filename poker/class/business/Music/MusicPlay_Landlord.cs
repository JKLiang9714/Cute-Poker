using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace poker
{
    class MusicPlay_Landlord
    {
        static private string str = "";
        private string START_URL = "";
        private string MUSIC_FORM = ".mp3";
        private string OTHER = "";
        private string MAN = "";
        private string WOMAN = "";
        private Random ran;
        MusicPlay musciplay;
        System.Timers.Timer t;

        //初始化构造函数
        public MusicPlay_Landlord()
        {
            //str = this.GetType().Assembly.Location;
            //str = str.Remove(str.IndexOf("poker.exe"));
            str = "";
            START_URL = str + "sound\\";
            OTHER = "specialsound\\";
            MAN = "man\\";
            WOMAN = "woman\\";
            ran = new Random();
            musciplay = new MusicPlay();
        }
        //播放音效的函数
        private void landlordMusicPlay(string s)
        {
            if (Constants.soundEffectMusicFlag == true)
                musciplay.Play(s);               
        }
              
        //胜利
        private void VictoryVoice()
        {           
            string s = START_URL + OTHER + "胜利_" + ran.Next(0, 2).ToString() + MUSIC_FORM;
            landlordMusicPlay(s);
        }
        //输牌
        private void LoseGame()
        {
            string s = START_URL + OTHER + "输牌" + MUSIC_FORM;
            landlordMusicPlay(s);
        }

        //飞机音
        private void PlaneVoice()
        {
            string s = START_URL + OTHER + "飞机音" + MUSIC_FORM;
            landlordMusicPlay(s);
        }
        //炸弹音
        private void BombVoice()
        {
            string s = START_URL + OTHER + "炸弹音" + MUSIC_FORM;
            landlordMusicPlay(s);
        }

        //连对
        private void NextTwoStraightVoice()
        {
            string s = START_URL + OTHER + "连对音" + MUSIC_FORM;
            landlordMusicPlay(s);
        }

        //玩家出牌的声音
        private void Player_OutCard_Voice(Boolean sex, List<Card> card)
        {
            CardStyle cs = CardStyle.judgeStyle(card);
            string news = "";
            //判断牌的大小
            string size = "";
            if (cs.size < 11)
                size = cs.size.ToString();
            switch (cs.size)
            {
                case 11: size = "J"; break;
                case 12: size = "Q"; break;
                case 13: size = "K"; break;
                case 14: size = "A"; break;
                case 15: size = "2"; break;
                case 16: size = "小王"; break;
                case 17: size = "大王"; break;
            }
            switch (cs.style)
            {
                case 1: news = size + "_0"; break;
                case 2: news = "对" + size + "_0"; break;
                case 3: news = "三个" + size; break;
                case 4: news = "三带一对_0"; break;
                case 5: news = "连对" + "_0"; break;
                case 6: news = "顺子" + "_0"; break;
                case 7: news = "飞机" + "_0"; break;
                case 8: news = "飞机" + "_0"; break;
                case 9: news = "炸弹" + "_0"; break;
                case 10: news = "王炸" + "_0"; break;
            }
            string s = START_URL + (sex == true ? MAN : WOMAN) + news + MUSIC_FORM;
            landlordMusicPlay(s);
            t = new System.Timers.Timer(500);//实例化Timer类，设置间隔时间为1000毫秒
            t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件
            t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                if (cs.style == 7 || cs.style == 8)
                {
                    PlaneVoice();
                }
                else if (cs.style == 9 || cs.style == 10)
                {
                    BombVoice();
                }
                else if (cs.style == 5 || cs.style == 6)
                {
                    NextTwoStraightVoice();
                }
            });         
        }
        //玩家抢牌的声音
        private void Player_FollowOutCard_Voice(Boolean sex)
        {
            string s = START_URL + (sex == true ? MAN : WOMAN) + "出牌_" + ran.Next(0, 3) + MUSIC_FORM;
            landlordMusicPlay(s);
        }

        //玩家叫地主的声音
        private void Player_CallLandlord_Voice(Boolean sex)
        {
            string s = START_URL + (sex == true ? MAN : WOMAN) + "叫地主_0" + MUSIC_FORM;
            landlordMusicPlay(s);
        }
        //玩家抢地主的声音
        private void Player_RobLandlord_Voice(Boolean sex)
        {
            string s = START_URL + (sex == true ? MAN : WOMAN) + "抢地主_" + ran.Next(0,3) + MUSIC_FORM;
            landlordMusicPlay(s);
        }       

        //玩家不叫的声音
        private void Player_NotScodeCall_Voice(Boolean sex)
        {
            string s = START_URL + (sex == true ? MAN : WOMAN) + "不叫_0" + MUSIC_FORM;
            landlordMusicPlay(s);
        }
        //玩家不抢的声音
        private void Player_NotRobLandlord_Voice(Boolean sex)
        {
            string s = START_URL + (sex == true ? MAN : WOMAN) + "不抢_0" + MUSIC_FORM;
            landlordMusicPlay(s);
        }

        //倍数翻倍的时候
        public void MultipleScore()
        {
            string s = START_URL + OTHER + "倍数翻倍" + MUSIC_FORM;
            landlordMusicPlay(s);
        }
        //剩两张牌
        private void Player_SurplusTwoCard_Voice(Boolean sex)
        {
            string s = START_URL + (sex == true ? MAN : WOMAN) + "我就两张牌了_0" + MUSIC_FORM;
            landlordMusicPlay(s);
            SpecialRemind();
        }
        //剩一张牌
        private void Player_SurplusOneCard_Voice(Boolean sex)
        {
            string s = START_URL + (sex == true ? MAN : WOMAN) + "我就一张牌了_0" + MUSIC_FORM;
            landlordMusicPlay(s);
            SpecialRemind();
        }

        //时间提醒
        public void TimeRemind()
        {
            string s = START_URL + OTHER + "时间提醒" + MUSIC_FORM;
            landlordMusicPlay(s);
        }
        //违反规则的时候
        public void BreakRule()
        {
            string s = START_URL + OTHER + "违反规则" + MUSIC_FORM;
            landlordMusicPlay(s);
        }
        //特别提醒的时候
        public void SpecialRemind()
        {
            t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为1000毫秒
            t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件
            t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                string s = START_URL + OTHER + "特别提醒" + MUSIC_FORM;
                landlordMusicPlay(s);
            });
        }
        //发牌音效
        public void DealCard()
        {
            t = new System.Timers.Timer(10);//实例化Timer类，设置间隔时间为1000毫秒
            t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件
            t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                string s = START_URL + OTHER + "发牌" + MUSIC_FORM;
                landlordMusicPlay(s);
            });
        }
        //开始游戏
        public void StartGame()
        {
            string s = START_URL + OTHER + "开始" + MUSIC_FORM;
            landlordMusicPlay(s);
        }
        //不叫地主或不抢地主
        public void NoCallScore_NoRobLandlord(int integration, Boolean sex)
        {
            if (integration == 0)//不叫地主
            {
                Player_NotScodeCall_Voice(sex);
            }
            else//不抢地主
            {
                Player_NotRobLandlord_Voice(sex);
            }
        }
        //叫分或抢地主
        public void CallScore_RobLandlord(int integration, Boolean sex)
        {
            if (integration == 0)//叫地主
            {
                Player_CallLandlord_Voice(sex);
            }
            else//抢地主
            {
                Player_RobLandlord_Voice(sex);
            }
        }
        //播放更新积分和卡组数量
        public void Update_Integration_DeckCount(int orign_integration, int integration, int deckCount, Boolean sex)
        {
            if (orign_integration != integration)   //倍数翻倍音效
            {
                t = new System.Timers.Timer(500);//实例化Timer类，设置间隔时间为1000毫秒
                t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)   
                t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件
                t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
                {
                    MultipleScore();
                });
            }
            t = new System.Timers.Timer(500);//实例化Timer类，设置间隔时间为1000毫秒
            t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件
            t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                if (deckCount == 2)  //提示就最后两张牌
                {
                    Player_SurplusTwoCard_Voice(sex);
                }
                else if (deckCount == 1) //提示就最后一张牌
                {
                    Player_SurplusOneCard_Voice(sex);
                }
            });
        }
        //出牌的声音，判断是出牌还是抢牌还是炸弹
        public void Player_OutorFollowCard_Voice(int who_out, int nowPerson, List<Card> []toOutCard, Boolean sex)
        {
             if (who_out == nowPerson || toOutCard[who_out].Count == 0)   //出牌声音
             {                
                 Player_OutCard_Voice(sex, toOutCard[nowPerson]);                             
             }
             else    //抢牌声音
             {
                 if (CardStyle.isBumb(toOutCard[nowPerson]) != 0 || CardStyle.isFourGhost(toOutCard[nowPerson]) == true)    //如果是炸弹
                 {
                     BombVoice();
                 }
                 else
                 {
                     Player_FollowOutCard_Voice(sex);
                    t = new System.Timers.Timer(500);//实例化Timer类，设置间隔时间为1000毫秒
                    t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)   
                    t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件
                    t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender2, System.Timers.ElapsedEventArgs e2)
                    {
                        if (CardStyle.isPlaneWithTwo(toOutCard[nowPerson]) != 0 || CardStyle.isPlane(toOutCard[nowPerson]) != 0)    //如果是飞机
                         {                         
                             PlaneVoice();                        
                         }
                        else if (CardStyle.isStraight(toOutCard[nowPerson]) != 0 || CardStyle.isNextTwo(toOutCard[nowPerson]) != 0) //如果是连对
                        {
                            NextTwoStraightVoice();
                        }
                    });
                }
             }
        }
        //玩家不出的声音
        public void Player_Refuse_Voice(Boolean sex)
        {
            string s = START_URL + (sex == true ? MAN : WOMAN) + "不出_" + ran.Next(0, 4) + MUSIC_FORM;
            landlordMusicPlay(s);
        }
        //播放最终胜利或者是失败的音效，true为胜利，false为失败
        public void Player_EndGame_Voice(Boolean winFlag)
        {
            t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为1000毫秒
            t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件
            t.Elapsed += new System.Timers.ElapsedEventHandler(delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                if (winFlag == true)   //如果玩家加分，则玩家获胜
                {
                    VictoryVoice();
                }
                else    //否则玩家失败
                {
                    LoseGame();
                }
            });
        }
    }
}
