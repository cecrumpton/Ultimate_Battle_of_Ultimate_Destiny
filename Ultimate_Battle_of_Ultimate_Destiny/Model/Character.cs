using System.Collections.Generic;

namespace Ultimate_Battle_of_Ultimate_Destiny.Model
{
    public class Character
    {
        public Character(string name, KeyValuePair<string, string> type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }
        public KeyValuePair<string, string> Type { get; set; }
    }
}
