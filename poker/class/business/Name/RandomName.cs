﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker
{
    class RandomName
    {
        private static string[] name =
        {
            "傲娇妹",
            "橘子喵",
            "棒棒哒",
            "蔷薇花",
            "朕卖萌",
            "我在笑",
            "倒霉熊",
            "乖乖猪",
            "起司猫",
            "派大星",
            "伯贤儿",
            "皮卡丘",
            "赖好人",
            "小呆瓜",
            "小丸子",
            "萌趴趴",
            "萌王殿",
            "小乌龟",
            "大头虾",
            "美少女",
            "笨小孩",
            "鱼丸",
            "张益达",
            "嘟嘟噜",
            "软萌女",
            "小能手",
            "凯蒂猫",
            "萌阿萌",
            "宫略萌",
            "娇羞君",
            "欧尼酱",
            "奶油",
            "棒棒糖",
            "肩头猫",
            "咪咕猫",
            "塔塔猫",
            "土豆喵",
            "软妹",
            "可爱酱",
            "萌面鹿",
            "小耳朵",
            "萌辣",
            "谜兔",
            "猫＞ω＜",
            "软喵メ",
            "小仙女",
            "寿司",
            "蒸汽机",
            "甜筒猫",
            "宽笑颜",
            "妖神丶",
            "醉慵容",
            "浅月流歌",
            "倾酒涟漪",
            "朝歌夜弦",
            "轻吟浅唱",
            "暖画凉筝",
            "故人远",
            "花开花落",
            "伤丶流离",
            "空忧径",
            "鱼雁音书",
            "胭脂易染",
            "清扬婉兮",
            "繁华终殇",
            "红颜一笑",
            "与君共枕",
            "焚音",
            "玉笙寒",
            "百年新娘",
            "浮生梦",
            "三生缘",
            "吾皇",
            "莫执意",
            "情未深",
            "青山长河",
            "石板幽径",
            "媚笙",
            "血染墨冢",
            "弦断心凉",
            "烟雨凡馨",
            "花月夜",
            "苍老年华",
            "初南溪",
            "似是而非",
            "白展堂。",
            "青丝暮雪",
            "箴言",
            "倾城红颜",
            "繁华落尽",
            "仗剑行诗",
            "韵华镜池",
            "落花独立",
            "许烟雨",
            "西阁的酒",
            "苦巷深桥",
            "千鸢锁画",
            "借花献人",
            "故城",
            "一世红妆"
        };
        public static int getNameLength()
        {
            int count = 0;
            foreach (var item in name)
            {
                count++;
            }
            return count;
        }

        Random ran;

        public RandomName()
        {
            ran = new Random();
        }

        /// <summary>
        /// 获取随机姓名
        /// </summary>
        /// <returns></returns>
        public string getRandomName()
        {
            int index = ran.Next(0, getNameLength());
            return name[index].ToString();
        }
    }
}
