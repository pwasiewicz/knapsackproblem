namespace KnapsackGeneticAlgorithm.Selection.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class RouletteWheelStrategy : ISelectionStrategy
    {
        private static readonly Random RandomGenerator = new Random();

        private readonly List<KeyValuePair<Chromosome, double>> currentPopulation;

        public RouletteWheelStrategy(IEnumerable<Chromosome> currentPopulation)
        {
            var chromosomes = currentPopulation as IList<Chromosome> ?? currentPopulation.ToList();
            var totalCost = chromosomes.Sum(chr => chr.TotalCost);
            this.currentPopulation =
                chromosomes.ToDictionary(chr => chr, chr => (double) (chr.TotalCost)/totalCost)
                           .OrderBy(b => b.Value)
                           .ToList();
        }

        public Tuple<Chromosome, Chromosome> NextParents()
        {
            return new Tuple<Chromosome, Chromosome>(this.NextParent(), this.NextParent());
        }

        private Chromosome NextParent()
        {
            var roll = RandomGenerator.NextDouble();
            var cumulative = 0.0;
            foreach (var chromosomeProbabilityPair in this.currentPopulation)
            {
                cumulative += chromosomeProbabilityPair.Value;
                if (roll < cumulative)
                {
                    return chromosomeProbabilityPair.Key;
                }
            }

            return this.currentPopulation[this.currentPopulation.Count - 1].Key;
        }
    }
}
