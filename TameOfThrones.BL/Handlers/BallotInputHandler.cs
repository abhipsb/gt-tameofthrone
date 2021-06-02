namespace TameOfThrones.BL.Handlers
{
    using TameOfThrones.BL.Interfaces;
    using TameOfThrones.BL.Interfaces.Data;
    using TameOfThrones.BL.Parsers;
    using TameOfThrones.BL.Providers;

    /// <summary>
    /// Handler for the user choice of Ballot or sending message to kingdom
    /// </summary>
    internal class BallotInputHandler : IBallotInputHandler
    {
        private readonly BallotInputParser _ballotInputParser;
        private readonly IMessageInputHandler _messageHandler;
        private string _tieList;
        private int _maxAlliesCount = 0;
        private bool _isTieExist = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public BallotInputHandler()
        {
            _ballotInputParser = new BallotInputParser();
            _messageHandler = new MessageInputHandler();
            _tieList = string.Empty;
            _maxAlliesCount = 0;
            _isTieExist = false;
        }

        /// <summary>
        /// Initiate the Ballot Process forcompeting kingdoms and find the tie
        /// </summary>
        /// <param name="competingKingtoms"></param>
        /// <returns></returns>
        public BallotResult DoProcessBallot(string competingKingtoms)
        {
            BallotResult result = new BallotResult();
            _isTieExist = false;
            _tieList = string.Empty;
            _ballotInputParser.DoParseInput(competingKingtoms);
            if (!_ballotInputParser.IsInputValid)
            {
                result.IsSuccess = false;
                return result;
            }

            // Each competing kingdom will send predefind messages to other kingdoms
            foreach (string competingKingdom in _ballotInputParser.CompetingKingdomList)
            {
                IKingdom senderKingdom = InstanceProvider.GetKingdomByName(competingKingdom) as IKingdom;
                if (senderKingdom == null)
                {
                    continue;
                }

                ProessCompetingKingdom(senderKingdom);
                CheckAndSetTie(competingKingdom, senderKingdom.AlliesNameList.Count);
            }

            result.IsSuccess = true;
            result.IsTieExist = false;
            result.TieList = _ballotInputParser.CompetingKingdomList;
            if (_isTieExist)
            {
                _ballotInputParser.DoParseInput(_tieList);
                result.IsTieExist = true;
                result.TieListString = _tieList;
                result.TieList = _ballotInputParser.CompetingKingdomList;
            }

            return result;
        }

        /// <summary>
        /// List of competing kingdoms that has same count of allies i.e. Tie
        /// </summary>
        /// <param name="kingdomName"></param>
        /// <param name="alliesCount"></param>
        private void CheckAndSetTie(string kingdomName, int alliesCount)
        {
            if (alliesCount > _maxAlliesCount)
            {
                _tieList = kingdomName;
                _maxAlliesCount = alliesCount;
                return;
            }

            if (alliesCount > 0 && _maxAlliesCount == alliesCount)
            {
                _isTieExist = true;
                _tieList = _tieList + " " + kingdomName;
            }
        }

        /// <summary>
        /// Each competing kingdom sends random message to other kingdoms for allie
        /// </summary>
        /// <param name="senderKingdom"></param>
        private void ProessCompetingKingdom(IKingdom senderKingdom)
        {
            foreach (IKingdom receiverKingdom in InstanceProvider.KingdomsHost.Values)
            {
                if (_ballotInputParser.CompetingKingdomList.Contains(receiverKingdom.KingdomName.ToLower())
                    || receiverKingdom.AllienceKingdom != null)
                {
                    continue;
                }

                string recvKingdomAndMessage = receiverKingdom.KingdomName + "," + KingdomData.RandomMessage;
                _messageHandler.DoProcessMessage(senderKingdom, recvKingdomAndMessage);
            }
        }
    }
}