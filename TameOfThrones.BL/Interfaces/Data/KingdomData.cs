namespace TameOfThrones.BL.Interfaces.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Definations for common constants related to kingdoms
    /// </summary>
    public static class KingdomData
    {
        public const string NoneValue = "None";
        public const string SpaceKingdomRulerName = "King Shan";

        // Kingdom names
        public const string AirKingdomName = "Air";
        public const string IceKingdomName = "Ice";
        public const string LandKingdomName = "Land";
        public const string FireKingdomName = "Fire";
        public const string WaterKingdomName = "Water";
        public const string SpaceKingdomName = "Space";

        // Emblems for public processing purpose
        public const string AirKingdomEmblem = "owl";
        public const string IceKingdomEmblem = "mammoth";
        public const string LandKingdomEmblem = "panda";
        public const string FireKingdomEmblem = "dragon";
        public const string WaterKingdomEmblem = "octopus";
        public const string SpaceKingdomEmblem = "gorilla";

        // Ruler names for public processing purpose
        public const string AirKingdomRuler = AirKingdomName;
        public const string IceKingdomRuler = IceKingdomName;
        public const string LandKingdomRuler = LandKingdomName;
        public const string FireKingdomRuler = FireKingdomName;
        public const string WaterKingdomRuler = WaterKingdomName;
        public const string SpaceKingdomRuler = SpaceKingdomRulerName;

        public static readonly IList<string> KingdomsNameList = new List<string>()
        { AirKingdomName, FireKingdomName, IceKingdomName, LandKingdomName, SpaceKingdomName, WaterKingdomName };
        
        // Count of Total Kingdoms
        public static readonly int TotalKingdomsCount = KingdomsNameList.Count;

        public static string RandomMessage => GetRandomMessage();

        // Collection of predefined meaages
        private static readonly string[] MessagesCollection = new string[]
        {
            "Summer is coming", "a1d22n333a4444p", "oaaawaala", "zmzmzmzaztzozh", "Go risk it all", "Let's swing the sword together",
            "Die or play the tame of thrones", "Ahoy! Fight for me with men and money", "Drag on Martin!",
            "When you play the tame of thrones you win or you die.", "What could we say to the Lord of Death? Game on?",
            "Turn us away and we will burn you ﬁrst", "Death is so terribly ﬁnal while life is full of possibilities.",
            "You Win or You Die", "His watch is Ended", "Sphinx of black quartz judge my dozen vows",
            "Fear cuts deeper than swords My Lord.", "Different roads sometimes lead to the same castle.",
            "A DRAGON IS NOT A SLAVE.", "Do not waste paper", "Go ring all the bells",
            "Crazy Fredrick bought many very exquisite pearl emerald and diamond jewels.",
            "The quick brown fox jumps over a lazy dog multiple times.", "We promptly judged antique ivory buckles for the next prize.",
            "Walar Morghulis: All men must die."
        };

        private static string GetRandomMessage()
        {
            Random indexGenerator = new Random();
            int index = indexGenerator.Next(MessagesCollection.GetUpperBound(0));
            return MessagesCollection[index];
        }
    }
}