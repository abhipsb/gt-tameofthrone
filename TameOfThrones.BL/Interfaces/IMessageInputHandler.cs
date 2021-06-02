namespace TameOfThrones.BL.Interfaces
{
    /// <summary>
    /// Interface for user input handling e.g. Ballot
    /// </summary>
    public interface IMessageInputHandler
    {
        /// <summary>
        /// Send messsage to Kingdom for Alliance
        /// </summary>
        /// <param name="senderKingdom"></param>
        /// <param name="recvKingdomAndMessage"></param>
        /// <returns></returns>
        bool DoProcessMessage(IKingdom senderKingdom, string recvKingdomAndMessage);
    }
}
