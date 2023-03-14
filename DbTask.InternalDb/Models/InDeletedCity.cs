using System.ComponentModel.DataAnnotations.Schema;

namespace DbTask.InternalDb.Models
{
    public sealed class InDeletedCity
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets id of country.
        /// </summary>
        public long CountryId { get; set; }
        
        /// <summary>
        /// Gets or sets time when city was deleted.
        /// </summary>
        public DateTime DeletedAt { get; set; }
    }
}
