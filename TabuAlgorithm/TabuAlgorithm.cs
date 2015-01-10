namespace TabuAlgorithm
{
    using KnapsackContract;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TabuAlgorithm : IKnapsackSolver
    {
        private readonly  static Random Random = new Random();

        private Queue<KnapsackItem> tabu;
        private KnapsackConfiguration configuration;
        private TabuElement bestSolution;

        private readonly int tabuSize;
        private readonly int maxIterations;

        public TabuAlgorithm(int tabuSize, int maxIterations)
        {
            this.tabuSize = tabuSize;
            this.maxIterations = maxIterations;

            this.tabu = new Queue<KnapsackItem>(this.tabuSize);
        }


        public void Init(KnapsackConfiguration conf)
        {
            this.configuration = conf;
        }

        public KnapsackItem[] Solve()
        {
            if (this.configuration == null)
            {
                throw new InvalidOperationException("Algorithm must be inited first.");
            }

            this.bestSolution = this.NewTabuElement();

            var currentIteration = 0;
            while (currentIteration < this.maxIterations)
            {
                var neighbourhoods = this.BuildNeighbourhoods(this.bestSolution).ToList();
                if (!neighbourhoods.Any())
                {
                    break;
                }

                currentIteration += 1;
            }

            return this.BuildBestSolution().ToArray();
        }

        private IEnumerable<KnapsackItem> BuildBestSolution()
        {
            return
                this.configuration.Items.Select((item, idx) => this.bestSolution[idx] ? item : null)
                    .Where(item => item != null);
        }

        private TabuElement NewTabuElement(bool ensureFitness = true)
        {
            var elem = new TabuElement(this.configuration);
            if (ensureFitness)
            {
                elem.EnsureFitness();
            }

            return elem;
        }

        private IEnumerable<TabuElement> BuildNeighbourhoods(TabuElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            yield break;
        }

        private class TabuElement
        {
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

            private void Randomize()
            {
                for (var i = 0; i < this.configuration.ItemsLength; i++)
                {
                    this.IncludeInKnapsack[i] = Random.Next(0, 2) > 0;
                }
            }
        }
    }
}
