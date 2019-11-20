﻿using Newtonsoft.Json;
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
        static string Jmeno;
        static string Heslo;
        static readonly string URL = "https://brendlu16.sps-prosek.cz/API/";
        public static List<Profil> NacistVse()
        {
            List<Profil> profily = new List<Profil>();
            var result = Task.Run(async () => { return await GetVse(profily); }).Result;
            return result;
        }
        public static async Task<List<Profil>> GetVse(List<Profil> profily)
        {
            string test = Jmeno;
            HttpClient client = new HttpClient();
            var parameters = new Dictionary<string, string> { { "Jmeno", Jmeno }, { "Heslo", Heslo } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(URL, encodedContent);
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
            var parameters = new Dictionary<string, string> { { "Jmeno", Jmeno }, { "Heslo", Heslo }, { "Nazev", profil.Nazev }, { "Sens", profil.Sens.ToString() }, { "Scroll", profil.Scroll.ToString() }, { "DoubleClick", profil.DoubleClick.ToString()} };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(URL, encodedContent);
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
            var response = await client.PostAsync(URL, encodedContent);
        }
        public static void Prihlasit(string jmeno, string heslo)
        {
            Jmeno = jmeno;
            Heslo = heslo;
        }
    }
}
