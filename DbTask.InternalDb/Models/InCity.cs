using System.ComponentModel.DataAnnotations.Schema;

namespace DbTask.InternalDb.Models
{
    public sealed class InCity
    {
        /// <summary>
        /// Gets or sets id of city.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        /// <summary>
        /// Gets or sets offices of the city.
        /// </summary>
        public IEnumerable<InOffice> Offices { get; set; } = new List<InOffice>();
    }
}
