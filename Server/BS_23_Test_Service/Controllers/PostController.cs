using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.DTO.Model;
using AspNet.UsersBLL;
using Microsoft.AspNetCore.Mvc;

namespace BS_23_Test_Service.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostBLLManager _PostBLLManager;

        public PostController(IPostBLLManager postBLLManager)
        {
            _PostBLLManager = postBLLManager;
        }
        [HttpGet]
        [Route("GetPosts")]
        public IActionResult GetPosts()
        {
            try
            {
                var posts = _PostBLLManager.GetAllPost();
                if (posts == null)
                {
                    return NotFound();
                }

                return Ok(posts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddPost")]
        public IActionResult Save([FromBody] Post model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var post = _PostBLLManager.AddPost(model);
                    if (post.PostID > 0)
                    {
                        return Ok(post.PostID);
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

    }
}
