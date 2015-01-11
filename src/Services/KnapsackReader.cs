namespace KnapsackProblem.Services
{
    using KnapsackContract;
    using Newtonsoft.Json;
    using System.IO;

    public class KnapsackReader
    {
        private readonly string file;

        public KnapsackReader(string file)
        {
            this.file = file;
        }

        public KnapsackConfiguration ReadConfiguration()
        {
            using (var streamReader = new StreamReader(this.file))
            {
                var text = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<KnapsackConfiguration>(text);
            }
        }
    }
}
