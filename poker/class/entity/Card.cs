using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    public enum CardType
    {
        Spade, Hearts, Club, Diamond   //黑桃，红桃，梅花，方块，0123
    }

    public class Card        //牌的实体类
    {
        public int size;       //牌的大小
        public CardType color;     //牌的花色
        public int num;       //对应标签编号
        public int cardnum;     //相同牌的个数

        public Card(int size, int color, int num)
        {
            this.size = size;
            this.color = (CardType)color;
            this.num = num;
            this.cardnum = 0;
        }
        public Card(int size, int color, int num, int cardnum)
        {
            this.size = size;
            this.color = (CardType)color;
            this.num = num;
            this.cardnum = cardnum;
        }
        Card() { }
        //重写equals方法，否则会调用父类方法只删除同一对象的元素
        public override bool Equals(object obj) 
        {
            Card card = (Card)obj;
            return this.num == card.num && this.cardnum == card.cardnum && this.size == card.size && this.color == card.color;
        }
        //重载hashCode方法。否则用Card对象作为Key放到HashMap中时，还会出现问题。
        public override int GetHashCode()
        {
            return num;
        }
        //复制一个新的card链表
        public static List<Card> CopyListCard(List<Card> people)    
        {
            List<Card> TCard = new List<Card>();
            for (int i = 0; i < people.Count; i++)
            {
                Card tcard = new Card(people[i].size, (int)people[i].color, people[i].num, people[i].cardnum);
                TCard.Add(tcard);
            }
            return TCard;
        }
        //按牌的大小从大到小排，再按牌的花色排
        public static int CampareModeCardSize(Card c1, Card c2)
        {
            if (c1.size == c2.size)
            {
                return c1.color > c2.color ? 1 : -1;
            }
            else
            {
                return c1.size < c2.size ? 1 : -1;
            }
        }
        //让牌按牌的数量从大到小排，再按牌的大小从大到小排，再按牌的花色排
        public static int CampareModeCardNum(Card c1, Card c2)
        {
            if (c1.cardnum == c2.cardnum)
            {
                if (c1.size == c2.size)
                {                  
                    return c1.color > c2.color ? 1 : -1;
                }
                else
                {
                    return c1.size < c2.size ? 1 : -1;
                }
            }
            else
            {
                return c1.cardnum < c2.cardnum ? 1 : -1;
            }
        }
    }
}
