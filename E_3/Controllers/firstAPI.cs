using dal_4;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class firstAPI : ControllerBase
    {
        [HttpPost]
        public ActionResult insert(product_dto p)
        {
            dal.insert(p);
            return Ok("add");
        }

        [HttpGet]
        public ActionResult<product_dto> product_info(string name, string type)
        {
            var find = dal.list_info(type, name);
            if (find != null)
            {
                return Ok(find);
            }
            else
                return NotFound("not found");
        }
    }
}
