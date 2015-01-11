namespace KnapsackGeneticAlgorithm.Crossovering.Crossovers
{
    using System;

    internal class SinglePointCrossover : ICrossover
    {
        private static readonly Random RandomGenerator = new Random();

        public Chromosome Crossover(Chromosome parent1, Chromosome parent2)
        {
            var child = new Chromosome(parent1);
            var splitPoint = RandomGenerator.Next(1, parent2.Items.Length);

            for (var i = splitPoint; i < parent2.Items.Length; i++)
            {
                child.Items[i] = parent2.Items[i];
            }

            return child;
        }
    }
}
