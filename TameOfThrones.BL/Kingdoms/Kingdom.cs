namespace TameOfThrones.BL.Kingdoms
{
    using System.Collections.Generic;
    using System.Linq;

    using TameOfThrones.BL.Interfaces;
    using TameOfThrones.BL.Interfaces.Data;

    /// <summary>
    /// Kingdom class, implemnts functionality for providing allience
    /// </summary>
    internal class Kingdom : IKingdom
    {
        private IList<IKingdom> _alliesKingdomList = new List<IKingdom>();

        /// <summary>
        /// Constructur
        /// </summary>
        /// <param name="kingdomName"></param>
        /// <param name="emblem"></param>
        /// <param name="rulerName"></param>
        public Kingdom(string kingdomName, string emblem, string rulerName)
        {
            KingdomName = kingdomName;
            Emblem = emblem;
            RulerName = rulerName;
            AllienceKingdom = null;
        }

        /// <summary>
        /// List of names of allie kingdoms
        /// </summary>
        public IList<string> AlliesNameList => _alliesKingdomList.Select(kingdom => kingdom.KingdomName).ToList();

        /// <summary>
        /// The allience is given to the kingdom
        /// </summary>
        public IKingdom AllienceKingdom { get; private set; }

        /// <summary>
        /// Name of the kingdom
        /// </summary>
        public string KingdomName { get; private set; }

        /// <summary>
        /// Emblem of the kingdom
        /// </summary>
        public string Emblem { get; private set; }

        /// <summary>
        /// Name of the ruler
        /// </summary>
        public string RulerName { get; private set; }

        /// <summary>
        /// Ask the receiving kingdom for allience by sending message
        /// </summary>
        /// <param name="recvKingdom"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool IsAllienceProvided(IKingdom recvKingdom, string message)
        {
            if (recvKingdom != null && message != string.Empty &&
                recvKingdom.KingdomName != KingdomName && !_alliesKingdomList.Contains(recvKingdom)
                && _alliesKingdomList.Count < KingdomData.TotalKingdomsCount - 1)
            {
                return ((Kingdom)recvKingdom).CheckAndProvideAllience(this, message);
            }

            return false;
        }

        /// <summary>
        /// Reset all the previous selections
        /// </summary>
        public void ResetAllience()
        {
            _alliesKingdomList = new List<IKingdom>();
            AllienceKingdom = null;
        }

        /// <summary>
        /// Check if allience can be given to sender kingdom
        /// </summary>
        /// <param name="senderKingdom"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool CheckAndProvideAllience(IKingdom senderKingdom, string message)
        {
            bool isMessageAccepted = true;
            string emblemInLowerCase = Emblem.ToLower();
            string messageInLowerCase = message.ToLower();
            foreach (char emblemChar in emblemInLowerCase.ToCharArray())
            {
                if (!messageInLowerCase.Contains(emblemChar))
                {
                    isMessageAccepted = false;
                    break;
                }

                messageInLowerCase = messageInLowerCase.Remove(messageInLowerCase.IndexOf(emblemChar), 1);
            }

            // Give allience only if it doesn't exist
            if (isMessageAccepted && AllienceKingdom == null)
            {
                ((Kingdom)senderKingdom)._alliesKingdomList.Add(this);
                AllienceKingdom = senderKingdom;
            }

            return true;
        }
    }
}
