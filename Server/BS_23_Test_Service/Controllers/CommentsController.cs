using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.DTO.Model;
using AspNet.UsersBLL;
using Microsoft.AspNetCore.Mvc;

namespace BS_23_Test_Service.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsBLLManager _CommentsBLLManager;

        public CommentsController(ICommentsBLLManager commentsBLLManager)
        {
            _CommentsBLLManager = commentsBLLManager;
        }
        [HttpGet]
        [Route("GetComments")]
        public IActionResult GetPosts()
        {
            try
            {
                var comments = _CommentsBLLManager.GetAll();
                if (comments == null)
                {
                    return NotFound();
                }

                return Ok(comments);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddComments")]
        public IActionResult Save([FromBody] Comments model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var comments = _CommentsBLLManager.AddComments(model);
                    if (comments.CommentsID > 0)
                    {
                        return Ok(comments.CommentsID);
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
