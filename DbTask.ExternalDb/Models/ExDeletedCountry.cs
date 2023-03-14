using System.ComponentModel.DataAnnotations.Schema;

namespace DbTask.ExternalDb.Models
{
    public sealed class ExDeletedCountry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
