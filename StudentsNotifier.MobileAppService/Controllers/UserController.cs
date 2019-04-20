using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StudentsNotifier.MobileAppService.Models;

namespace StudentsNotifier.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository UserRepository;

        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(UserRepository.GetAll());
        }

        [HttpGet("{id}")]
        public User GetItem(string id)
        {
            User user = UserRepository.Get(id);
            return user;
        }

        [HttpPost]
        public IActionResult Create([FromBody]User user)
        {
            try
            {
                if (user == null || !ModelState.IsValid)
                    return BadRequest("Invalid state");

                UserRepository.Add(user);
            }
            catch (Exception)
            {
                return BadRequest("Error");
            }
            return Ok(user);
        }

        [HttpPut]
        public IActionResult Edit([FromBody]User user)
        {
            try
            {
                if (user == null || !ModelState.IsValid)
                    return BadRequest("Invalid state");

                UserRepository.Add(user);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            UserRepository.Remove(id);
        }

        [HttpGet("RozvrhoveAkce/{id}")]
        public IEnumerable<RozvrhovaAkce> GetRozvrhoveAkce(string id)
        {
            var result = UserRepository.GetRozvrhoveAkce(id);
            return result;
        }

        [HttpGet("UserIDsByRozvrhovaAkce/{id}")]
        public IEnumerable<string> GetUserIDsByRozvrhovaAkce(string id)
        {
            var result = UserRepository.GetUserIDsByRozvrhoveAkce(id);
            return result;
        }
    }
}
