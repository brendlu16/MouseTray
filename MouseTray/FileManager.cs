using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTray
{
    class FileManager
    {
        private static readonly string path = @"profily\";
        public static void ZkontrolovatSlozku()
        {
            Directory.CreateDirectory("profily");
        }
        public static void UlozitProfil(Profil profil)
        {
            ZkontrolovatSlozku();
            string path2 = System.IO.Path.Combine(path, profil.Nazev+".json");
            System.IO.File.WriteAllText(path2, JsonConverter.ProfilDoJson(profil));
        }
        public static List<Profil> NacistProfily()
        {
            ZkontrolovatSlozku();
            string[] files = System.IO.Directory.GetFiles(path, "*.json");
            List<Profil> profily = new List<Profil>();
            foreach (var item in files)
            {
                profily.Add(JsonConverter.ProfilZJson(File.ReadAllText(item)));
            }
            return profily;
        }
        public static Profil NacistProfil(string nazev)
        {
            ZkontrolovatSlozku();
            string path2 = System.IO.Path.Combine(path, nazev + ".json");
            var file = System.IO.File.ReadAllText(path2);
            return JsonConverter.ProfilZJson(file);
        }
    }
}
