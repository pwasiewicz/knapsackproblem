namespace KnapsackGeneticAlgorithm.Arguments
{
    using CommandLine;

    internal class AlgArguments
    {
        [Option('g', "generations", DefaultValue = 1000, HelpText = "Number of generations.")]
        public int Generations { get; set; }

        [Option('p', "population", DefaultValue = 100, HelpText = "Number of chromosomes in one population.")]
        public int Population { get; set; }

        [Option('m', "mutation", DefaultValue = 0.1, HelpText = "Probability of child chromosome mutation.")]
        public double MutationProbability { get; set; }

        [Option("force-generations", DefaultValue = false, HelpText = "Forces to use all generations to search solution. Otherwise, when 75% has the same fitness - algorithm is ended.")]
        public bool ForceAllGenerations { get; set; }
    }
}
