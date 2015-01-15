namespace KnapsackGeneticAlgorithm
{
    using Arguments;
    using CommandLine;
    using KnapsackContract;
    using System;
    using System.IO;

    public class GeneticAlgorithmFactory : IKnapsackSolverFactory
    {
        public string Name
        {
            get { return "genetic"; }
        }

        public IKnapsackSolver Create(TextWriter outWriter, params string[] args)
        {
            var algArgs = new AlgArguments();
            var parser = new Parser(cfg =>
                                    {
                                        cfg.HelpWriter = outWriter;
                                        cfg.IgnoreUnknownArguments = true;
                                    });

            parser.ParseArgumentsStrict(args, algArgs, () =>
                                                       {
                                                           outWriter.WriteLine("Invalid arguments for genetic algorithm");
                                                           Environment.Exit(4);
                                                       });


            return new GeneticAlgorithm(algArgs.Generations, algArgs.Population, algArgs.MutationProbability, algArgs.ForceAllGenerations);
        }
    }
}
