using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CollageApp.State
{
    public static class Serializer
    {
        private static XmlSerializer formatter = new XmlSerializer(typeof(Settings));
        private const string defaultPath = "settings.xml";

        public static void SaveSettings(Settings settings)
        {
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory()+ "\\" + defaultPath, FileMode.Create))
            {
                formatter.Serialize(fs, settings);
            }           
        }

        public static Settings LoadSettings()
        {
            Settings settings = null;
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + defaultPath, FileMode.OpenOrCreate))
            {
                try
                {
                    settings = (Settings)formatter.Deserialize(fs);
                }
                catch (Exception e)
                {
                    settings = null;
                }               
            }
            return settings;
        }
    }
}
