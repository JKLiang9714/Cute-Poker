using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    //斗地主 
    public class LandLord_GameMode : GameMode
    {
        //将整数转化为Card类型
        public static Card ConvertCard(int num)
        {
            int x = (num > 54 ? num - 54 : num);
            int size = (x > 52 ? x - 37 : (x - 1) / 4 + 1);   //x - 52 + 13 + 2
            if (size <= 2)       //如果是1和2，加13；如果是大王小王，加2
            {
                size += 13;
            }
            return new Card(size, (x - 1) % 4, num);
        }
        //发牌
        public void Shuffle(ref PeopleList[] pl, ref CardPile cp)
        {
            int[] num = new int[108];
            for (int i = 0; i < 108; i++)       //生成108张牌
            {
                num[i] = i + 1;
            }
            Random random = new Random();
            for (int i = 0; i < 108; i++)       //随机打乱牌序
            {
                int x, t = random.Next(108);
                x = num[i];
                num[i] = num[t];
                num[t] = x;
            }
            for (int i = 0; i < 108; i++)       //发牌
            {
                Card card = ConvertCard(num[i]);
                if (i < 100)
                {
                    pl[i % 4].deck.Add(card);     //对应人的队列增加相应卡牌
                }
                else
                {
                    cp.deck.Add(card);        //底牌先放到牌堆中
                }
            }
            cp.deck.Sort(Card.CampareModeCardNum);
        }
        //改变牌的顺序，0是让牌按牌的大小排，1是让牌按的数量排
        public void ChangeOrder(ref List<Card> deck, int orderMode)
        {
            if (orderMode == 0)
            {
                deck.Sort(Card.CampareModeCardSize);
            }
            else
            {
                deck.Sort(Card.CampareModeCardNum);
            }
        }
          
        //判断出牌是否合法
        public bool OutCard(List<Card> prevCard, List<Card> myCard)
        {
            if (prevCard == null || prevCard.Count == 0)   //如果上家为空，且自家牌正确，则可以打出
            {
                CardStyle Cs2 = new CardStyle();
                myCard.Sort(Card.CampareModeCardSize);
                Cs2 = CardStyle.judgeStyle(myCard);
                if (Cs2.size != 0)
                    return true;
                else
                    return false;
            }
            int prevSize = prevCard.Count;
            int mySize = myCard.Count;
            prevCard.Sort(Card.CampareModeCardSize);
            myCard.Sort(Card.CampareModeCardSize);
            CardStyle cs1 = new CardStyle();
            CardStyle cs2 = new CardStyle();
            cs1 = CardStyle.judgeStyle(prevCard);
            cs2 = CardStyle.judgeStyle(myCard);
            if (cs2.style == 0)         //自家出的牌错了
                return false;
            if (cs1.style == 10)        //上家出四大天王
                return false;
            else if (cs1.style == 9)    //上家出炸弹
            {
                if (cs2.style == 9)
                    return cs1.cardlength < cs2.cardlength || cs1.cardlength == cs2.cardlength && cs1.size < cs2.size;
                else
                    return false;
            }
            else                        //上家出其他牌
            {
                if (cs2.style >= 9)
                    return true;
                if (cs1.style == cs2.style && cs1.cardlength == cs2.cardlength)
                {
                    return cs1.size < cs2.size;
                }
                else
                {
                    return false;
                }
            }
        }

        //玩家游戏提示，记录上次找的牌
        public List<Card> PlayerRemind(List<Card> deck, List<Card> prevCard, ref int remindStyle)
        {
            this.ChangeOrder(ref deck, 1);          //将原卡组按卡牌数量进行排序
            CardStyle cs = new CardStyle();
            cs = CardStyle.judgeStyle(prevCard);
            if (cs.style == -1)     //如果想出什么牌就出什么牌
            {
                return People_Free_Remind(deck, ref remindStyle);
            }
            else
            {
                return FindRemind_CrushPrevCard(deck, cs, false, true);  //要不住，不提示炸弹
            }
        }
        //玩家想出什么牌出什么牌的提示
        public List<Card> People_Free_Remind(List<Card> deck, ref int remindStyle)
        {
            List<Card> reminderList = new List<Card>();
            int length = deck.Count;
            CardStyle css;
            this.ChangeOrder(ref deck, 1);          //将原卡组按卡牌数量进行排序

            if (remindStyle == 1)    //飞机带几对
            {
                for (int i = length / 5 * 5; i >= 10 && reminderList.Count == 0; i -= 5)
                {
                    css = new CardStyle(8, 0, i);
                    reminderList = FindRemind_CrushPrevCard(deck, css, false, true);
                }
                remindStyle = 2;
            }
            if (remindStyle == 2 && reminderList.Count == 0)  //飞机不带
            {
                for (int i = length / 3 * 3; i >= 6 && reminderList.Count == 0; i -= 3)
                {
                    css = new CardStyle(7, 0, i);
                    reminderList = FindRemind_CrushPrevCard(deck, css, false, true);
                }
                remindStyle = 3;
            }
            if (remindStyle == 3 && reminderList.Count == 0)   //顺子
            {
                for (int i = 11; i >= 5 && reminderList.Count == 0; i--)   //3-A共12张
                {
                    css = new CardStyle(6, 0, i);
                    reminderList = FindRemind_CrushPrevCard(deck, css, false, true);
                }
                remindStyle = 4;
            }
            if (remindStyle == 4 && reminderList.Count == 0)   //连对
            {
                for (int i = 22; i >= 6 && reminderList.Count == 0; i -= 2)   //对3-对A共24张
                {
                    css = new CardStyle(5, 0, i);
                    reminderList = FindRemind_CrushPrevCard(deck, css, false, true);
                }
                remindStyle = 5;
            }
            if (remindStyle == 5 && reminderList.Count == 0)   //三带二
            {
                css = new CardStyle(4, 0, 5);
                reminderList = FindRemind_CrushPrevCard(deck, css, false, true);
                remindStyle = 6;
            }
            if (remindStyle == 6 && reminderList.Count == 0)   //三不带
            {
                css = new CardStyle(3, 0, 3);
                reminderList = FindRemind_CrushPrevCard(deck, css, false, true);
                remindStyle = 7;
            }
            if (remindStyle == 7 && reminderList.Count == 0)   //对子
            {
                css = new CardStyle(2, 0, 2);
                reminderList = FindRemind_CrushPrevCard(deck, css, false, true);
                remindStyle = 8;
            }
            if (remindStyle == 8 && reminderList.Count == 0)   //单张
            {
                css = new CardStyle(1, 0, 1);
                reminderList = FindRemind_CrushPrevCard(deck, css, false, true);
                remindStyle = 9;
            }
            if (remindStyle == 9 && reminderList.Count == 0)   //全部不能出，出炸弹
            {
                for (int i = 8; i >= 4; i--)
                {
                    css = new CardStyle(9, 0, i);
                    reminderList = FindRemind_CrushPrevCard(deck, css, false, true);
                }
                remindStyle = 10;
            }
            return reminderList;
        }
        
        //电脑游戏提示
        public List<Card> ComupterRemind(List<Card> deck, List<Card> prevCard)
        {          
            this.ChangeOrder(ref deck, 1);          //将原卡组按卡牌数量进行排序
            CardStyle cs = new CardStyle();
            cs = CardStyle.judgeStyle(prevCard);          
            if (cs.style == -1)     //如果想出什么牌就出什么牌
            {
                return Robort_Free_Remind(deck, 1);
            }   
            else
            {
                return FindRemind_CrushPrevCard(deck, cs, true, false);  //要不住，提示炸弹
            }       
        }
        //电脑想出什么牌就出什么牌  //outCardStyle为1正常出牌，为2为尽量出小的
        public List<Card> Robort_Free_Remind(List<Card> deck, int outCardStyle)
        {
            List<Card> reminderList = new List<Card>();
            int length = deck.Count;
            CardStyle css;
            this.ChangeOrder(ref deck, 1);          //将原卡组按卡牌数量进行排序

            if (length >= 10)    //飞机带几对
            {
                for (int i = length / 5 * 5; i >= 10 && reminderList.Count == 0; i -= 5)
                {
                    css = new CardStyle(8, 0, i);
                    reminderList = FindRemind_CrushPrevCard(deck, css, false, false);
                }
            }
            if (reminderList.Count == 0 && length >= 6)  //飞机不带
            {
                for (int i = length / 3 * 3; i >= 6 && reminderList.Count == 0; i -= 3)
                {
                    css = new CardStyle(7, 0, i);
                    reminderList = FindRemind_CrushPrevCard(deck, css, false, false);
                }
            }
            if (reminderList.Count == 0 && length >= 5)   //智能顺子
            {
                for (int i = 11; i >= 5 && reminderList.Count == 0; i--)   //3-A共12张
                {
                    css = new CardStyle(6, 0, i);
                    reminderList = StraightOrTwo_CanDepatch_Remind(ref deck, css, false, 1, false);
                }
            }
            if (reminderList.Count == 0 && length >= 6)   //智能连对
            {
                for (int i = 22; i >= 6 && reminderList.Count == 0; i -= 2)   //对3-对A共24张
                {
                    css = new CardStyle(5, 0, i);
                    reminderList = StraightOrTwo_CanDepatch_Remind(ref deck, css, false, 2, false);
                }
            }
            if (reminderList.Count == 0 && length >= 5)   //三带二
            {
                css = new CardStyle(4, 0, 5);
                reminderList = FindRemind_CrushPrevCard(deck, css, false, false);
                if (reminderList.Count != 0 && (reminderList[0].size >= 15 || reminderList[4].size >= 15) && AfterThisOut_CanOutAllCard(deck, reminderList) != true)//不出3个2，不带对2，对小鬼，对老鬼
                {//除非出完这些牌可以一次性出完其他牌，否则不允许出       
                    reminderList.Clear();
                }
            }
            if (reminderList.Count == 0 && length >= 3)   //三不带
            {
                css = new CardStyle(3, 0, 3);
                reminderList = FindRemind_CrushPrevCard(deck, css, false, false);
                if (reminderList.Count != 0 && reminderList[0].size >= 15 && AfterThisOut_CanOutAllCard(deck, reminderList) != true)//不要打出三个2
                {//除非出完这些牌可以一次性出完其他牌，否则不允许出       
                    reminderList.Clear();
                }
            }
            if (reminderList.Count == 0 && length >= 2)   //对子
            {
                css = new CardStyle(2, 0, 2);
                reminderList = FindRemind_CrushPrevCard(deck, css, false, false);
                if (reminderList.Count != 0 && reminderList[0].size >= 15 && AfterThisOut_CanOutAllCard(deck, reminderList) != true)//不要单独打出对2，对小鬼，对老鬼
                {//除非出完这些牌可以一次性出完其他牌，否则不允许出       
                    reminderList.Clear();   
                }
                if (outCardStyle == 2 && reminderList.Count != 0)  //如果尽量出小的，出对子和出单支时，尽量挑小的出
                {
                    css = new CardStyle(1, 0, 1);
                    List<Card> treminderList = FindRemind_CrushPrevCard(deck, css, false, false);
                    if (treminderList.Count != 0 && reminderList[0].size > treminderList[0].size)
                    {
                        reminderList = treminderList;
                    }
                }
            }
            if (reminderList.Count == 0 && length >= 1)   //单张
            {
                css = new CardStyle(1, 0, 1);
                reminderList = FindRemind_CrushPrevCard(deck, css, false, false);
            }
            if (reminderList.Count == 0 && length >= 4)   //全部不能出，出炸弹
            {
                for (int i = 8; i >= 4; i--)
                {
                    css = new CardStyle(9, 0, i);
                    reminderList = FindRemind_CrushPrevCard(deck, css, false, false);
                }
            }
            return reminderList;
        }
        //根据CardStyle进行提示打别人的牌，canOutBoom表示是否可出炸弹，isPlayer表示是否是玩家
        public List<Card> FindRemind_CrushPrevCard(List<Card> deck, CardStyle cs, Boolean canOutBoom, Boolean isPlayer)
        {
            List<Card> reminderList = new List<Card>();
            //上家出什么调用相应的提示
            switch (cs.style)
            {
                case 10://上家出四大天王
                    break;
                case 9://上家出炸弹
                    reminderList = Remind_LandLord.Bomb_Remind(ref deck, cs);
                    break;
                case 1://上家出单牌
                    reminderList = Remind_LandLord.Single_Remind(ref deck, cs, canOutBoom);
                    break;
                case 2://上家出对子
                    reminderList = Remind_LandLord.Two_Remind(ref deck, cs, canOutBoom);
                    break;
                case 3://上家出三不带
                    reminderList = Remind_LandLord.Three_Remind(ref deck, cs, canOutBoom);
                    break;
                case 4://上家出三带二
                    reminderList = Remind_LandLord.ThreeWithTwo_Remind(ref deck, cs, canOutBoom);
                    break;
                case 5://上家出连对
                    if (canOutBoom == true)
                        reminderList = StraightOrTwo_CanDepatch_Remind(ref deck, cs, canOutBoom, 2, isPlayer);
                    else
                        reminderList = Remind_LandLord.NextTwo_Remind(ref deck, cs, canOutBoom);
                    break;
                case 6://上家出顺子 
                    if (canOutBoom == true)
                        reminderList = StraightOrTwo_CanDepatch_Remind(ref deck, cs, canOutBoom, 1, isPlayer);
                    else
                        reminderList = Remind_LandLord.Straight_Remind(ref deck, cs, canOutBoom);
                    break;
                case 7://上家出飞机不带
                    reminderList = Remind_LandLord.Plane_Remind(ref deck, cs, canOutBoom);
                    break;
                case 8://上家出飞机带两对
                    reminderList = Remind_LandLord.PlaneWithTwo_Remind(ref deck, cs, canOutBoom);
                    break;
            }
            return reminderList;
        }

        private List<Card> [] NextRemindCard = new List<Card>[3]; //记录农民提示出牌情况，0代表当前出牌的人，1代表下一个
        //机器人智能出牌，包含各种情况
        public List<Card> Robort_IntelligentOutCard(ref List<Card>[] people, int whosland, int whoOut, int nowPerson, List<Card> prevCard)
        {
            //landlord：谁是地主    nowPerson：谁要出牌     whoOut：上家出牌的是谁
            List<Card> tCard = new List<Card>();
            if (nowPerson == whosland)  //如果是地主
            {//不出3个2，对小鬼，对老鬼，对2 //打单支可拆2，A，小鬼，老鬼
                tCard = ComupterRemind(people[nowPerson], prevCard);
            }
            else    //如果是农民
            {
                //先判断自己能不能出完，在判断后面的农民能不能出完
                Boolean canNextPeopleOutAllCard = false;
                int i, j;
                for (i = nowPerson, j = 0; i != whosland; i = (i + 1) % 4, j++)
                {   //将当前出牌者和地主前的农民要出的牌保存起来
                    NextRemindCard[j] = ComupterRemind(people[i], prevCard);
                    if (NextRemindCard[j].Count == people[i].Count) //如果有人能出完
                        break;
                }
                if (i == nowPerson || NextRemindCard[0].Count == 0)  //如果自己能出完或者自己什么牌都出不了
                {
                    tCard = NextRemindCard[0];  //出完自己的牌
                }
                else if (i != whosland) //如果后面有农民能出完
                {                   
                    if (whoOut == nowPerson) //如果是自己出牌，想办法放农民跑
                    {
                        CardStyle cs = CardStyle.judgeStyle(NextRemindCard[j]);
                        //-1表示当前没有牌，0不能出，1单支，2对子，3三不带，4三带，5连对，6顺子，7飞机不带，8飞机带两对，9炸弹，10四大天王
                        int[] map_CardStyle_remindStyle = new int[] {0, 8, 7, 6, 5, 4, 3, 2, 1, 0, 0}; //cs的size和提示顺序的对应关系
                        if (cs.style == 1)
                        {
                            ChangeOrder(ref people[nowPerson], 0);
                            tCard.Add(people[nowPerson][people[nowPerson].Count - 1]);
                            ChangeOrder(ref people[nowPerson], 1);
                        }
                        else
                        {
                            int x = map_CardStyle_remindStyle[cs.size];
                            if (x < 1)  //下家为炸，自己想出啥出啥
                            {
                                tCard = NextRemindCard[0];
                            }
                            else
                            {
                                tCard = People_Free_Remind(people[nowPerson], ref x);//找同类型自己能出的最小的牌
                                CardStyle css = CardStyle.judgeStyle(tCard);
                                if (css.style != cs.style || css.size >= cs.style)  //不能出证明放不了
                                {
                                    tCard = NextRemindCard[0];
                                }
                            }
                        }                       
                    }//if (whoOut == nowPerson) //如果是自己出牌，想办法放农民跑
                }//else if (i != whosland) //如果后面有农民能出完
                else //如果没有人能出完
                {
                    if (whoOut == nowPerson)//农民自由出牌
                    {
                        tCard = Farmer_free_outCard(ref people, whosland, nowPerson);//放人走 > 卡位
                    }
                    else //农民打农民或农民打地主
                    {
                        //打农民不炸 > 原来的算法 > 卡位
                        if (whoOut != whosland)
                        {
                            List<Card> canOutAllCard = Robort_Free_Remind(people[whoOut], 1);
                            if (canOutAllCard.Count == people[whoOut].Count)//农民打农民
                            {//如果打农民时，当前出牌农民出完牌后可以一次走，就不打
                                canNextPeopleOutAllCard = true;
                            }
                            else if (NextRemindCard[0].Count <= 2 && NextRemindCard[0][0].size >= 15)
                            {//如果用大小王打其他农民就算了
                                canNextPeopleOutAllCard = true;
                            }
                        }                        
                        if (canNextPeopleOutAllCard != true)    //如果没有其它玩家可以出完牌
                        {
                            tCard = Farmer_CrushCard(ref people, whosland, whoOut, nowPerson, prevCard);
                        }
                    }
                }//判断农民有没有人可以走                                            
            }
            return tCard;
        }
        //农民自由出牌
        private List<Card> Farmer_free_outCard(ref List<Card>[] people, int whosland, int nowPerson) 
        {
            List<Card> tRemind = new List<Card>();
            if ((nowPerson + 1) % 4 == whosland)
                tRemind = Robort_Free_Remind(people[nowPerson], 1);
            else
                tRemind = Robort_Free_Remind(people[nowPerson], 2);
            return tRemind;
        }
        //农民打别人的牌//打农民不炸 > 原来的算法 > 卡位
        private List<Card> Farmer_CrushCard(ref List<Card>[] people, int whosland, int whoOut, int nowPerson, List<Card> prevCard)
        {
            //landlord：谁是地主    nowPerson：谁要出牌     whoOut：上家出牌的是谁
            List<Card> tCard = new List<Card>();
            if ((CardStyle.isBumb(NextRemindCard[0]) != 0 || NextRemindCard[0].Count == 0) && whoOut != whosland)   //上家出牌的是自己人而且我只能出炸弹，则不出牌
                return tCard;   //返回空值            
            //如果下家是地主，且上家出的牌是单牌，则需要守门
            if ((nowPerson + 1) % 4 == whosland)     
            {
                if (prevCard.Count == 1)
                {
                    List<Card> tempCard2 = new List<Card>();
                    tempCard2 = Robort_Single_Remind(ref people[nowPerson], prevCard, true);
                    return tempCard2;
                }
            }
            //判断自己是否一定要出炸弹
            int i, j;
            for (i = nowPerson, j = 0; i != whosland; i = (i + 1) % 4, j++)
            {
                if (CardStyle.isBumb(NextRemindCard[j]) == 0 && NextRemindCard[j].Count != 0) //如果该家出的是普通牌
                    break;
            }
            if (i == nowPerson || i == whosland)  //如果我家出的是普通牌，或者没有一家出的不是普通牌
            {
                return NextRemindCard[0];
            }
            else    //后面有农民可以要住，我就不用炸
            {
                return tCard;
            }       
        }
        //如果下家是地主，且上家出的牌是单牌，则需要守门
        private List<Card> Robort_Single_Remind(ref List<Card> deck, List<Card> prevCard, bool remindflag)
        {
            List<Card> reminderList = new List<Card>();
            ChangeOrder(ref deck, 0);
            int i;
            bool flag = false;
            for (i = deck.Count - 1; i >= 0; i--) //如果没找到能出的单支
            {
                if (deck[i].size < 11 || deck[i].cardnum > 3 || deck[i].size <= prevCard[0].size) continue;
                reminderList.Add(deck[i]);
                flag = true;
                break;
            }
            if (!flag && remindflag)
            {
                reminderList = NextRemindCard[0];   //如果封不住，出原来的牌
            }
            return reminderList;
        }

        //判断出完这次牌后，下一次能不能一次出完
        private Boolean AfterThisOut_CanOutAllCard(List<Card> people, List<Card> thisCard)
        {
            List<Card> tDeck = Card.CopyListCard(people);
            for (int i = 0; i < thisCard.Count; i++)
                tDeck.Remove(thisCard[i]);
            List<Card> tCard = Robort_Free_Remind(tDeck, 1);
            if (tCard.Count == tDeck.Count)    //如果该玩家提示的牌可全部出完
                return true;
            else
                return false;
        }

        //出顺子或连对的提示（可拆版本）//必须先按牌从大到小排序
        private List<Card> StraightOrTwo_CanDepatch_Remind(ref List<Card> deck, CardStyle cs, bool remindflag, int straightOrTwo, bool isPlayer)
        {
            List<Card> reminderList = new List<Card>();
            ChangeOrder(ref deck, 0);
            int length = cs.cardlength, i = deck.Count - 1, recordi = 0;
            bool isRobertCanOut = true;
            while (i >= 0)
            {
                for (; i >= 0; i -= deck[i].cardnum)
                {
                    if (deck[i].size > cs.size && deck[i].cardnum >= straightOrTwo)
                    {
                        int j = i, len = 1, k = j - deck[j].cardnum;
                        //没有到底，数量超过cardnum，长度要够上一家出的牌，要连在一起，最高是A
                        while (k >= 0 && deck[k].cardnum >= straightOrTwo && len < length && deck[j].size == deck[k].size - 1 && deck[k].size <= 14)
                        {
                            len++;
                            j = k;
                            k = j - deck[j].cardnum;
                        }
                        if (len == length)  //如果找到
                        {
                            int not_single_num = 0, single_num = 0;
                            for (k = i; k >= j; k = k - deck[k].cardnum)
                            {
                                single_num++;
                                for (int num = 0; num < straightOrTwo; num++) //加入出牌队列
                                {
                                    reminderList.Add(deck[k - num]);
                                }
                                if (deck[k].cardnum == straightOrTwo + 1)  //判断是否拆了不是单支或是顺子
                                {
                                    not_single_num++;
                                }
                                if (deck[k].cardnum >= 4 && deck[k].cardnum - straightOrTwo < 4)   //判断有没有拆炸弹
                                    isRobertCanOut = false;
                            }
                            //如果拆的个数超过1个，不让拆
                            if (single_num < not_single_num * 2 + 1)
                            {
                                isRobertCanOut = false;
                            }
                            recordi = i - deck[i].cardnum;  //保存当前位置，可以让它找下一个位置
                            break;
                        }
                        else
                        {
                            i = j;
                        }
                    }//if (deck[i].size >= cs.size && deck[i].cardnum >= cardnum)                   
                }//for (; i >= 0; i -= deck[i].cardnum)
                if (isRobertCanOut == true || isPlayer == true || i < 0)  //如果是玩家，或者电脑可以拆的版本或者根本找不到，则跳出
                    break;
                else
                {
                    i = recordi;
                    reminderList.Clear();
                }
            }
            if (i < 0)  //如果没找到就清空
                reminderList.Clear();
            ChangeOrder(ref deck, 1);
            if (reminderList.Count == 0 && remindflag)
            {
                reminderList = Remind_LandLord.OutBumb(deck, deck.Count - 1);   //我出炸弹
            }
            return reminderList;
        }
    }
}
