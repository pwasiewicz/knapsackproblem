namespace KnapsackContract
{
    using Newtonsoft.Json;

    public class KnapsackConfiguration
    {
        public KnapsackConfiguration(KnapsackItem[] items, int volume)
        {
            this.Items = items;
            this.KnapsackVolume = volume;
        }

        [JsonProperty]
        public KnapsackItem[] Items { get; private set; }

        [JsonProperty]
        public int KnapsackVolume { get; private set; }

        public int ItemsLength
        {
            get { return this.Items.Length; }
        }
    }
}
