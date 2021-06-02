namespace TameOfThrones.BL.Providers
{
    using System.Collections.Generic;
    using System.Linq;

    using TameOfThrones.BL.Handlers;
    using TameOfThrones.BL.Interfaces.Data;
    using TameOfThrones.BL.Kingdoms;

    /// <summary>
    /// Class to provide the instances of different classes
    /// </summary>
    internal class InstanceProvider
    {
        /// <summary>
        /// Host the instances of all kingdoms
        /// </summary>
        public static IDictionary<string, object> KingdomsHost;

        /// <summary>
        /// Min. allie required to be the ruler of Southeros
        /// </summary>
        public static int MinAllieCountRequired = 1;

        /// <summary>
        /// Create and return the instance of Kingdom class
        /// </summary>
        /// <param name="kingdomName"></param>
        /// <param name="emblem"></param>
        /// <param name="rulerName"></param>
        /// <returns></returns>
        public static object CreateKingdomInstance(string kingdomName, string emblem, string rulerName)
        {
            return new Kingdom(kingdomName, emblem, rulerName);
        }

        /// <summary>
        /// Create the instance of UserInputHandler class
        /// </summary>
        /// <returns></returns>
        public static object CreateBallotInputHandler()
        {
            return new BallotInputHandler();
        }

        /// <summary>
        /// Create the instance of UserInputHandler class
        /// </summary>
        /// <returns></returns>
        public static object CreateMessageInputHandler()
        {
            return new MessageInputHandler();
        }

        /// <summary>
        /// Get the instance of Kingdom using Name from the Host
        /// </summary>
        /// <param name="kingdomName"></param>
        /// <returns></returns>
        public static object GetKingdomByName(string kingdomName)
        {
            return KingdomsHost == null || KingdomsHost.Count == 0
                ? null
                : (from key in KingdomsHost.Keys
                   where key.ToLower().Equals(kingdomName.ToLower())
                   select KingdomsHost[key]).FirstOrDefault();
        }

        /// <summary>
        /// Initialize the Global variables i.e. KingdomHost
        /// </summary>
        public static void InitializeGlobal()
        {
            KingdomsHost = new Dictionary<string, object>();
            KingdomsHost.Add(KingdomData.AirKingdomName, CreateKingdomInstance(
                        KingdomData.AirKingdomName,
                        KingdomData.AirKingdomEmblem,
                        KingdomData.AirKingdomRuler));
            KingdomsHost.Add(KingdomData.FireKingdomName, CreateKingdomInstance(
                        KingdomData.FireKingdomName,
                        KingdomData.FireKingdomEmblem,
                        KingdomData.FireKingdomRuler));
            KingdomsHost.Add(KingdomData.IceKingdomName, CreateKingdomInstance(
                        KingdomData.IceKingdomName,
                        KingdomData.IceKingdomEmblem,
                        KingdomData.IceKingdomRuler));
            KingdomsHost.Add(KingdomData.LandKingdomName, CreateKingdomInstance(
                        KingdomData.LandKingdomName,
                        KingdomData.LandKingdomEmblem,
                        KingdomData.LandKingdomRuler));
            KingdomsHost.Add(KingdomData.SpaceKingdomName, CreateKingdomInstance(
                        KingdomData.SpaceKingdomName,
                        KingdomData.SpaceKingdomEmblem,
                        KingdomData.SpaceKingdomRuler));
            KingdomsHost.Add(KingdomData.WaterKingdomName, CreateKingdomInstance(
                        KingdomData.WaterKingdomName,
                        KingdomData.WaterKingdomEmblem,
                        KingdomData.WaterKingdomRuler));
        }
    }
}
