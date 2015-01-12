namespace TabuAlgorithm
{
    using KnapsackContract;
    using Strategies;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TabuAlgorithm : IKnapsackSolver
    {
        private const string DefaultStrategy = "swap";

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
    }
}
