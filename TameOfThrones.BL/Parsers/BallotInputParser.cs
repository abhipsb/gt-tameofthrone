namespace TameOfThrones.BL.Parsers
{
    using System.Collections.Generic;
    using System.Linq;

    using TameOfThrones.BL.Interfaces.Data;
    using TameOfThrones.BL.Providers;

    /// <summary>
    /// Class to parse the input for ballot option
    /// </summary>
    internal class BallotInputParser
    {
        private readonly char[] _ballotSeparator = { ' ' };
        private IList<string> _inputList = new List<string>();

        /// <summary>
        /// Parse the unser input and make list of competing kingdoms
        /// </summary>
        /// <param name="input"></param>
        public void DoParseInput(string input)
        {
            input = input.Trim();
            _inputList = input.ToLower().Split(_ballotSeparator).ToList();
        }

        /// <summary>
        /// List of competing kingdoms
        /// </summary>
        public IList<string> CompetingKingdomList
        {
            get
            {
                if (IsInputValid)
                {
                    return _inputList;
                }

                return null;
            }
        }

        /// <summary>
        /// Checks if the user input is valid
        /// </summary>
        public bool IsInputValid
        {
            get
            {
                if (_inputList == null || !_inputList.Any() || 
                    _inputList.Count > KingdomData.TotalKingdomsCount)
                {
                    return false;
                }

                return _inputList.All(kingdom => InstanceProvider.GetKingdomByName(kingdom) != null);
            }
        }
    }
}
