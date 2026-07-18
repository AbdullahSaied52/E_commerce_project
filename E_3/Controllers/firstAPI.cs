using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using class_1_DTO;
using BLL;
using Microsoft.AspNetCore.Authorization;

namespace E_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class firstAPI : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult login(string username,string password)
        {
            return ClsBLL.login(username, password) == 1 ? Ok("welcome") : NotFound("not authorized");
        }

        [HttpPost("insert")]
        //[Authorize]
        public ActionResult insert(product_dto p)
        {
            ClsBLL.insert(p);
            return Ok("add");
        }

        [HttpGet("get_info")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<product_dto> product_info(string name)
        {
            var find = ClsBLL.info( name);
            if (find != null)
            {
                return Ok(find);
            }
            else
                return NotFound("not found");
        }

        [HttpPut("for_sell")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult sell(string name,int amount)
        {
            int result = ClsBLL.sell(name, amount);
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
