using System.ComponentModel.DataAnnotations.Schema;

namespace DbTask.InternalDb.Models
{
    public sealed class InOffice
    {
        /// <summary>
        /// Gets or sets id of office.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets name of office.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets city id of office.
        /// </summary>
        public long CityId { get; set; }
        public InCity City { get; set; }
    }
}
