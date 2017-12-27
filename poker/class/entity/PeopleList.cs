using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    public class CardPile
    {
        public List<Card> deck = new List<Card>();
    }

    public class PeopleList : CardPile         //人手中牌的类
    {
        private int integration;            //积分
        private int orderMode;              //牌序方式，0是从大到小排，1是按牌的个数排
        private bool remind_flag;           //是否点了提示
        private bool landlord_flag;         //是否是地主
        private int outCard_flag;          //出过牌的次数
        private bool win_flag;              //是否出完牌了
        private List<Card> result = new List<Card>();       //能出的牌
        private int remind_size;        //提示出牌的种类

        public int Integration
        {
            get
            {
                return integration;
            }

            set
            {
                integration = value;
            }
        }
        public bool Landlord_flag
        {
            get
            {
                return landlord_flag;
            }

            set
            {
                landlord_flag = value;
            }
        }
        public int OrderMode
        {
            get
            {
                return orderMode;
            }

            set
            {
                orderMode = value;
            }
        }
        public int OutCard_flag
        {
            get
            {
                return outCard_flag;
            }

            set
            {
                outCard_flag = value;
            }
        }
        public bool Win_flag
        {
            get
            {
                return win_flag;
            }

            set
            {
                win_flag = value;
            }
        }
        public bool Remind_flag
        {
            get
            {
                return remind_flag;
            }

            set
            {
                remind_flag = value;
            }
        }
        public List<Card> Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }
        public int Remind_size
        {
            get
            {
                return remind_size;
            }

            set
            {
                remind_size = value;
            }
        }

        public int remindStyle;

        //初始化
        public PeopleList()
        {
            Integration = 0;
            Init_AfterTurn();
        }
        //一局以后清空的函数
        public void Init_AfterTurn()
        {
            remindStyle = 1;
            result = null;
            orderMode = 1;
            remind_flag = false;
            landlord_flag = false;          //当前为农民
            outCard_flag = 0;               //当前没出牌
            win_flag = false;               //初始化为输
            this.deck.Clear();          //清空当前卡组
        }

        //判断是否为第一次提示
        public Boolean JudgeFirstRemind()
        {
            return !remind_flag;
        }
        public void UpdateHintResult()  //更改hintResult
        {
            remind_flag = false;
            result = null;
            remindStyle = 1;
        }
    }
}
