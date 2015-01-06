namespace KnapsackProblem.Arguments
{
    using CommandLine;

    public class ProgramArgs
    {
        [Option('w', "wait", DefaultValue = false)]
        public bool WaitForKey { get; set; }
    }
}
