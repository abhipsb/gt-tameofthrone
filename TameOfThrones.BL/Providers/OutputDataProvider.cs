namespace TameOfThrones.BL.Providers
{
    using System.Linq;
    using TameOfThrones.BL.Interfaces;
    using TameOfThrones.BL.Interfaces.Data;

    /// <summary>
    /// Class provides common methods for displaying output
    /// </summary>
    internal class OutputDataProvider : IOutputDataProvider
    {                
        /// <summary>
        /// Name of current ruler
        /// </summary>
        public string RulerName => RulerKingdom == null ? KingdomData.NoneValue : RulerKingdom.RulerName;

        /// <summary>
        /// List of allies for current ruler
        /// </summary>
        public string AlliesList => GetFormattedAlliesListForOutput();

        /// <summary>
        /// The ruler kingdom with max. allies
        /// </summary>
        private IKingdom RulerKingdom => GetRulerKingdom();

        /// <summary>
        /// Make the formatted list of allies for the output purpose
        /// </summary>
        private string GetFormattedAlliesListForOutput()
        {
            string formattedAlliesList = KingdomData.NoneValue;

            // If RulerKingdom exist, make the allies list for formatted output
            if (RulerKingdom == null || RulerKingdom.AlliesNameList.Count < InstanceProvider.MinAllieCountRequired)
            {
                return formattedAlliesList;
            }

            foreach (string allieKingdom in RulerKingdom.AlliesNameList)
            {
                // The comma ',' is not required for the first
                if (RulerKingdom.AlliesNameList.First().Equals(allieKingdom))
                {
                    formattedAlliesList = allieKingdom;
                    continue;
                }

                formattedAlliesList = formattedAlliesList + ", " + allieKingdom;
            }

            return formattedAlliesList;
        }

        /// <summary>
        /// Find the ruler kingdom
        /// </summary>
        /// <returns></returns>
        private IKingdom GetRulerKingdom()
        {
            IKingdom rulerKingdom = null;
            int maxAlliesCount = 0;

            // Get the kingdom with Max. Allies
            foreach (IKingdom kingdom in InstanceProvider.KingdomsHost.Values)
            {
                if (kingdom.AlliesNameList.Count > maxAlliesCount)
                {
                    rulerKingdom = kingdom;
                    maxAlliesCount = rulerKingdom.AlliesNameList.Count;
                }
            }

            return rulerKingdom != null && rulerKingdom.AlliesNameList.Count >= InstanceProvider.MinAllieCountRequired
                ? rulerKingdom
                : null;
        }
    }
}
