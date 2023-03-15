namespace DbTask.Api
{
    public interface ISynchronize
    {
        /// <summary>
        /// Synchronize countries from internal database to external database.
        /// </summary>
        public Task SyncCountriesAsync();

        /// <summary>
        /// Synchronize cities from external database to internal database.
        /// </summary>
        public Task SyncCitiesAsync();

        /// <summary>
        /// Synchronize offices from external database to internal database.
        /// </summary>
        public Task SyncOfficesAsync();
        public Task OptimizedSyncCountriesAsync();
    }
}
