namespace KnapsackProblem
{
    using Arguments;
    using CommandLine;
    using IoC;
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

            using (var lifetimeScope = BuildContainer(args, programArgs))
            {
                var factoryResolver = lifetimeScope.Resolve<IFactoryResolver>();
                var solverFactory = factoryResolver.GetFactory(programArgs.AlgorithmName);
                if (solverFactory == null)
                {
                    WriteLine("Cannot find specified algorithm implementation.");
                    Environment.Exit(InvalidAlgorithmCode);
                }

                var programImpl = lifetimeScope.Resolve<IProgramImpl>();
                programImpl.Run(solverFactory);
            }

            if (programArgs.WaitForKey)
            {
                Console.ReadKey();
            }
        }

        private static ILifetimeScope BuildContainer(string[] programArgs, ProgramArgs programArgsParsed)
        {
            var bld = new ContainerBuilder();
            return IoCRegistration.Register(bld, programArgs, programArgsParsed).Build();
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
