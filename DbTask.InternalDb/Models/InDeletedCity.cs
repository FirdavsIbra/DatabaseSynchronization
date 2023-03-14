using System.ComponentModel.DataAnnotations.Schema;

namespace DbTask.InternalDb.Models
{
    public sealed class InDeletedCity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
