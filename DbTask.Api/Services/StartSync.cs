namespace DbTask.Api.Services
{
    public class StartSync : IStartSync
    {
        private readonly ISynchronize _synchronize;
        public StartSync(ISynchronize synchronize)
        {
            _synchronize = synchronize;
        }

        /// <summary>
        /// Start synchronize.
        /// </summary>
        public async Task StartSyncAsync()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Synchronization started");
                    await _synchronize.SyncCountriesAsync();
                    await _synchronize.SyncCitiesAsync();
                    await _synchronize.SyncOfficesAsync();
                    Console.WriteLine("Synchronization completed");
                }
                catch (Exception)
                {
                    Console.WriteLine("Syncronization failed");
                }
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }
}
