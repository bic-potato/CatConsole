using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatConsole
{
    public class Cat
    {
        public Cat(int iD, string? name, int isInAtlas, string? nickname, int colorIndex, string? location, int sex, string? state, int isSterilize, DateTime? sterilizeDate, DateTime? birthday, string? description, int? character, string? firstUpdate, string? firstUpdatePoistion, string? relationship, string? more, string route, DateTime? adoptionTime, DateTime? deathTime, string? deathReason, int? audio, int? video)
        {
            ID=iD;
            Name=name;
            this.isInAtlas=isInAtlas;
            this.Nickname=nickname;
            ColorIndex=colorIndex;
            Location=location;
            Sex=sex;
            State=state;
            this.isSterilize=isSterilize;
            SterilizeDate=sterilizeDate;
            Birthday=birthday;
            Description=description;
            Character=character;
            FirstUpdate=firstUpdate;
            FirstUpdatePoistion=firstUpdatePoistion;
            Relationship=relationship;
            More=more;
            Route=route;
            AdoptionTime=adoptionTime;
            DeathTime=deathTime;
            DeathReason=deathReason;
            Audio=audio;
            Video=video;
        }


#nullable enable
        public int ID { get; set; }
        
        public string? Name { get; set; }
        
        public int isInAtlas { get; set; }

        public string? Nickname { get; set; }

        public int ColorIndex { get; set; }

        public string? Location { get; set; }
        
        public int Sex { get; set; }
        
        public string? State { get; set; }
        public int isSterilize { get; set; }

        public DateTime? SterilizeDate { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Description { get; set; }
        
        public int? Character { get; set; }
        
        public string? FirstUpdate { get; set; }
        
        // public DateTime? LastUpdated { get; set; }

        public string? FirstUpdatePoistion { get; set; }

        public string? Relationship { get; set; }

        public string? More { get; set; }

        public string Route { get; set; }

        public DateTime? AdoptionTime { get; set; }

        public DateTime? DeathTime { get; set; }

        public string? DeathReason { get; set; }

        public int? Audio { get; set; }

        public int? Video { get; set; }


    }
}
