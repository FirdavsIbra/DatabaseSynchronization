namespace DbTask.InternalDb.Models
{
    public sealed class InDeletedCity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
