using System.ComponentModel.DataAnnotations.Schema;

namespace DbTask.ExternalDb.Models
{
    public sealed class ExCountry
    {
        /// <summary>
        /// Gets or sets id of country.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets name of country.
        /// </summary>
        public string Name { get; set; }
    }
}
