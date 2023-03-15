namespace DbTask.ExternalDb.Models
{
    public sealed class ExCity
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
        public ExCountry Country { get; set; }

        /// <summary>
        /// Gets or sets offices of the city.
        /// </summary>
        public IEnumerable<ExOffice> Offices { get; set; } = new List<ExOffice>();
    }
}
