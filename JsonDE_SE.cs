using Newtonsoft.Json;
using System;
using System.IO;

namespace Note
{
    public class NoteParameter
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime todayDay { get; set; }
    }

    public static class JsonDE_SE
    {
        public static void Serialize<T>(T file, string fullNote)
        {
            string json = JsonConvert.SerializeObject(file);
            File.WriteAllText(fullNote, json);
        }

        public static T Deserialize<T>(string fullNote)
        {
            string json = File.ReadAllText(fullNote);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

}
