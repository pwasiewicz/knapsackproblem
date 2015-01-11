﻿namespace TabuAlgorithm
{
    using global::TabuAlgorithm.Strategies;
    using KnapsackContract;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TabuAlgorithm : IKnapsackSolver
    {
        private const string DefaultStrategy = "swap";

        private readonly  static Random Random = new Random();

        private readonly Queue<TabuMove> tabu;
        private readonly SelectionStrategyGetter selectionStrategyGetter;
        private KnapsackConfiguration configuration;
        private TabuElement bestSolution;

        private readonly int tabuSize;
        private readonly int neighbourhoodsCount;
        private readonly int maxIterations;

        private string strategy;

        public TabuAlgorithm(int tabuSize, int maxIterations, int neighbourhoodsCount)
        {
            this.tabuSize = tabuSize;
            this.maxIterations = maxIterations;
            this.neighbourhoodsCount = neighbourhoodsCount;

            this.tabu = new Queue<TabuMove>(this.tabuSize);
            this.selectionStrategyGetter = new SelectionStrategyGetter();
            this.strategy = DefaultStrategy;
        }

        public string SelectionStrategy
        {
            private get { return this.strategy; }
            set
            {
                if (!this.selectionStrategyGetter.HasStrategy(value))
                {
                    throw new InvalidOperationException("Invalid strategy name.");
                }

                this.strategy = value;
            }
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

                this.bestSolution.CreatedFrom = null;

                neighbourhoods.ForEach(tabuElement => tabuElement.EnsureFitness());

                foreach (var ngh in neighbourhoods.Where(ngh => ngh.TotalCost > this.bestSolution.TotalCost))
                {
                    this.bestSolution = ngh;
                }

                if (this.bestSolution.CreatedFrom != null)
                {
                    if (this.tabu.Count >= this.tabuSize)
                    {
                        this.tabu.Dequeue();
                    }

                    this.tabu.Enqueue(this.bestSolution.CreatedFrom);
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

            var possibleNeighbourhoods = this.neighbourhoodsCount * 1000;

            var localNeighbourhoods = 0;
            var currentTry = 0;

            while (localNeighbourhoods < this.neighbourhoodsCount)
            {
                localNeighbourhoods += 1;

                var move = TabuMove.Next(this.configuration);
                if (this.tabu.Contains(move))
                {
                    currentTry += 1;
                    if (currentTry >= possibleNeighbourhoods)
                    {
                        yield break;
                    }

                    continue;
                }

                currentTry = 0;

                var ngh = new TabuElement(element)
                          {
                              CreatedFrom = move
                          };

                this.selectionStrategyGetter[this.SelectionStrategy]
                    .Select(ngh.IncludeInKnapsack, move.Element, move.Source);

                yield return ngh;

            }
        }

        private class TabuMove
        {
            private readonly int tabuElementIndex;
            private readonly int sourceElementIndex;

            private TabuMove(int tabuElementIndex, int sourceElementIndex)
            {
                this.tabuElementIndex = tabuElementIndex;
                this.sourceElementIndex = sourceElementIndex;
            }

            public int Element
            {
                get { return this.tabuElementIndex; }
            }

            public int Source
            {
                get { return this.sourceElementIndex; }
            }

            public static TabuMove Next(KnapsackConfiguration configuration)
            {
                return new TabuMove(Random.Next(configuration.ItemsLength), Random.Next(configuration.ItemsLength));
            }

            private bool Equals(TabuMove other)
            {
                return this.sourceElementIndex == other.sourceElementIndex &&
                       this.tabuElementIndex == other.tabuElementIndex;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return obj.GetType() == this.GetType() && this.Equals((TabuMove) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (this.sourceElementIndex*397) ^ this.tabuElementIndex;
                }
            }
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
}