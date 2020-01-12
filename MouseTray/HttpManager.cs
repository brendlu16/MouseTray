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
        public static string Jmeno;
        static string Heslo;
        static readonly string URLNacteni = "https://brendlu16.sps-prosek.cz/API/nacteniprofilu.php";
        static readonly string URLNahrani = "https://brendlu16.sps-prosek.cz/API/nahraniprofilu.php";
        static readonly string URLSmazani = "https://brendlu16.sps-prosek.cz/API/smazaniprofilu.php";
        static readonly string URLLogin = "https://brendlu16.sps-prosek.cz/API/prihlaseni.php";
        public static List<Profil> NacistVse()
        {
            List<Profil> profily = new List<Profil>();
            var result = Task.Run(async () => { return await GetVse(profily); }).Result;
            return result;
        }
        public static async Task<List<Profil>> GetVse(List<Profil> profily)
        {
            HttpClient client = new HttpClient();
            var parameters = new Dictionary<string, string> { { "Jmeno", Jmeno }, { "Heslo", Heslo } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(URLNacteni, encodedContent);
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
            HttpClient client = new HttpClient();
            var parameters = new Dictionary<string, string> { { "Jmeno", Jmeno }, { "Heslo", Heslo }, { "Nazev", profil.Nazev }, { "Sens", profil.Sens.ToString() }, { "Scroll", profil.Scroll.ToString() }, { "DoubleClick", profil.DoubleClick.ToString()} };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(URLNahrani, encodedContent);
        }
        public static void SmazatProfil(string nazev)
        {
            Task.Run(async () => { await DeleteProfil(nazev); });
        }
        public static async Task DeleteProfil(string nazev)
        {
            HttpClient client = new HttpClient();
            var parameters = new Dictionary<string, string> { { "NazevSmazat", nazev }, { "Jmeno", Jmeno }, { "Heslo", Heslo } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(URLSmazani, encodedContent);
            string json = await response.Content.ReadAsStringAsync();
        }
        public static bool Prihlasit(string jmeno, string heslo)
        {
            var result = Task.Run(async () => { return await Login(jmeno, heslo); }).Result;
            return result;
        }
        public static void Odhlasit()
        {
            Jmeno = "";
            Heslo = "";
        }
        public async static Task<bool> Login(string jmeno, string heslo)
        {
            string hesloHash = Hashovat(heslo);
            HttpClient client = new HttpClient();
            var parameters = new Dictionary<string, string> { { "Jmeno", jmeno }, { "Heslo", hesloHash } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(URLLogin, encodedContent);
            string odpoved = await response.Content.ReadAsStringAsync();
            if (odpoved == "1")
            {
                Jmeno = jmeno;
                Heslo = hesloHash;
                return true;
            } else
            {
                return false;
            }
        }
        private static string Hashovat(string heslo)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(heslo));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
