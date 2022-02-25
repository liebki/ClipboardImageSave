using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClipboardImageSave
{
    public class Manager
    {
        public int getTimerDuration()
        {
            int duration = 4;
            if (File.Exists("App.config"))
            {
                duration = readCheckDuration();
            }
            return duration;
        }

        private int readCheckDuration()
        {
            string duration = readConfigValue("check_duration");
            int parsedDuration = Convert.ToInt32(duration);
            if (parsedDuration <= 0)
            {
                parsedDuration = 1;
            }
            return parsedDuration;
        }

        private string readConfigValue(String key)
        {
            string value = "";
            XmlTextReader textReader = new XmlTextReader("cisconfig.xml");
            if (textReader.NodeType == XmlNodeType.Element)
            {
                if (textReader.Name == key)
                {
                    textReader.Read();
                    value = textReader.Value;
                }
            }
            return value;
        }
    }
}