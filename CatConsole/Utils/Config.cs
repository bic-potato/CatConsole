using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatConsole.Utils
{
    public class Config
    {
        public Hashtable ClassMap;
        public Hashtable Sex;
        public Hashtable Color;
        public Hashtable Sterilize;
        public Hashtable Character;
        public string Link;

        public Config()
        {
            ClassMap = new Hashtable();
            ClassMap.Add("ID", "ID");
            ClassMap.Add("Name", "名字");
            ClassMap.Add("isInAtlas", "是否写入图鉴");
            ClassMap.Add("Nickname", "昵称");
            ClassMap.Add("ColorIndex", "毛色");
            ClassMap.Add("Location", "出没地点");
            ClassMap.Add("Sex", "性别");
            ClassMap.Add("State", "状态");
            ClassMap.Add("isSterilize", "绝育");
            ClassMap.Add("SterilizeDate", "绝育时间");
            ClassMap.Add("Birthday", "出生年月");
            ClassMap.Add("Description", "外貌简述");
            ClassMap.Add("Character", "性格");
            ClassMap.Add("FirstUpdate", "第一次被目击时间");
            ClassMap.Add("FirstUpdatePoistion", "第一次被目击地点");
            ClassMap.Add("Relationship", "关系");
            ClassMap.Add("More", "更多");
            ClassMap.Add("Route", "线路");
            ClassMap.Add("AdoptionTime", "送养时间");
            ClassMap.Add("DeathTime", "离世时间");
            ClassMap.Add("DeathReason", "离世原因");
            ClassMap.Add("Audio", "是否加音频");
            ClassMap.Add("Video", "是否加视频");
            Sex = new();
            Sex.Add(0, "母");
            Sex.Add(1, "公");
            Sex.Add(-1, "未知");
            Sterilize = new();
            Sterilize.Add(0, "未绝育");
            Sterilize.Add(1, "绝育");
            Sterilize.Add(-1, "未知");
        if (!(File.Exists("./CatConsole.json")))
            {
                Character = new();
                Character.Add(6, "亲人可抱");
                Character.Add(5, "亲人不可抱 可摸");
                Character.Add(4, "薛定谔亲人");
                Character.Add(3, "吃东西时可以一直摸");
                Character.Add(2, "吃东西时可以摸一下");
                Character.Add(1, "怕人 安全距离1m以内");
                Character.Add(0, "怕人 安全距离1m以外");
                Character.Add(-1, "未知 数据缺失");

                Color = new();
                Color.Add(1,"狸花");
                Color.Add(2, "橘猫及橘白");
                Color.Add(3, "奶牛");
                Color.Add(4, "玳瑁及三花");
                Color.Add(5, "纯色");

                var json = new JsonConverter("//pku-lostangel.oss-cn-beijing.aliyuncs.com/", Color, Character);

           
                var result = JsonConvert.SerializeObject(json, Formatting.Indented);
                File.WriteAllText("CatConsole.json", result, Encoding.UTF8);
            } else
            {
                var result = File.ReadAllText("CatConsole.json");
                var jsonConventer = JsonConvert.DeserializeObject<JsonConverter>(result);
                if (jsonConventer != null)
                {
                    Character = jsonConventer.Character;
                    Color = jsonConventer.Color;
                    Link = jsonConventer.Link;
                }
                

            }

        }
    }

    public class JsonConverter
    {
        public string Link;
        public Hashtable Color;
        public Hashtable Character;

        public JsonConverter(string link, Hashtable color, Hashtable character)
        {
            this.Link=link;
            Color = color;
            Character=character;
        }
    }
}
