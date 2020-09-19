using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.DTO.Model;
using AspNet.UsersBLL;
using Microsoft.AspNetCore.Mvc;

namespace BS_23_Test_Service.Controllers
{
    [Route("api/vote")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteBLLManager _VoteBLLManager;

        public VoteController(IVoteBLLManager voteBLLManager)
        {
            _VoteBLLManager = voteBLLManager;
        }
        [HttpGet]
        [Route("GetVots")]
        public IActionResult GetVots()
        {
            try
            {
                var votes = _VoteBLLManager.GetAllVote();
                if (votes == null)
                {
                    return NotFound();
                }

                return Ok(votes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddVots")]
        public IActionResult Save([FromBody] Vote model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var vote = _VoteBLLManager.AddVote(model);
                    if (vote.VoteID > 0)
                    {
                        return Ok(vote.VoteID);
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
