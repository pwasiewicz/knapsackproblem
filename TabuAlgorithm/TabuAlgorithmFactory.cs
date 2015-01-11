namespace TabuAlgorithm
{
    using CommandLine;
    using global::TabuAlgorithm.Arguments;
    using KnapsackContract;
    using System;
    using System.IO;

    public class TabuAlgorithmFactory : IKnapsackSolverFactory
    {
        public string Name
        {
            get { return "tabu"; }
        }
        public IKnapsackSolver Create(TextWriter outWriter, params string[] args)
        {
            var options = new AlgArguments();

            var parser = new Parser(cfg =>
                                    {
                                        cfg.HelpWriter = null;
                                        cfg.IgnoreUnknownArguments = true;
                                    });

            parser.ParseArgumentsStrict(args, options, () =>
                                                       {
                                                           outWriter.WriteLine("Invalid arguments for tabu algorithm");
                                                           Environment.Exit(4);
                                                       });

            return new TabuAlgorithm(options.TabuSize, options.Iterations, options.Neighbourhoods)
                       {
                           SelectionStrategy
                               =
                               options
                               .Selection
                       };
        }
    }
}
