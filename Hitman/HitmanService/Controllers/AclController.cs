using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HitmanService.Controllers
{
    [Produces("application/json")]
    [Route("api/Acl")]
    public class AclController : Controller
    {
        [HttpGet("{category?}/{uniquename?}/{key?}")]
        public IActionResult Get(string identifier, string key)
        {
            return Content("");
        }
    }
}