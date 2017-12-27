using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    class Remind_LandLord
    {
        //判断能不能出炸弹和四王
        public static List<Card> OutBumb(List<Card> deck, int pos)
        {
            List<Card> reminderList = new List<Card>();
            int i, j, k;
            bool flag = false;
            for (j = pos; j >= 0; j--)
            {
                if (deck[j].cardnum < 4) continue;
                int len = deck[j].cardnum;
                for (k = 0; k < len; k++)       //我出普通炸弹
                    reminderList.Add(deck[j--]);
                flag = true;
                break;
            }
            if (!flag)
            {
                int count1 = 0, count2 = 0, index1 = 0, index2 = 0;
                for (i = 0; i < deck.Count - 1; i++)
                {
                    if (deck[i].size == 16)
                    {
                        count1++; index1 = i;
                    }
                    if (deck[i].size == 17)
                    {
                        count2++; index2 = i;
                    }
                }
                if (count1 == 2 && count2 == 2)     //我出四大天王
                {
                    reminderList.Add(deck[index1]);
                    reminderList.Add(deck[index1 - 1]);
                    reminderList.Add(deck[index2]);
                    reminderList.Add(deck[index2 - 1]);
                }
            }
            return reminderList;
        }
        //出炸弹的提示（打别人的炸弹）
        public static List<Card> Bomb_Remind(ref List<Card> deck, CardStyle cs)
        {
            List<Card> reminderList = new List<Card>();
            int i, j, k;
            bool flag = false;
            for (i = deck.Count - 1; i >= 0; i--)
            {
                if (deck[i].cardnum < cs.cardlength) continue;
                if (deck[i].size > cs.size || deck[i].cardnum > cs.cardlength)   //我出炸弹
                {
                    flag = true;
                    for (k = i, j = 0; j < deck[k].cardnum; j++)
                        reminderList.Add(deck[i--]);
                    break;
                }
            }
            if (!flag)
            {
                int count1 = 0, count2 = 0, index1 = 0, index2 = 0;
                for (i = 0; i < deck.Count - 1; i++)
                {
                    if (deck[i].size == 16)
                    {
                        count1++; index1 = i;
                    }
                    if (deck[i].size == 17)
                    {
                        count2++; index2 = i;
                    }
                }
                if (count1 == 2 && count2 == 2)     //我出四大天王
                {
                    reminderList.Add(deck[index1]);
                    reminderList.Add(deck[index1 - 1]);
                    reminderList.Add(deck[index2]);
                    reminderList.Add(deck[index2 - 1]);
                }
            }
            return reminderList;
        }
        //出单牌的提示，不可以拆炸弹，但可以拆2，小王，大王
        public static List<Card> Single_Remind(ref List<Card> deck, CardStyle cs, bool remindflag)
        {
            List<Card> reminderList = new List<Card>();
            int i;
            bool flag = false;
            for (i = deck.Count - 1; i >= 0; i--)
            {
                if (deck[i].cardnum > 1) break;
                if (deck[i].size > cs.size)   //我出单牌
                {
                    reminderList.Add(deck[i]);
                    flag = true;
                    break;
                }
            }
            if (!flag)//找2，小王，大王拆
            {
                for (int j = i; j >= 0; j--)
                {
                    if (deck[j].cardnum != 4 && deck[j].size >= 15 && deck[j].size > cs.size)   //不拆4个头，（拆不是4个头的炸还是炸）我出单牌
                    {
                        reminderList.Add(deck[j]);
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag && remindflag)
            {
                reminderList = OutBumb(deck, i);   //我出炸弹
            }
            return reminderList;
        }
        //出对子的提示，不可以拆炸弹，但可以拆2
        public static List<Card> Two_Remind(ref List<Card> deck, CardStyle cs, bool remindflag)
        {
            List<Card> reminderList = new List<Card>();
            int i, j;
            bool flag = false;
            for (i = deck.Count - 1; i >= 0; i--)
            {
                if (deck[i].cardnum < 2) continue;
                if (deck[i].cardnum > 2) break;
                if (deck[i].size > cs.size)   //我出对子
                {
                    for (j = 0; j < 2; j++)
                        reminderList.Add(deck[i--]);
                    flag = true;
                    break;
                }
            }
            if (!flag && cs.size < 15)//找2，小王，大王拆
            {
                for (j = i; j >= 0; j--)
                {
                    if ((deck[j].cardnum == 3 || deck[j].cardnum > 5) && deck[j].size >= 15)   //不拆4个头，（拆不是4个头的炸还是炸）我出单牌
                    {
                        reminderList.Add(deck[j]);
                        reminderList.Add(deck[j - 1]);
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag && remindflag)
            {
                reminderList = OutBumb(deck, i);   //我出炸弹
            }
            return reminderList;
        }
        //出三不带的提示
        public static List<Card> Three_Remind(ref List<Card> deck, CardStyle cs, bool remindflag)
        {
            List<Card> reminderList = new List<Card>();
            int i, j;
            bool flag = false;
            for (i = deck.Count - 1; i >= 0; i--)
            {
                if (deck[i].cardnum < 3) continue;
                if (deck[i].cardnum > 3) break;
                if (deck[i].size > cs.size)   //我出三不带
                {
                    for (j = 0; j < 3; j++)
                        reminderList.Add(deck[i--]);
                    flag = true;
                    break;
                }
            }
            if (!flag && remindflag)
            {
                reminderList = OutBumb(deck, i);   //我出炸弹
            }
            return reminderList;
        }
        //出三带二的提示//先塞3个，再塞2个
        public static List<Card> ThreeWithTwo_Remind(ref List<Card> deck, CardStyle cs, bool remindflag)
        {
            List<Card> reminderList = new List<Card>();
            int i = deck.Count - 1, j = deck.Count - 1;
            bool flag = false;
            for (i = deck.Count - 1; i >= 0; i--)
            {
                if (deck[i].cardnum < 2) continue;
                else if (deck[i].cardnum > 2) break;
                else
                {
                    flag = true;       //找到对子
                    break;
                }
            }
            if (flag)
            {
                flag = false;
                for (j = i; j >= 0; j--)
                {
                    if (deck[j].cardnum < 3) continue;
                    if (deck[j].cardnum > 3) break;
                    if (deck[j].size > cs.size)   //找到三个头
                    {
                        flag = true;
                        for (int k = 0; k < 3; k++) reminderList.Add(deck[j--]);
                        for (int k = 0; k < 2; k++) reminderList.Add(deck[i--]);
                        break;
                    }
                }
            }
            if (!flag && remindflag)
            {
                reminderList = OutBumb(deck, j);   //我出炸弹
            }
            return reminderList;
        }
        //出连对的提示
        public static List<Card> NextTwo_Remind(ref List<Card> deck, CardStyle cs, bool remindflag)
        {
            List<Card> reminderList = new List<Card>();
            int i, j;
            int length = cs.cardlength;
            bool flag = false;
            for (i = deck.Count - 1; i >= 0; i--)
            {
                if (deck[i].cardnum < 2) continue;
                if (deck[i].cardnum > 2) break;
                //找到连对：先比它大、它长度最后是对子且是连对且符合要求（小于等于A）
                if (deck[i].size > cs.size && i - length + 1 >= 0 && deck[i - length + 1].cardnum == 2 && deck[i - length + 1].size == deck[i].size + length / 2 - 1 && deck[i - length + 1].size <= 14)
                {
                    flag = true;
                    for (j = i; j > i - length; j--)
                        reminderList.Add(deck[j]);
                    break;
                }
            }
            if (!flag && remindflag)
            {
                reminderList = OutBumb(deck, i);   //我出炸弹
            }
            return reminderList;
        }
        //出顺子的提示
        public static List<Card> Straight_Remind(ref List<Card> deck, CardStyle cs, bool remindflag)
        {
            List<Card> reminderList = new List<Card>();
            int i, j;
            int length = cs.cardlength;
            bool flag = false;
            for (i = deck.Count - 1; i >= 0; i--)
            {
                if (deck[i].cardnum > 1) break;
                //找到顺子：先比它大、它长度最后是单支且是顺子且符合要求（小于等于A）
                if (deck[i].size > cs.size && i - length + 1 >= 0 && deck[i - length + 1].cardnum == 1 && deck[i - length + 1].size == deck[i].size + length - 1 && deck[i - length + 1].size <= 14)
                {
                    flag = true;
                    for (j = i; j > i - length; j--)
                        reminderList.Add(deck[j]);
                    break;
                }
            }
            if (!flag && remindflag)
            {
                reminderList = OutBumb(deck, i);   //我出炸弹
            }
            return reminderList;
        }      
        //出飞机不带的提示
        public static List<Card> Plane_Remind(ref List<Card> deck, CardStyle cs, bool remindflag)
        {
            List<Card> reminderList = new List<Card>();
            int i, j;
            int length = cs.cardlength;
            bool flag = false;
            for (i = deck.Count - 1; i >= 0; i--)
            {
                if (deck[i].cardnum < 3) continue;
                if (deck[i].cardnum > 3) break;
                //找到飞机不带：先比它大、它长度最后是三个头且是飞机且符合要求（小于等于A）
                if (deck[i].size > cs.size && i - length + 1 >= 0 && deck[i - length + 1].cardnum == 3 && deck[i - length + 1].size == deck[i].size + length / 3 - 1 && deck[i - length + 1].size <= 14)
                {
                    flag = true;
                    for (j = i; j > i - length; j--)
                        reminderList.Add(deck[j]);
                    break;
                }
            }
            if (!flag && remindflag)
            {
                reminderList = OutBumb(deck, i);   //我出炸弹
            }
            return reminderList;
        }
        //出飞机连对的提示
        public static List<Card> PlaneWithTwo_Remind(ref List<Card> deck, CardStyle cs, bool remindflag)
        {
            List<Card> reminderList = new List<Card>();
            int i, j, num = 0;
            int length = cs.cardlength;
            for (i = deck.Count - 1; i >= 0; i--)
            {
                if (deck[i].cardnum < 2) continue;
                else if (deck[i].cardnum > 2) break;
                else if (i - length / 5 * 2 + 1 >= 0 && deck[i - length / 5 * 2 + 1].cardnum == 2)
                {
                    num++;
                    break;
                }
            }
            j = i;
            if (num != 0)       //找到对应个数的对子
            {
                num = 0;
                for (; j >= 0; j--)
                {
                    if (deck[j].cardnum < 3) continue;
                    if (deck[j].cardnum > 3) break;
                    //找到飞机不带：先比它大、它长度最后是三个头且是飞机且符合要求（小于等于A）
                    int len = length / 5 * 3;
                    int len2 = length / 5 * 2;
                    if (deck[j].size > cs.size && j - len + 1 >= 0 && deck[j - len + 1].cardnum == 3 && deck[j - len + 1].size == deck[j].size + len / 3 - 1 && deck[j - len + 1].size <= 14)
                    {
                        num = 1;
                        for (int k = 0; k < len; k++) reminderList.Add(deck[j--]);
                        for (int k = 0; k < len2; k++) reminderList.Add(deck[i--]);
                        break;
                    }
                }
            }
            if (num == 0 && remindflag)
            {
                reminderList = OutBumb(deck, j);   //我出炸弹
            }
            return reminderList;
        }
    }
}
