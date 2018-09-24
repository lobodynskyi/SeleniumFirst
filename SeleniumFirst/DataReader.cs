using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFirst
{
    public class DataReader
    {
        

        public static ListInfo LoadJson()
        {
            string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("\\bin\\Debug", "");
            string path = Path.Combine(folderPath, "Data.json");
            string userData;

            using (StreamReader reader = new StreamReader(path))
            {
                userData = reader.ReadToEnd();
            }

            ListInfo users = JsonConvert.DeserializeObject<ListInfo>(userData);
            return users;
        }

        

    }

    public struct Info
    {
        public string firstname;
        public string lastname;
        public string address;
        public string city;
        public string country;
        public string zone;

    }

    public struct ListInfo
    {
        public Info[] infos { get; set; }
    }
}
