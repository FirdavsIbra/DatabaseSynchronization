using System.ComponentModel.DataAnnotations.Schema;

namespace DbTask.ExternalDb.Models
{
    public sealed class ExDeletedOffice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
