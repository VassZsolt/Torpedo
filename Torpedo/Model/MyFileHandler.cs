using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NationalInstruments.Torpedo.Model
{
    public class MyFileHandler
    {
        private readonly JsonSerializerOptions _options =
               new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        public void SimpleWrite(object obj, string fileName)
        {
            var myString = JsonSerializer.Serialize(obj, _options);

            File.AppendAllText(fileName, myString);
        }
        public void PrettyWrite(object obj)
        {
            string fileName = "Matches.json";
            var options = new JsonSerializerOptions(_options)
            {
                WriteIndented = true
            };
            var myString = JsonSerializer.Serialize(obj, options);

            File.AppendAllText(fileName, myString);
        }
    }
}
