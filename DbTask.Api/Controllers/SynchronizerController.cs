using Microsoft.AspNetCore.Mvc;

namespace DbTask.Api.Controllers
{
    [ApiController]
    [Route("api/synchronizer")]
    public class SynchronizerController : ControllerBase
    {
        private readonly IStartSync _startSync;
        public SynchronizerController(IStartSync startSync)
        {
            _startSync = startSync;
        }

        /// <summary>
        /// Synchronize databases.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SynchronizeAsync()
        {
            await _startSync.StartSyncAsync();
            return Ok();
        }
    }
}
