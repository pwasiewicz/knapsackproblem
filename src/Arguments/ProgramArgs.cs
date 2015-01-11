namespace KnapsackProblem.Arguments
{
    using CommandLine;

    public class ProgramArgs
    {
        [Option('w', "wait", DefaultValue = false, HelpText = "If enabled, program waits for input to terminate application.")]
        public bool WaitForKey { get; set; }

        [Option('a', "algorithm", Required = true, HelpText = "Algorithm name used to solve knapsack problem.")]
        public string AlgorithmName { get; set; }

        [Option('f', "file", Required = true, HelpText = "File with possible knapsack items configuration.")]
        public string File { get; set; }
    }
}
