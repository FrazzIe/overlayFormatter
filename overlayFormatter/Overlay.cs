using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overlayFormatter
{
    public enum Zone
    {
        ZONE_TORSO = 0,
        ZONE_HEAD = 1,
        ZONE_LEFT_ARM = 2,
        ZONE_RIGHT_ARM = 3,
        ZONE_LEFT_LEG = 4,
        ZONE_RIGHT_LEG = 5,
        ZONE_UNKNOWN = 6,
        ZONE_NONE = 7,
    };
    public enum Faction
    {
        MICHAEL = 0,
        FRANKLIN = 1,
        TREVOR = 2,
        FM = 4,
    }
    public enum Gender
    {
        GENDER_MALE = 0,
        GENDER_FEMALE = 1,
        GENDER_DONTCARE = 2,
    }

    public enum Type
    {
        TYPE_TATTOO = 0,
        TYPE_BADGE = 1,
        TYPE_HAIR = 2,
    }

    public class Overlay
    {
        public string name;
        public int zone;
        public int type;
        public int faction;
        public int gender;
        public string label;
        public string collection;

        public Overlay(string name, Zone zone, Type type, Faction faction, Gender gender)
        {
            this.name = name;
            this.zone = (int)zone;
            this.type = (int)type;
            this.faction = (int)faction;
            this.gender = (int)gender;
        }
    }
}
