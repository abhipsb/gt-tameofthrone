namespace TameOfThrones.BL.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for kingdom and its info
    /// </summary>
    public interface IKingdom
    {
        string KingdomName { get; }

        string Emblem { get; }

        string RulerName { get; }

        IList<string> AlliesNameList { get; }

        IKingdom AllienceKingdom { get; }

        bool IsAllienceProvided(IKingdom recvingKingdom, string message);

        void ResetAllience();
    }
}
