using System.IO;
using Newtonsoft.Json;

namespace DataCollector.Local.PC
{
    internal class SettingsLoader<T> where T : new()
    {
        private const string DefaultFilename = "settings.json";

        public void Save(string fileName = DefaultFilename)
        {
            //File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
        }

        public static void Save(T pSettings, string fileName = DefaultFilename)
        {
            //File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
        }

        public static T Load(string fileName = DefaultFilename)
        {
            var t = new T();
            if (File.Exists(fileName))
            {
                t = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
            }
            return t;
        }


    }
}
