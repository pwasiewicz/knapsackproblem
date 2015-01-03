namespace KnapsackGeneticAlgorithm
{
    using System;
    using System.Linq;
    using KnapsackContract;

    internal class Chromosome
    {
        private static readonly Random RandomGenerator = new Random();

        private readonly int itemsSize;

        private readonly KnapsackConfiguration knapsackConfiguration;

        private readonly bool[] itemsIncludedInKnapsack;

        private int? lastTotalWeight;

        private int? lastTotalCost;

        public Chromosome(KnapsackConfiguration knapsackConfiguration)
        {
            this.knapsackConfiguration = knapsackConfiguration;
            this.itemsSize = knapsackConfiguration.Items.Length;

            this.itemsIncludedInKnapsack = new bool[this.itemsSize];
            this.RandomItems();
        }

        public Chromosome(Chromosome chromosome)
        {
            this.knapsackConfiguration = chromosome.knapsackConfiguration;
            this.itemsSize = chromosome.itemsSize;
            this.itemsIncludedInKnapsack = chromosome.itemsIncludedInKnapsack;
        }

        public bool[] Items
        {
            get { return this.itemsIncludedInKnapsack; }
        }

        public int TotalWeight
        {
            get
            {
                if (!this.lastTotalWeight.HasValue)
                {
                    throw new InvalidOperationException("Fitness was not calculated yet.");
                }

                return this.lastTotalWeight.Value;
            }
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

        public void EnsureFitness()
        {
            while (true)
            {
                this.lastTotalCost = 0;
                this.lastTotalWeight = 0;

                for (var i = 0; i < this.itemsSize; i++)
                {
                    if (!this.itemsIncludedInKnapsack[i])
                    {
                        continue;
                    }

                    this.lastTotalCost += this.knapsackConfiguration.Items[i].Cost;
                    this.lastTotalWeight += this.knapsackConfiguration.Items[i].Weight;
                }

                if (this.lastTotalWeight <= this.knapsackConfiguration.KnapsackVolume)
                {
                    break;
                }

                var choosenItems =
                    this.itemsIncludedInKnapsack.Select(
                        (inKnapsack, index) => new {IsIncluded = inKnapsack, Index = index})
                        .Where(item => item.IsIncluded).Select(item => item.Index).ToArray();

                var indexToExclude = RandomGenerator.Next(0, choosenItems.Length);
                this.itemsIncludedInKnapsack[choosenItems[indexToExclude]] = false;
            }
        }

        private void RandomItems()
        {
            for (var i = 0; i < this.itemsSize; i++)
            {
                this.itemsIncludedInKnapsack[i] = RandomGenerator.NextDouble() > 0.5;
            }
        }

    }
}
