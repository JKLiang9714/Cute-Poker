using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    public interface GameMode              //游戏模式
    {
        void Shuffle(ref PeopleList[] pl, ref CardPile cp);     //发牌
        bool OutCard(List<Card> prevCard, List<Card> myCard);             //判断能否出牌   
        void ChangeOrder(ref List<Card> deck, int orderMode);   //调整用户手中牌的排序
        List<Card> ComupterRemind(List<Card> deck, List<Card> prevCard);       //提示和托管
        List<Card> PlayerRemind(List<Card> deck, List<Card> prevCard, ref int remindStyle);  //人的提示
        List<Card> Robort_IntelligentOutCard(ref List<Card>[] people, int whosland, int whoOut, int nowPerson, List<Card> prevCard);    //机器人
    }
}
