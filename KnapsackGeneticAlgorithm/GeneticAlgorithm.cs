namespace KnapsackGeneticAlgorithm
{
    using KnapsackContract;
    using KnapsackGeneticAlgorithm.Crossovering.Crossovers;
    using KnapsackGeneticAlgorithm.Mutating;
    using KnapsackGeneticAlgorithm.Mutating.Strategies;
    using KnapsackGeneticAlgorithm.Selection.Strategies;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GeneticAlgorithm : IKnapsackSolver
    {
        private const float PercentageOfPopulationThatEndsSearching = 0.9f;

        private static readonly Random RandomGenerator = new Random();

        private readonly int maxGenerations;
        private readonly int populationCount;
        private readonly double mutationProbability;
        private KnapsackConfiguration configuration;

        private List<Chromosome> currentPopulation;

        public GeneticAlgorithm(int maxGenerations, int populationCount, double mutationProbability)
        {
            if (mutationProbability < 0.01)
            {
                throw new ArgumentOutOfRangeException("mutationProbability");
            }

            if (mutationProbability > 0.99)
            {
                throw new ArgumentOutOfRangeException("mutationProbability");
            }

            this.maxGenerations = maxGenerations;
            this.populationCount = populationCount;
            this.mutationProbability = mutationProbability;
        }

        public void Init(KnapsackConfiguration conf)
        {
            this.configuration = conf;
            this.currentPopulation = new List<Chromosome>();

            for (var i = 0; i < this.populationCount; i++)
            {
                this.currentPopulation.Add(new Chromosome(this.configuration));
            }
        }

        public KnapsackItem[] Solve()
        {
            var result = this.DoWork();

            return this.configuration.Items
                       .Zip(result.Items, (item, isInKnapsack) => new
                                                                  {
                                                                      Item = item,
                                                                      IsInKnapsack = isInKnapsack
                                                                  })
                       .Where(item => item.IsInKnapsack)
                       .Select(item => item.Item)
                       .ToArray();
        }

        private Chromosome DoWork()
        {
            var currentGeneration = 0;
            var populatingEnd = false;

            Chromosome result = null;

            while (!populatingEnd)
            {
                this.EvaluteFitness();

                var childPopulation = new List<Chromosome>();
                this.PerformElitism(childPopulation);

                var selectionStrategy = new RouletteWheelStrategy(this.currentPopulation);
                var crossover = new SinglePointCrossover();
                var mutationStrategy = new SimpleMutationStrategy();

                while (childPopulation.Count < this.populationCount)
                {
                    var parents = selectionStrategy.NextParents();
                    var child = crossover.Crossover(parents.Item1, parents.Item2);
                    
                    this.Mutate(mutationStrategy, child);

                    childPopulation.Add(child);
                }

                populatingEnd = currentGeneration > this.maxGenerations
                                            || (result = this.FitnessesEnough()) != null;
                if (populatingEnd)
                {
                    continue;
                }

                this.currentPopulation = childPopulation;
                currentGeneration += 1;
            }

            return result ?? this.FitnessesEnough(resultOnly: true);
        }

        private Chromosome FitnessesEnough(bool resultOnly = false)
        {
            if (resultOnly)
            {
                return this.currentPopulation.OrderByDescending(chromosome => chromosome.TotalCost).First();
            }

            var fitnesses =
                this.currentPopulation
                    .GroupBy(chromosome => chromosome.TotalCost)
                    .OrderByDescending(group => group.Count())
                    .First();

            var isEnd = (double) fitnesses.Count()/this.populationCount >= PercentageOfPopulationThatEndsSearching;
            return isEnd ? fitnesses.First() : null;
        }

        private void EvaluteFitness()
        {
            foreach (var chromosome in this.currentPopulation)
            {
                chromosome.EnsureFitness();
            }
        }

        private void PerformElitism(List<Chromosome> target)
        {
            var ordered = this.currentPopulation.OrderByDescending(chr => chr.TotalCost);
            target.AddRange(ordered.Take(2));
        }

        private void Mutate(IMutationStrategy mutationStrategy, Chromosome chromosome)
        {
            var dice = RandomGenerator.NextDouble();
            if (!(dice <= this.mutationProbability))
            {
                return;
            }

            mutationStrategy.Mutate(chromosome);
        }
    }
}
