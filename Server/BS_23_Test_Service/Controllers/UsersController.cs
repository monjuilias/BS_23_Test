using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.DTO;
using AspNet.DTO.VM;
using AspNet.UsersBLL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BS_23_Test_Service.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBLLManager _UserBLLManager;

        public UsersController(IUserBLLManager userBLLManager)
        {
            _UserBLLManager = userBLLManager;
        }

        // GET: api/<UsersController>
        [HttpGet("getusers")]
        public List<Users> Get()
        {
          return  _UserBLLManager.GetAll();
        }
        [HttpPost]
        [Route("AddUser")]
        public IActionResult Save([FromBody] Users model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _UserBLLManager.AddUsers(model);
                    if (user!=null)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();

        }
        [HttpPost("GetAllPostDetail")]
        public ActionResult GetAllPostDetail(PostFilterPagination postFilter)
        {
            try
            {
                var postdetails = _UserBLLManager.GetAllPostDetail(postFilter);
                if (postdetails == null)
                {
                    return NotFound();
                }

                return new JsonResult(postdetails);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
