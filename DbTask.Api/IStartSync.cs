namespace DbTask.Api
{
    public interface IStartSync
    {
        /// <summary>
        /// Start synchronization.
        /// </summary>
        public Task StartSyncAsync();
    }
}
