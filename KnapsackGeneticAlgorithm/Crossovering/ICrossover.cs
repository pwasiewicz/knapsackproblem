namespace KnapsackGeneticAlgorithm.Crossovering
{
    internal interface ICrossover
    {
        Chromosome Crossover(Chromosome parent1, Chromosome parent2);
    }
}
