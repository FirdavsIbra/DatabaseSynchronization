namespace DbTask.InternalDb.Models
{
    public sealed class InDeletedOffice
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
