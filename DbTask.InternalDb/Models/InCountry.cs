namespace DbTask.InternalDb.Models
{
    public sealed class InCountry
    {
        /// <summary>
        /// Gets or sets id of country.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets name of country.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets cities of country.
        /// </summary>
        public IEnumerable<InCity> Cities { get; set; }  = new List<InCity>();
    }
}