namespace TabuAlgorithm
{
    using System;
    using System.Linq;
    using KnapsackContract;

    internal class TabuElement
    {
        private static readonly Random Random = new Random();

        private readonly KnapsackConfiguration configuration;

        private int? lastTotalCost;
        private int? lastTotalWeight;
        private readonly bool[] itemsIncludedInKnapsack;

        public TabuElement(KnapsackConfiguration configuration)
        {
            this.configuration = configuration;

            this.itemsIncludedInKnapsack = new bool[this.configuration.ItemsLength];

            this.Randomize();
        }

        public TabuElement(TabuElement element)
        {
            this.configuration = element.configuration;

            this.itemsIncludedInKnapsack = new bool[this.configuration.ItemsLength];
            element.itemsIncludedInKnapsack.CopyTo(this.itemsIncludedInKnapsack, 0);
        }

        public TabuMove CreatedFrom { get; set; }

        public bool this[int idx]
        {
            get { return this.itemsIncludedInKnapsack[idx]; }
        }

        public void EnsureFitness()
        {
            while (true)
            {
                this.lastTotalCost = 0;
                this.lastTotalWeight = 0;

                for (var i = 0; i < this.itemsIncludedInKnapsack.Length; i++)
                {
                    if (!this.IncludeInKnapsack[i])
                    {
                        continue;
                    }

                    this.lastTotalCost += this.configuration.Items[i].Cost;
                    this.lastTotalWeight += this.configuration.Items[i].Weight;
                }

                if (this.lastTotalWeight <= this.configuration.KnapsackVolume)
                {
                    break;
                }

                var choosenItems =
                    this.itemsIncludedInKnapsack.Select(
                        (inKnapsack, index) => new { IsIncluded = inKnapsack, Index = index })
                        .Where(item => item.IsIncluded).Select(item => item.Index).ToArray();

                var indexToExclude = Random.Next(0, choosenItems.Length);
                this.itemsIncludedInKnapsack[choosenItems[indexToExclude]] = false;
            }
        }

        public bool[] IncludeInKnapsack
        {
            get { return this.itemsIncludedInKnapsack; }
        }

        public int TotalCost
        {
            get
            {
                if (!this.lastTotalCost.HasValue)
                {
                    throw new InvalidOperationException("Fitness was not calculated yet.");
                }

                return this.lastTotalCost.Value;
            }
        }

        private void Randomize()
        {
            for (var i = 0; i < this.configuration.ItemsLength; i++)
            {
                this.IncludeInKnapsack[i] = Random.Next(0, 2) > 0;
            }
        }
    }
}
