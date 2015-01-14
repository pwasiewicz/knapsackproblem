namespace KnapsackProblem.Services
{
    using Exceptions;
    using KnapsackContract;
    using KnapsackContract.Exception;
    using System.IO;

    internal class ProgramImpl : IProgramImpl
    {
        private readonly string[] programArgs;
        private readonly IResultWriter resultWriter;
        private readonly TextWriter outWriter;
        private readonly IKnapsackReader knapsackReader;


        public ProgramImpl(string[] programArgs, IResultWriter resultWriter,
                           TextWriter outWriter, IKnapsackReader knapsackReader)
        {
            this.programArgs = programArgs;
            this.resultWriter = resultWriter;
            this.outWriter = outWriter;
            this.knapsackReader = knapsackReader;
        }

        public void Run(IKnapsackSolverFactory solverFactory)
        {
            KnapsackConfiguration[] knapsackConfigurations;

            try
            {
                knapsackConfigurations = this.knapsackReader.ReadConfigurations();
            }
            catch (ReadingConfigurationException ex)
            {
                this.outWriter.WriteLine(
                                         "An error occured while reading knapsack items and knapsack configuration: {0}",
                                         ex.Message);
                return;
            }

            var currentCase = 1;

            foreach (var configuration in knapsackConfigurations)
            {
                try
                {
                    var watcher = new Stopwatch();
                    var solver = solverFactory.Create(this.outWriter, this.programArgs);

                    watcher.Start();

                    solver.Init(configuration);
                    var solution = solver.Solve();

                    watcher.Stop();

                    resultWriter.Write(currentCase, solution, watcher);
                }
                catch (AlgorithmInitializationException ex)
                {
                    if (!ex.CustomOutput)
                    {
                        this.outWriter.WriteLine("An error occured while initializing algorithm: {0}", ex.Message);
                    }
                }

                currentCase += 1;
            }
        }
    }
}
