namespace KnapsackContract
{
    public class KnapsackItem
    {
        public KnapsackItem(int weight, int cost)
        {
            this.Weight = weight;
            this.Cost = cost;
        }

        public int Weight { get; private set; }

        public int Cost { get; private set; }
    }
}
