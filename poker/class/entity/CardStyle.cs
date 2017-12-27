using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    public class CardStyle          //出牌形式
    {
        public int style;      //-1表示当前没有牌，0不能出，1单支，2对子，3三不带，4三带，5连对，6顺子，7飞机不带，8飞机带两对，9炸弹，10四大天王
        public int size;       //相同牌的第一张
        public int cardlength;    //出牌的数量
        public CardStyle(int style, int size, int cardlength)
        {
            this.style = style;
            this.size = size;
            this.cardlength = cardlength;
        }
        public CardStyle()
        {
            style = 0;
            size = 0;
            cardlength = 0;
        }

        //判断是不是对子，返回对子的第一张牌
        public static int isTwo(List<Card> card)
        {
            if (card[0].size != card[1].size) return 0;
            return card[0].size;
        }
        //判断是不是三不带，返回第一张牌
        public static int isThree(List<Card> card)
        {
            if (card[0].size != card[card.Count - 1].size) return 0;
            else return card[0].size;
        }
        //判断是不是三带二，返回三个头的第一张牌
        public static int isThreeTakeTwo(List<Card> card)
        {
            int length = card.Count;
            if (length != 5) return 0;
            int num1 = 0, num2 = 0;
            for (int i = 0; i < length; i++)
            {
                if (card[i].size == card[0].size) num1++;
                else if (card[i].size == card[length - 1].size) num2++;
            }
            if (num1 == 2 && num2 == 3) return card[length - 1].size;
            else if (num1 == 3 && num2 == 2) return card[0].size;
            else return 0;
        }
        //判断是不是连对，返回第一个对子的第一张牌
        public static int isNextTwo(List<Card> card)
        {
            int length = card.Count;
            if (length < 6 || length % 2 != 0 || card[length - 1].size > 14) return 0;
            int[] num = new int[18];
            for (int i = 0; i < 18; i++)
                num[i] = 0;
            for (int i = 0; i < length; i++)
                num[card[i].size]++;
            int begin = 18, end = 0;
            for (int i = 0; i < 18; i++)
            {
                if (num[i] == 1 || num[i] > 2) return 0;
                if (num[i] == 2)
                {
                    if (i < begin) begin = i;
                    if (i > end) end = i;
                }
            }
            if (length != (end - begin + 1) * 2) return 0;
            return begin;
        }
        //判断是不是顺子，返回顺子的第一张牌
        public static int isStraight(List<Card> card)
        {
            int length = card.Count;
            if (length < 5 || card[length - 1].size > 14) return 0;
            for (int i = 1; i < length; i++)
            {
                if (card[i].size + 1 != card[i - 1].size)
                    return 0;
            }
            return card[length - 1].size;
        }
        //判断是不是飞机不带，返回飞机的第一张牌
        public static int isPlane(List<Card> card)
        {
            int length = card.Count;
            if (length % 3 != 0 || length < 6 || card[length - 1].size > 14) return 0;
            int[] num = new int[18];
            for (int i = 0; i < 18; i++)
                num[i] = 0;
            for (int i = 0; i < length; i++)
                num[card[i].size]++;
            int begin = 18, end = 0;
            for (int i = 0; i < 18; i++)
            {
                if (num[i] == 1 || num[i] == 2 || num[i] > 3) return 0;
                if (num[i] == 3)
                {
                    if (i < begin) begin = i;
                    if (i > end) end = i;
                }
            }
            if (length != (end - begin + 1) * 3) return 0;
            return begin;
        }
        //判断是不是飞机带两对，返回第一个三个头的第一张牌
        public static int isPlaneWithTwo(List<Card> card)
        {
            int length = card.Count;
            if (length < 10 || length % 5 != 0) return 0;
            int[] num = new int[18];
            for (int i = 0; i < 18; i++)
                num[i] = 0;
            for (int i = 0; i < length; i++)
                num[card[i].size]++;
            int begin = 18, end = 0;
            for (int i = 0; i < 18; i++)
            {
                if (num[i] == 1 || num[i] > 3) return 0;
                if (num[i] == 3)
                {
                    if (i < begin) begin = i;
                    if (i > end) end = i;
                }
            }
            int count = end - begin + 1;
            if (length != count * 5 || end > 14) return 0;
            return begin;
        }
        //判断是不是炸弹，返回炸弹的第一张牌
        public static int isBumb(List<Card> card)
        {
            if (card.Count < 4) return 0;
            else if (card[0].size != card[card.Count - 1].size) return 0;
            else return card[0].size;
        }
        //判断是不是四大天王
        public static bool isFourGhost(List<Card> card)
        {
            card.Sort(Card.CampareModeCardSize); //按牌的大小排序
            if (card.Count == 4 && card[0].size == 17 && card[1].size == 17 && card[2].size == 16 && card[3].size == 16) return true;
            else return false;
        }

        //判断出牌的种类
        public static CardStyle judgeStyle(List<Card> list)
        { //-1表示没有，0出错，1单支，2对子，3三不带，4三带二，5连对，6顺子，7飞机不带，8飞机带两对，9炸弹，10四大天王           
            CardStyle cardstyle = new CardStyle();
            if (list == null || list.Count == 0)
            {
                cardstyle.style = -1;
                return cardstyle;
            }
            list.Sort(Card.CampareModeCardSize); //按牌的大小排序
            cardstyle.cardlength = list.Count;      //保存出的牌的长度
            switch (list.Count)
            {
                case 1: cardstyle.style = 1; cardstyle.size = list[0].size; return cardstyle;
                case 2:
                    if (isTwo(list) > 0)
                    {
                        cardstyle.style = 2;
                        cardstyle.size = isTwo(list);
                    }
                    return cardstyle;
                case 3:
                    if (isThree(list) > 0)
                    {
                        cardstyle.style = 3;
                        cardstyle.size = isThree(list);
                    }
                    return cardstyle;
            }
            //判断三带二
            int x = isThreeTakeTwo(list);
            if (x > 0)
            {
                cardstyle.style = 4;
                cardstyle.size = x;
                return cardstyle;
            }

            //判断连对
            x = isNextTwo(list);
            if (x > 0)
            {
                cardstyle.style = 5;
                cardstyle.size = x;
                return cardstyle;
            }

            //判断顺子
            x = isStraight(list);
            if (x > 0)
            {
                cardstyle.style = 6;
                cardstyle.size = x;
                return cardstyle;
            }

            //判断飞机不带
            x = isPlane(list);
            if (x > 0)
            {
                cardstyle.style = 7;
                cardstyle.size = x;
                return cardstyle;
            }

            //判断飞机带两对
            x = isPlaneWithTwo(list);
            if (x > 0)
            {
                cardstyle.style = 8;
                cardstyle.size = x;
                return cardstyle;
            }

            //判断炸弹
            x = isBumb(list);
            if (x > 0)
            {
                cardstyle.style = 9;
                cardstyle.size = x;
                return cardstyle;
            }

            //判断四大天王
            x = isBumb(list);
            if (x > 0)
            {
                cardstyle.style = 10;
                return cardstyle;
            }
            return cardstyle;
        }
    }
}
