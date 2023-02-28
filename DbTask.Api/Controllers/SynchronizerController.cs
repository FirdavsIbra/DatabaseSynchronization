using Microsoft.AspNetCore.Mvc;

namespace DbTask.Api.Controllers
{
    [ApiController]
    [Route("api/synchronizer")]
    public class SynchronizerController : ControllerBase
    {
        private readonly ISynchronize _synchronize;
        public SynchronizerController(ISynchronize synchronize)
        {   
            _synchronize = synchronize;
        }

        [HttpPost]
        public async Task<IActionResult> SynchronizeAsync(CancellationToken cancellationToken)
        {
            await _synchronize.StartSyncAsync(cancellationToken);
            return Ok();
        }
    }
}
