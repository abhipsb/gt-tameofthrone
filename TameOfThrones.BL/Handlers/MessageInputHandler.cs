namespace TameOfThrones.BL.Handlers
{
    using TameOfThrones.BL.Interfaces;
    using TameOfThrones.BL.Parsers;

    /// <summary>
    /// Handler for the user choice of Ballot or sending message to kingdom
    /// </summary>
    internal class MessageInputHandler : IMessageInputHandler
    {
        /// <summary>
        /// Process the message for getting Alliance
        /// </summary>
        /// <param name="senderKingdom"></param>
        /// <param name="recvKingdomAndMessage"></param>
        /// <returns></returns>
        public bool DoProcessMessage(IKingdom senderKingdom, string recvKingdomAndMessage)
        {
            var inputParser = new InputMessageParser();
            inputParser.DoParseInput(recvKingdomAndMessage);
            return senderKingdom.IsAllienceProvided(
                inputParser.ReceiverKingdom as IKingdom, inputParser.MessageToReceiverKingdom);
        }
    }
}