namespace DbTask.ExternalDb.Models
{
    public sealed class ExDeletedCountry
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
