using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Diagnostics;

namespace StudentsNotifier.MobileAppService.Models
{
    public class RozvrhoveAkceRepository : IRozvrhovaAkceRepository
    {
        private static List<RozvrhoveAkce> akce = new List<RozvrhoveAkce>();

        public RozvrhoveAkceRepository()
        {

        }

        public List<RozvrhovaAkce> Get(string userId)
        {
            Debug.WriteLine("UserID: " + userId);
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string jsonString = wc.DownloadString("https://stag-ws.utb.cz/ws/services/rest2/rozvrhy/getRozvrhByStudent?outputFormat=JSON&osCislo=" + userId);
                    var rozvrhoveAkce = RozvrhoveAkce.FromJson(jsonString);
                    return rozvrhoveAkce.RozvrhovaAkce;
                }
                catch(Exception)
                {
                    Debug.WriteLine("Json read failed..");
                    return null;
                }
            }
        }


        /*
        public List<RozvrhovaAkce> GetRozvrhovaAkce(string id)
        {
            using (WebClient wc = new WebClient())
            {
                // TODO move url
                // my id = A15656
                string jsonString = wc.DownloadString("https://stag-ws.utb.cz/ws/services/rest2/rozvrhy/getRozvrhByStudent?outputFormat=JSON&osCislo=" + id);
                var rozvrhoveAkce = RozvrhoveAkce.FromJson(jsonString);
                return rozvrhoveAkce.RozvrhovaAkce;
            }
        }*/

        /*public IEnumerable<RozvrhoveAkce> GetAll()
        {
            // get rozvrhoveAkce from stag api

        }*/
    }
}
