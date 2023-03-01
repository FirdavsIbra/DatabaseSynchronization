namespace DbTask.ExternalDb.Models
{
    public sealed class ExOffice
    {
        /// <summary>
        /// Gets or sets id of office.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets name of office.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets city id of office.
        /// </summary>
        public long CityId { get; set; }
        public ExCity City { get; set; }
    }
}
