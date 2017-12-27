using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    //计算各个控件在界面的位置
    class PeopleCardPostion
    {
        private int parentWeight;//卡牌的宽度
        private int parentHeight;//卡牌的高度
        private int borderHeight;//向上空出的距离

        private int player_one_card_y;
        //946, 607
        //962, 646
        //65, 92
        //无参构造函数
        public PeopleCardPostion() { }
        //有参构造函数
        //根据父窗体的长宽，计算相应控件的显示位置
        public PeopleCardPostion(int parentW, int parentH, int borderW)
        {
            this.parentWeight = parentW;
            this.parentHeight = parentH;
            this.borderHeight = borderW;
            player_one_card_y = (int)Math.Round(parentH - 1.0 - Constants.CARD_HEIGHT - Constants.CARD_INTERVAL * 2) + borderW;
        }

        public int getPlayerCardY_F()//获取play1未出牌时y的坐标
        {
            return player_one_card_y;
        }

        public int getPlayerCardY_T()//获取play1出牌时y的坐标
        {
            return player_one_card_y - Constants.CARD_INTERVAL;
        }

        //返回各家显示卡牌的初始位置
        //player是玩家位置0,1,2,3代表下，右，上，左
        //num表示当前玩家的卡牌数量
        //isSelected代表下家的时候这张牌是否为选中状态
        public Point DisplayCardPostion(int player, int num = 1, bool isSelected = false)
        {
            int totalW = Constants.CARD_WEIGHT + (num - 1) * Constants.CARD_INTERVAL;
            int totalH = Constants.CARD_HEIGHT;
            int x = 0, y = 0;   //最终形成的相对于父控件的坐标
            switch (player)
            {
                case 0:
                    x = (int)Math.Round((parentWeight - 1) / 2.0 + totalW / 2.0 - Constants.CARD_WEIGHT);
                    if (isSelected == true)
                    {
                        y = getPlayerCardY_T();
                    }
                    else
                    {
                        y = getPlayerCardY_F();
                    }
                    break;
                case 1:
                    x = 785;
                    y = 210 + borderHeight;
                    break;
                case 2:
                    x = 426;
                    y = 25 + borderHeight;
                    break;
                case 3:
                    x = 120;
                    y = 210 + borderHeight;
                    break;
            }
            Point p = new Point(x, y);
            return p;
        }

        //返回各家显示出的卡牌的初始位置
        //player是玩家位置0,1,2,3代表下，右，上，左
        //num表示当前玩家的卡牌数量
        public Point OutCardPostion(int player, int num)
        {
            int totalW = Constants.CARD_WEIGHT + (num - 1) * Constants.CARD_INTERVAL;
            int totalH = Constants.CARD_HEIGHT;
            int x = 0, y = 0;   //最终形成的相对于父控件的坐标
            switch (player)
            {
                case 0:
                    x = (int)Math.Round((parentWeight - 1) / 2.0 + totalW / 2.0 - Constants.CARD_WEIGHT);
                    y = 350 + borderHeight;
                    break;
                case 1:
                    x = 685;
                    y = 220 + borderHeight;
                    break;
                case 2:
                    x = (int)Math.Round((parentWeight - 1) / 2.0 + totalW / 2.0 - Constants.CARD_WEIGHT);
                    y = 165 + borderHeight;
                    break;
                case 3:
                    x = num * 15 + 200;
                    y = 220 + borderHeight;
                    break;
            }
            Point p = new Point(x, y);
            return p;
        }

        //翻转时各家牌的坐标
        public Point OverTurnCardPostion(int player, int num)
        {
            int totalW = Constants.CARD_WEIGHT + (num - 1) * Constants.CARD_INTERVAL;
            int totalH = Constants.CARD_HEIGHT;
            int x = 0, y = 0;   //最终形成的相对于父控件的坐标
            switch (player)
            {
                case 0:
                    x = (int)Math.Round((parentWeight - 1) / 2.0 + totalW / 2.0 - Constants.CARD_WEIGHT);
                    y = 350 + borderHeight;
                    break;
                case 1:
                    x = 680;
                    y = 300 + borderHeight;
                    break;
                case 2:
                    x = (int)Math.Round((parentWeight - 1) / 2.0 + totalW / 2.0 - Constants.CARD_WEIGHT);
                    y = 25 + borderHeight;
                    break;
                case 3:
                    x = 170 + Constants.CARD_INTERVAL * (num - 1);
                    y = 180 + borderHeight;
                    break;
            }
            Point p = new Point(x, y);
            return p;
        }
    }
}
