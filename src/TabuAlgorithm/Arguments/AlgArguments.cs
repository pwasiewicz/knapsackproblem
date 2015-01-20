namespace TabuAlgorithm.Arguments
{
    using CommandLine;

    internal class AlgArguments
    {
        [Option('t', "tabu", DefaultValue = 150, HelpText = "Maximum size of tabu search list.")]
        public int TabuSize { get; set; }

        [Option('i', "iterations", DefaultValue = 3000, HelpText = "Number of iterations.")]
        public int Iterations { get; set; }

        [Option('n', "neighbourhoods", DefaultValue = 200, HelpText = "Quantity of neighbourhoods generated for best solution.")]
        public int Neighbourhoods { get; set; }

        [Option('s', "selection", DefaultValue = "swap", HelpText = "Selection algorithm name.")]
        public string Selection { get; set; }
    }
}
