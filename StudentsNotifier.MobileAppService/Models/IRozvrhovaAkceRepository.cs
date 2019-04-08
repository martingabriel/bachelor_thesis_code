using System;
using System.Collections.Generic;

namespace StudentsNotifier.MobileAppService.Models
{
    public interface IRozvrhovaAkceRepository
    {
        List<RozvrhovaAkce> Get(string userId);
        //IEnumerable<RozvrhoveAkce> GetAll();
    }
}
