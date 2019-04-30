using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MouseTray
{
    class HttpManager
    {
        public static List<Profil> NacistVse()
        {
            List<Profil> profily = new List<Profil>();
            var result = Task.Run(async () => { return await GetVse(profily); }).Result;
            return result;
        }
        public static async Task<List<Profil>> GetVse(List<Profil> profily)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://brendlu16.sps-prosek.cz/");
            string json = await response.Content.ReadAsStringAsync();
            List<string> stringy = json.Split(';').ToList<string>();
            foreach (var item in stringy)
            {
                profily.Add(JsonConverter.ProfilZJson(item));
            }
            return profily;
        }
    }
}
