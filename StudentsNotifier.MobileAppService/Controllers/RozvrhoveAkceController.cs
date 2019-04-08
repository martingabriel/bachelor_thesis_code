using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using StudentsNotifier.MobileAppService.Models;

namespace StudentsNotifier.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    public class RozvrhoveAkceController : Controller
    {

        private readonly IRozvrhovaAkceRepository RozvrhovaAkceRepository;

        public RozvrhoveAkceController(IRozvrhovaAkceRepository rozvrhovaAkceRepository)
        {
            RozvrhovaAkceRepository = rozvrhovaAkceRepository;
        }

        [HttpGet("{id}")]
        public List<RozvrhovaAkce> GetItem(string id)
        {
            var result = RozvrhovaAkceRepository.Get(id);
            return result;
        }
    }
}
