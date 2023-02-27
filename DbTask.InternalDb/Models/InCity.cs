namespace DbTask.InternalDb.Models
{
    public sealed class InCity
    {
        /// <summary>
        /// Gets or sets id of city.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets name of city.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets id of country of city.
        /// </summary>
        public long CountryId { get; set; }
        public InCountry Country { get; set; }
    }
}
