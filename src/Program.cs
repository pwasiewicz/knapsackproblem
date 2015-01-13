namespace KnapsackProblem
{
    using Arguments;
    using CommandLine;
    using Exceptions;
    using IoC;
    using KnapsackContract;
    using KnapsackContract.Exception;
    using MiniAutFac;
    using MiniAutFac.Interfaces;
    using Services;
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

            using (var lifetimeScope = BuildContainer(args))
            {
                var factoryResolver = lifetimeScope.Resolve<IFactoryResolver>();
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

        private static ILifetimeScope BuildContainer(string[] programArgs)
        {
            var bld = new ContainerBuilder();
            return IoCRegistration.Register(bld, programArgs).Build();
        }

        private static void RunProgram(IResolvable lifetimeScope,
                                       IKnapsackSolverFactory solverFactory,
                                       ProgramArgs programArgs,
                                       params string[] args)
        {
            KnapsackConfiguration[] knapsackConfigurations;

            try
            {
                knapsackConfigurations = new KnapsackReader(programArgs.File).ReadConfiguration();
            }
            catch (ReadingConfigurationException ex)
            {
                WriteLine("An error occured while reading knapsack items and knapsack configuration: {0}", ex.Message);
                return;
            }

            var resultWriter = lifetimeScope.Resolve<IResultWriter>();

            foreach (var configuration in knapsackConfigurations)
            {

                try
                {
                    var solver = solverFactory.Create(OutWriter, args);
                    solver.Init(configuration);

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
