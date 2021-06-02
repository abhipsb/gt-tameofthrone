namespace TameOfThrones.BL.Interfaces
{
    /// <summary>
    /// Interface for user input handling e.g. Ballot
    /// </summary>
    public interface IBallotInputHandler
    {
        /// <summary>
        /// Process the ballot
        /// </summary>
        /// <param name="competingKingtoms"></param>
        /// <returns></returns>
        BallotResult DoProcessBallot(string competingKingtoms);
    }
}
