namespace DbTask.ExternalDb.Models
{
    public sealed class ExDeletedOffice
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
