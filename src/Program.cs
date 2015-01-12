namespace KnapsackProblem
{
    using CommandLine;
    using Exceptions;
    using KnapsackContract;
    using KnapsackContract.Exception;
    using KnapsackProblem.Arguments;
    using KnapsackProblem.IoC;
    using KnapsackProblem.Services;
    using MiniAutFac;
    using MiniAutFac.Interfaces;
    using System;
    using System.IO;

    public class Program
    {
        private const int InvalidAlgorithmCode = 2;
        private const int InvalidArgumentsCode = 1;

        public static void Main(string[] args)
        {
            var programArgs = new ProgramArgs();
            var parser = new Parser(settings =>
                                    {
                                        settings.HelpWriter = OutWriter;
                                        settings.IgnoreUnknownArguments = true;
                                    });

            parser.ParseArgumentsStrict(args, programArgs, () =>
                                                           {
                                                               WriteLine("Program ran with invalid arguments.");
                                                               Environment.Exit(InvalidArgumentsCode);
                                                           });

            using (var lifetimeScope = BuildContainer())
            {
                var factoryResolver = lifetimeScope.Resolve<FactoryResolver>();
                var solverFactory = factoryResolver.GetFactory(programArgs.AlgorithmName);
                if (solverFactory == null)
                {
                    WriteLine("Cannot find specified algorithm implementation.");
                    Environment.Exit(InvalidAlgorithmCode);
                }

                RunProgram(lifetimeScope, solverFactory, programArgs, args);
            }

            if (programArgs.WaitForKey)
            {
                Console.ReadKey();
            }
        }

        private static ILifetimeScope BuildContainer()
        {
            var bld = new ContainerBuilder(); ;
            return IoCRegistration.Register(bld).Build();
        }

        private static void RunProgram(IResolvable lifetimeScope,
                                       IKnapsackSolverFactory solverFactory,
                                       ProgramArgs programArgs,
                                       params string[] args)
        {
            KnapsackConfiguration knapsackConfiguration;

            try
            {
                knapsackConfiguration = new KnapsackReader(programArgs.File).ReadConfiguration();
            }
            catch (ReadingConfigurationException ex)
            {
                WriteLine("An error occured while reading knapsack items and knapsack configuration: {0}", ex.Message);
                return;
            }

            var resultWriter = lifetimeScope.Resolve<ResultWriter>();

            try
            {
                var solver = solverFactory.Create(OutWriter, args);
                solver.Init(knapsackConfiguration);

                var solution = solver.Solve();
                resultWriter.Write(OutWriter, solution);
            }
            catch (AlgorithmInitializationException ex)
            {
                if (!ex.CustomOutput)
                {
                    WriteLine("An error occured while initializing algorithm: {0}", ex.Message);
                }
            }
        }

        public static TextWriter OutWriter
        {
            get { return Console.Out; }
        }

        public static void WriteLine(string format, params object[] args)
        {
            OutWriter.WriteLine(format, args);
        }
    }
}
