using Newtonsoft.Json;
using System.IO;

namespace GetSelectionShowPopup
{
    public static class DocumentationFileSerializer
    {
        public static FileDocumentation Deserialize(string filepath)
        {
            if (!File.Exists(filepath))
            {
                return new FileDocumentation();
            }
            string fileContents = File.ReadAllText(filepath);
            var deserialized = JsonConvert.DeserializeObject<FileDocumentation>(fileContents);
            return deserialized;
        }

        public static void Serialize(string filepath, FileDocumentation data)
        {
            string serialized = JsonConvert.SerializeObject(data);
            File.WriteAllText(filepath, serialized);
        }
    }
}
