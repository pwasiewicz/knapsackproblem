namespace KnapsackGeneticAlgorithm.Arguments
{
    using CommandLine;

    internal class AlgArguments
    {
        [Option('g', "generations",DefaultValue = 1000)]
        public int Generations { get; set; }

        [Option('p', "population", DefaultValue = 100)]
        public int Population { get; set; }

        [Option('m', "mutation", DefaultValue = 0.1)]
        public float MutationProbability { get; set; }
    }
}
