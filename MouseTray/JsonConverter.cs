using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTray
{
    class JsonConverter
    {
        public static string ProfilDoJson(Profil input)
        {
            return JsonConvert.SerializeObject(input);
        }
        public static Profil ProfilZJson(string input)
        {
            return JsonConvert.DeserializeObject<Profil>(input);
        }
    }
}
