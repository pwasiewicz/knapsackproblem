namespace KnapsackProblem.Services
{
    using Exceptions;
    using KnapsackContract;
    using Newtonsoft.Json;
    using System;
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
            try
            {
                using (var streamReader = new StreamReader(this.file))
                {
                    var text = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<KnapsackConfiguration>(text);
                }
            }
            catch (Exception ex)
            {
                throw new ReadingConfigurationException(ex.Message, ex);
            }
        }
    }
}
