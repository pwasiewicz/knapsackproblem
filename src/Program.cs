namespace KnapsackProblem
{
    using CommandLine;
    using KnapsackContract;
    using KnapsackProblem.Arguments;
    using KnapsackProblem.IoC;
    using KnapsackProblem.Services;
    using MiniAutFac;
    using MiniAutFac.Interfaces;
    using System;

    public class Program
    {

        public static void Main(string[] args)
        {
            var programArgs = new ProgramArgs();
            var parser = new Parser(settings =>
                                    {
                                        settings.HelpWriter = null;
                                        settings.IgnoreUnknownArguments = true;
                                    });

            parser.ParseArgumentsStrict(args, programArgs, () =>
                                                           {
                                                               WriteLine("Program ran with invalid arguments.");
                                                               Environment.Exit(1);
                                                           });

            using (var lifetimeScope = Build())
            {
                var factoryResolver = lifetimeScope.Resolve<FactoryResolver>();
                var solverFactory = factoryResolver.GetFactory(programArgs.AlgorithmName);
                if (solverFactory == null)
                {
                    WriteLine("Cannot find specified algorithm implementation");
                    Environment.Exit(2);
                }

                RunProgram(lifetimeScope, solverFactory, programArgs, args);
            }

            if (programArgs.WaitForKey)
            {
                Console.ReadKey();
            }
        }

        private static ILifetimeScope Build()
        {
            var bld = new ContainerBuilder();
            IoCRegistration.Register(bld);
            return bld.Build();
        }

        private static void RunProgram(IResolvable lifetimeScope,
                                       IKnapsackSolverFactory solverFactory, 
                                       ProgramArgs programArgs,
                                       params string[] args)
        {
            var knapsackConfiguration = new KnapsackReader(programArgs.File).ReadConfiguration();

            var solver = solverFactory.Create(Console.Out, args);
            solver.Init(knapsackConfiguration);

            var solution = solver.Solve();

            var resultWriter = lifetimeScope.Resolve<ResultWriter>();
            resultWriter.Write(Console.Out, solution);
        }

        public static void WriteLine(string format, params object[] args)
        {
            Console.Out.WriteLine(format, args);
        }
    }
}
