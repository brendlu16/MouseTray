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
        private static string path = @"D:\source\repos\MouseTray\";//Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static void UlozitProfil(Profil profil)
        {
            string path2 = System.IO.Path.Combine(path, profil.Nazev+".json");
            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(JsonConverter.ProfilDoJson(profil));
            }
        }
        public static List<Profil> NacistProfily()
        {
            string[] files = System.IO.Directory.GetFiles(path, "*.json");
            List<Profil> profily = new List<Profil>();
            foreach (var item in files)
            {
                profily.Add(JsonConverter.ProfilZJson(File.ReadAllText(item)));
            }
            return profily;
        }
    }
}
