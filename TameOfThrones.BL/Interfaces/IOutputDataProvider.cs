namespace TameOfThrones.BL.Interfaces
{
    /// <summary>
    /// Interface for providing Output
    /// </summary>
    public interface IOutputDataProvider
    {
        /// <summary>
        /// Name of current ruler
        /// </summary>
        string RulerName { get; }

        /// <summary>
        /// List of allies for current ruler
        /// </summary>
        string AlliesList { get; }
    }
}
