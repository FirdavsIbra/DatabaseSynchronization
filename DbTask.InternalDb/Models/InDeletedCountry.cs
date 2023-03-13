namespace DbTask.InternalDb.Models
{
    public sealed class InDeletedCountry
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
