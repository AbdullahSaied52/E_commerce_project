using dal_4;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class firstAPI : ControllerBase
    {
        [HttpPost("insert")]
        public ActionResult insert(product_dto p)
        {
            dal.insert(p);
            return Ok("add");
        }

        [HttpGet("get_info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<product_dto> product_info(string name)
        {
            var find = dal.list_info( name);
            if (find != null)
            {
                return Ok(find);
            }
            else
                return NotFound("not found");
        }

        [HttpPut("for_sell")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult sell(string name,int amount)
        {
            int result = dal.sell(name, amount);
            if (result == 1)
            {
                return Ok("selled");
            }
            else if (result == 2)
            {
                return BadRequest("no device with this name");
            }
            else 
                return  NotFound("more than we have");
        }
    }
}
