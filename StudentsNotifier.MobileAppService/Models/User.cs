using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StudentsNotifier.MobileAppService.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string StagID { get; set; }
        public string NotificationToken { get; set; }
        public IEnumerable<RozvrhovaAkce> RozvrhoveAkce { get; private set; }

        public void SetRozvrhoveAkce(IEnumerable<RozvrhovaAkce> akce)
        {
            RozvrhoveAkce = akce;
        }

        public bool ContainsRoakce(string roakceID)
        {
            bool result = false;

            if (RozvrhoveAkce != null)
                foreach (RozvrhovaAkce akce in RozvrhoveAkce)
                    if (akce.RoakIdno.ToString() == roakceID)
                        result = true;

            return result;
        }
    }
}
