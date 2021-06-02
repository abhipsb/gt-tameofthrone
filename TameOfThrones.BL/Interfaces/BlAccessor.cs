namespace TameOfThrones.BL.Interfaces
{
    using System.Collections.Generic;
    using TameOfThrones.BL.Interfaces.Data;
    using TameOfThrones.BL.Providers;

    /// <summary>
    /// Structure to define the result of ballot round
    /// </summary>
    public struct BallotResult
    {
        public bool IsSuccess;
        public bool IsTieExist;
        public IList<string> TieList;
        public string TieListString;
    }

    /// <summary>
    /// Class used for interfacing between PL and BL
    /// </summary>
    public static class BlAccessor
    {
        /// <summary>
        /// Static constructor to make sure the Global variables are initialized before use
        /// </summary>
        static BlAccessor()
        {
            InitGlobals();
        }

        /// <summary>
        /// Gets the instance of GetOutputDataProvider class
        /// </summary>
        /// <returns></returns>
        public static IOutputDataProvider GetOutputDataProvider()
        {
            return new OutputDataProvider();
        }

        /// <summary>
        /// Gets the instance of Kingdom class using name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IKingdom GetKingdom(string name)
        {
            return InstanceProvider.GetKingdomByName(name) as IKingdom;
        }

        /// <summary>
        /// Sending message to kingdom for allience
        /// </summary>
        /// <param name="userInputValue"></param>
        /// <param name="resetPrevious"></param>
        /// <returns></returns>
        public static bool ProcessMessageInput(string userInputValue, bool resetPrevious = false)
        {
            IMessageInputHandler inputHandler = InstanceProvider.CreateMessageInputHandler() as IMessageInputHandler;
            if (resetPrevious)
            {
                ResetGlobals();
            }

            if (inputHandler == null)
            {
                return false;
            }

            InstanceProvider.MinAllieCountRequired = 3;
            return inputHandler.DoProcessMessage(
                InstanceProvider.KingdomsHost[KingdomData.SpaceKingdomName] as IKingdom, userInputValue);
        }

        /// <summary>
        /// Start the ballot processing
        /// </summary>
        /// <param name="userInputValue"></param>
        /// <returns></returns>
        public static BallotResult ProcessBallotInput(string userInputValue)
        {
            IBallotInputHandler inputHandler = InstanceProvider.CreateBallotInputHandler() as IBallotInputHandler;
            ResetGlobals();
            InstanceProvider.MinAllieCountRequired = 1;
            if (inputHandler == null)
            {
                return new BallotResult();
            }

            return inputHandler.DoProcessBallot(userInputValue);
        }

        /// <summary>
        /// Reset all the previous selection
        /// </summary>
        public static void ResetGlobals()
        {
            foreach (IKingdom kingdom in InstanceProvider.KingdomsHost.Values)
            {
                kingdom.ResetAllience();
            }
        }

        /// <summary>
        /// Initialize the global variables
        /// </summary>
        private static void InitGlobals()
        {
            InstanceProvider.InitializeGlobal();
        }
    }
}