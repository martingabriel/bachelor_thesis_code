using System;
using Microsoft.AspNetCore.Mvc;
using StudentsNotifier.MobileAppService.Models;

namespace StudentsNotifier.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageRepository MessageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            MessageRepository = messageRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(MessageRepository.GetAll());
        }

        [HttpGet("{id}")]
        public Message GetItem(string id)
        {
            Message item = MessageRepository.Get(id);
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Message item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                MessageRepository.Add(item);

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            MessageRepository.Remove(id);
        }
    }
}
