using System.ComponentModel.DataAnnotations.Schema;

namespace DbTask.ExternalDb.Models
{
    public sealed class ExDeletedCity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
