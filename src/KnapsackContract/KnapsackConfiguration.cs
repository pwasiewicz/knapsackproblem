namespace KnapsackContract
{
    public class KnapsackConfiguration
    {
        public KnapsackConfiguration(KnapsackItem[] items, int volume)
        {
            this.Items = items;
            this.KnapsackVolume = volume;
        }

        public KnapsackItem[] Items { get; private set; }

        public int KnapsackVolume { get; private set; }

        public int ItemsLength
        {
            get { return this.Items.Length; }
        }
    }
}
