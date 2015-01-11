namespace KnapsackGeneticAlgorithm.Mutating.Strategies
{
    using System;

    internal class SimpleMutationStrategy : IMutationStrategy
    {
        private static readonly Random RandomGenerator = new Random();

        public void Mutate(Chromosome chromosome)
        {
            var mutated = RandomGenerator.Next(0, chromosome.Items.Length);
            chromosome.Items[mutated] = !chromosome.Items[mutated];
        }
    }
}
