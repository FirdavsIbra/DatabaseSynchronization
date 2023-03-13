namespace DbTask.ExternalDb.Models
{
    public sealed class ExDeletedCity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
