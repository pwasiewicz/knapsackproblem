namespace KnapsackGeneticAlgorithm.Selection
{
    using System;

    internal interface ISelectionStrategy
    {
        Tuple<Chromosome, Chromosome> NextParents();
    }
}
