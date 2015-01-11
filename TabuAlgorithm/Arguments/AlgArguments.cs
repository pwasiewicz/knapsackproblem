namespace TabuAlgorithm.Arguments
{
    using CommandLine;

    internal class AlgArguments
    {
        [Option('t', "tabu", DefaultValue = 100)]
        public int TabuSize { get; set; }

        [Option('i', "iterations", DefaultValue = 1000)]
        public int Iterations { get; set; }

        [Option('n', "neighbourhoods", DefaultValue = 0.1)]
        public int Neighbourhoods { get; set; }

        [Option('s', "selection", DefaultValue = "swap")]
        public string Selection { get; set; }
    }
}
