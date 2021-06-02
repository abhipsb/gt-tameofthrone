namespace TameOfThrones.BL.Parsers
{
    using TameOfThrones.BL.Providers;

    /// <summary>
    /// Class to parse the kingdom name and message and put them separately
    /// </summary>
    internal class InputMessageParser
    {
        private readonly char[] _inputSeparator = { ',' };
        private string[] _inputArray = null;

        /// <summary>
        /// Parse the input
        /// </summary>
        /// <param name="input"></param>
        public void DoParseInput(string input)
        {
            _inputArray = input.Split(_inputSeparator);
        }

        /// <summary>
        /// Name of kingdom which will receive the message
        /// </summary>
        public object ReceiverKingdom => IsInputValid
            ? InstanceProvider.GetKingdomByName(_inputArray[0])
            : null;

        /// <summary>
        /// Message to receiver kingdom
        /// </summary>
        public string MessageToReceiverKingdom => IsInputValid ? _inputArray[1].ToLower() : string.Empty;

        /// <summary>
        /// Checks if the user input is valid
        /// </summary>
        private bool IsInputValid => _inputArray != null && (_inputArray.Length == 2);
    }
}
