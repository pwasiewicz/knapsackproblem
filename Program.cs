namespace KnapsackProblem
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using CommandLine;
    using KnapsackProblem.Arguments;
    using System;
    using KnapsackProblem.IoC;
    using KnapsackProblem.Services;
    using MiniAutFac;
    using MiniAutFac.Interfaces;

    public class Program
    {

        public static void Main(string[] args)
        {
            var programArgs = new ProgramArgs();
            var parser = new Parser(settings =>
                                    {
                                        settings.HelpWriter = null;
                                        settings.IgnoreUnknownArguments = true;
                                        settings.MutuallyExclusive = true;
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

                var solver = solverFactory.Create(args);
                // TODO solver
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

        public static void WriteLine(string format, params object[] args)
        {
            Console.Out.WriteLine(format, args);
        }
    }
}
