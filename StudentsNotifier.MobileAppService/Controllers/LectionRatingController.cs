using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentsNotifier.MobileAppService.Models;

namespace StudentsNotifier.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    public class LectionRatingController : Controller
    {
        private readonly ILectionRating LectionRatingRepository;

        public LectionRatingController(ILectionRating lectionRatingRepository)
        {
            LectionRatingRepository = lectionRatingRepository;
        }

        #region LectionRating

        [HttpGet]
        public IActionResult List()
        {
            return Ok(LectionRatingRepository.GetAll());
        }

        [HttpGet("{id}")]
        public LectionRating GetItem(string id)
        {
            LectionRating rating = LectionRatingRepository.Get(id);
            return rating;
        }

        [HttpPost]
        public IActionResult Create([FromBody]LectionRating lection)
        {
            try
            {
                if (lection == null || !ModelState.IsValid)
                    return BadRequest("Invalid state");

                LectionRatingRepository.Add(lection);
            }
            catch (Exception)
            {
                return BadRequest("Error");
            }
            return Ok(lection);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            LectionRatingRepository.Remove(id);
        }

        #endregion

        #region Vote

        [HttpPost("Vote/")]
        public IActionResult PutVote([FromBody]Vote vote)
        {
            try
            {
                if (vote == null || !ModelState.IsValid)
                    return BadRequest("Invalid state");

                LectionRatingRepository.AddVote(vote);
            }
            catch (Exception)
            {
                return BadRequest("Error");
            }
            return Ok(vote);
        }

        [HttpGet("Vote/{id}")]
        public Vote GetVote(string id)
        {
            Vote vote = LectionRatingRepository.GetVote(id);
            return vote;
        }

        [HttpDelete("Vote/{id}")]
        public void DeleteVote(string id)
        {
            LectionRatingRepository.RemoveVote(id);
        }

        #endregion

        #region Vote request

        [HttpPost("SendVoteRequest/")]
        public IActionResult SendVoteRequest([FromBody]VoteRequest request)
        {
            try
            {
                if (request == null || !ModelState.IsValid)
                    return BadRequest("Invalid state");

                // DEBUG: Send vote request
                Debug.WriteLine("\nSend vote request to lection: " + request.LectionRatingId + "\n");
            }
            catch (Exception)
            {
                return BadRequest("Error");
            }
            return Ok(request);
        }

        #endregion
    }
}
