using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    class ManagePeoplelist
    {
        private GameMode gamemode;

        //构造操作人卡组队列的类
        public ManagePeoplelist(GameMode gamemode)    
        {
            this.gamemode = gamemode;
        }

        //更新卡组中的牌的数量
        private void UpdateCardnum(ref PeopleList people)
        {
            ChangeOrder(ref people, 0);
            for (int j = 0; j < people.deck.Count; j++)    //改变每个牌的数量
            {
                int cardnum = 1, k = j;
                while (j < people.deck.Count - 1 && people.deck[j].size == people.deck[j + 1].size)
                {
                    cardnum++;
                    j++;
                }
                for (; k <= j; k++)
                {
                    people.deck[k].cardnum = cardnum;
                }
            }
            if (people.OrderMode == 0)     //如果当前是按个数排则重新排序
            {
                ChangeOrder(ref people, 1);
            }
        }

        //给四个玩家发牌，剩余的八张牌放在牌堆中
        public void Shuffle(ref PeopleList[] pl, ref CardPile cp)
        {
            gamemode.Shuffle(ref pl, ref cp);
            for (int i = 0; i < 4; i++)
            {
                UpdateCardnum(ref pl[i]);
            }
        }

        //给对应人队列加牌
        public void DealPeople(ref PeopleList people, List<Card> card)
        {
            for (int j = 0; j < card.Count; j++)//加牌
                people.deck.Add(card[j]);
            UpdateCardnum(ref people);  //更新卡的卡牌数量
        }

        //给地主8张牌，并且更新地主状态
        public void DealLandlord(ref PeopleList[] people, CardPile cp, int whosland)
        {
            DealPeople(ref people[whosland], cp.deck);  //给地主发牌
            people[whosland].Landlord_flag = true;      //改变四个人的地主状态
        }

        //判断是否能出牌     
        public bool OutCard(List<Card> prevCard, List<Card> myCard)
        {
            return gamemode.OutCard(prevCard, myCard);
        }
       
        //减卡并计分，如果获胜返回true，如果没有返回false，并播放倍数翻倍和提示牌没有的音效
        public bool TrueOutCard(ref PeopleList people, List<Card> card, ref int integration)
        {
            people.Remind_flag = false;
            for (int i = 0; i < card.Count; i++)    //删除牌
            {
                for (int j = 0; j < people.deck.Count; j++)
                    if (card[i].num == people.deck[j].num)
                    {
                        people.deck.Remove(people.deck[j]);
                    }
            }           
            UpdateCardnum(ref people);  //更新卡组数量            

            people.OutCard_flag++;
            if (card[0].size == card[card.Count - 1].size && card.Count >= 6 && card.Count <= 7)
                integration *= 2;
            else if (card[0].size == card[card.Count - 1].size && card.Count == 8 || (card.Count == 4 && card[0].size == 17 && card[1].size == 17 && card[2].size == 16 && card[3].size == 16))
                integration *= 3;
           
            //判断是否赢
            if (people.deck.Count == 0)
            {
                people.Win_flag = true;
                return true;
            }
            return false;
        }

        //玩家提示出卡
        public List<Card> Remind(ref PeopleList people, List<Card> prevCard)
        {
            List<Card> TCard = new List<Card>();
            for (int j = 0; j < 4; j++)
            {
                TCard = Card.CopyListCard(people.deck);
            }
            if (people.Remind_flag == false)
            {
                if (prevCard != null)
                    people.Result = gamemode.ComupterRemind(TCard, prevCard);
                else
                    people.Result = gamemode.PlayerRemind(TCard, prevCard, ref people.remindStyle);
                if (people.Result.Count != 0)
                    people.Remind_flag = true;
            }
            else
            {
                if (people.remindStyle == 1)    //如果当前有人出牌
                    people.Result = gamemode.ComupterRemind(TCard, people.Result);
                else
                {
                    people.Result = gamemode.PlayerRemind(TCard, people.Result, ref people.remindStyle);  //按类型提示出牌
                    if (people.Result.Count == 0)   //如果当前没牌提示，则提示下一种类型的牌
                    {
                        people.Remind_flag = false;
                        Remind(ref people, prevCard);
                    }                                        
                }             
            }
            return people.Result;
        }

        //机器人（托管）出卡
        public List<Card> IntelligentOutCard(ref PeopleList[] people, int landlord, int whoOut, int nowPerson, List<Card> prevCard)
        {
            List<Card> []TCard = new List<Card>[4];
            for (int j = 0; j < 4; j++)
            {
                TCard[j] = Card.CopyListCard(people[j].deck);
                TCard[j].Sort(Card.CampareModeCardNum); //在计算相应算法之前，必须先按照牌的数量进行排序
            }           
            return gamemode.Robort_IntelligentOutCard(ref TCard, landlord, whoOut, nowPerson, prevCard);
        }

        //结算分数  
        public void SettlementScore(ref PeopleList[] people, int integration)
        {
            bool flag = false;   //false是地主赢，true是农民赢
            int i, j, length = people.Length;
            for (i = 0; i < length; i++)
                if (people[i].Win_flag == true)
                    flag = people[i].Landlord_flag == true ? false : true;
            if (flag == false)  //地主赢的
            {
                for (j = 0; j < length; j++)
                    if (people[j].Landlord_flag == false && people[j].OutCard_flag > 0) //是农民且出过一次牌
                        break;
                if (j == length)        //没有一个农民出过牌，底分翻倍
                    integration *= 2;
            }
            else    //农民赢的
            {
                for (j = 0; j < length; j++)
                    if (people[j].Landlord_flag == true && people[j].OutCard_flag <= 1) //地主只出过一手牌或不出牌
                        break;
                if (j < length)     //地主只出过一手牌或不出牌，底分翻倍
                    integration *= 2;
            }

            //每家结算分数
            if (flag == true)   //农民获胜，地主减分，农民加分
                integration *= -1;
            for (i = 0; i < length; i++)
            {
                if (people[i].Landlord_flag == true)        //地主获胜
                    people[i].Integration += 3 * integration;
                else
                    people[i].Integration -= integration;
            }
        }

        //改变牌序
        public void ChangeOrder(ref PeopleList people)
        {
            gamemode.ChangeOrder(ref people.deck, people.OrderMode);
            people.OrderMode = 1 - people.OrderMode;
        }
        //改变牌序
        public void ChangeOrder(ref PeopleList people, int orderMode)
        {
            gamemode.ChangeOrder(ref people.deck, orderMode);
        }

    }
}
