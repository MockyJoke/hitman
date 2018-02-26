using System.Threading.Tasks;
using HitmanModel.Storage;
using HitmanService.Services;
using Microsoft.AspNetCore.Mvc;

namespace HitmanService.Controllers
{
    [Produces("application/json")]
    [Route("api/Store")]
    public class StoreController : Controller
    {
        //private IStorageService _storageService;
        private IStorageService _storageService;
        public StoreController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet("{category?}/{uniquename?}/{key?}")]
        public async Task<IActionResult> Get(StorableIdentifier identifier, string key)
        {
            IStorageObject storageObject = await _storageService.LoadAsync(identifier).ConfigureAwait(false);
            Request.HttpContext.Response.Headers.Add("Metadata", storageObject.Metadata);
            return new FileStreamResult(storageObject.DataStream, "application/octet-stream");

        }

        [HttpPost("{category?}/{uniquename?}/{key?}")]
        public async Task<IActionResult> Post(StorableIdentifier identifier, string key)
        {
            await _storageService.SaveAsync(Request.Body, Request.Headers["Metadata"], identifier).ConfigureAwait(false);
            return new OkResult();
        }
    }
}