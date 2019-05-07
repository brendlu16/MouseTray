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
        public static void NahratProfil(Profil profil)
        {
            Task.Run(async () => { await UploadProfil(profil); });
        }
        public static async Task UploadProfil(Profil profil)
        {
            string json = JsonConverter.ProfilDoJson(profil);
            HttpClient client = new HttpClient();
            var parameters = new Dictionary<string, string> { { "Nazev", profil.Nazev }, { "Sens", profil.Sens.ToString() }, { "Scroll", profil.Scroll.ToString() }, { "DoubleClick", profil.DoubleClick.ToString()} };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync("https://brendlu16.sps-prosek.cz/", encodedContent);
        }
        public static Profil NacistProfil(string nazev)
        {
            Profil profil = new Profil();
            Profil result = Task.Run(async () => { return await GetProfil(profil); }).Result;
            return result;
        }
        public static async Task<Profil> GetProfil(Profil profil)
        {
            HttpClient client = new HttpClient();
            var parameters = new Dictionary<string, string> { { "param1", "1" }, { "param2", "2" } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.GetAsync("https://brendlu16.sps-prosek.cz/");
            string json = await response.Content.ReadAsStringAsync();
            profil = JsonConverter.ProfilZJson(json);
            return profil;
        }
        public static void SmazatProfil(string nazev)
        {
            Task.Run(async () => { await DeleteProfil(nazev); });
        }
        public static async Task DeleteProfil(string nazev)
        {
            HttpClient client = new HttpClient();
            var parameters = new Dictionary<string, string> { { "NazevSmazat", nazev } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync("https://brendlu16.sps-prosek.cz/", encodedContent);
        }
    }
}
