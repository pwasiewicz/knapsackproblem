﻿namespace KnapsackGeneticAlgorithm
{
    using System;
    using KnapsackContract;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using KnapsackGeneticAlgorithm.Crossovering.Crossovers;
    using KnapsackGeneticAlgorithm.Mutating;
    using KnapsackGeneticAlgorithm.Mutating.Strategies;
    using KnapsackGeneticAlgorithm.Selection.Strategies;

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

        public void Init(KnapsackConfiguration configuration)
        {
            this.currentPopulation = new List<Chromosome>();
            for (var i = 0; i < this.populationCount; i++)
            {
                this.currentPopulation.Add(new Chromosome(configuration));
            }

            this.configuration = configuration;
        }

        public KnapsackItem[] Solve()
        {
            var result = this.DoWork();

            return this.configuration.Items.Zip(result.Items, (item, b) => new
                                                                           {
                                                                               Item = item,
                                                                               IsInKnapsack = b
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

                Debug.Assert(this.currentPopulation.Count == childPopulation.Count,
                             "this.currentPopulation.Count == childPopulation.Count");

                this.currentPopulation = childPopulation;
                currentGeneration += 1;
            }

            return result ?? this.FitnessesEnough(resultOnly: true);
        }

        private Chromosome FitnessesEnough(bool resultOnly= false)
        {
            if (resultOnly)
            {
                return this.currentPopulation.OrderByDescending(chromosome => chromosome.TotalCost).First();
            }

            var fitnesses =
                this.currentPopulation.GroupBy(chromosome => chromosome.TotalCost)
                    .OrderByDescending(group => group.Count())
                    .First();

            var isEnd= (double) fitnesses.Count()/this.populationCount >= PercentageOfPopulationThatEndsSearching;
            return isEnd ? fitnesses.First() : null;
        }

        private void EvaluteFitness()
        {
            this.currentPopulation.ForEach(chromosome => chromosome.EnsureFitness());
        }

        private void PerformElitism(List<Chromosome> target)
        {
            target.AddRange(this.currentPopulation.OrderByDescending(chr => chr.TotalCost).Take(2));
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