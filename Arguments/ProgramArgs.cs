namespace KnapsackProblem.Arguments
{
    using CommandLine;

    public class ProgramArgs
    {
        [Option('w', "wait", DefaultValue = false)]
        public bool WaitForKey { get; set; }

        [Option('a', "algorithm", Required = true)]
        public string AlgorithmName { get; set; }

        [Option('f', "file")]
        public string File { get; set; }
    }
}
