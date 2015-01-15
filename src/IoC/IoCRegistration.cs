namespace KnapsackProblem.IoC
{
    using Arguments;
    using KnapsackContract;
    using KnapsackGeneticAlgorithm;
    using MiniAutFac;
    using Services;
    using System;
    using System.IO;
    using System.Text;
    using TabuAlgorithm;

    internal class IoCRegistration
    {
        public static ContainerBuilder Register(ContainerBuilder cntBld, string[] programArgs, ProgramArgs programArgsParsed)
        {
            cntBld.Register<TabuAlgorithmFactory>()
                  .As<IKnapsackSolverFactory>()
                  .PerLifetimeScope();

            cntBld.Register<GeneticAlgorithmFactory>()
                  .As<IKnapsackSolverFactory>()
                  .PerLifetimeScope();

            cntBld.Register<FactoryResolver>().As<IFactoryResolver>().PerLifetimeScope();
            cntBld.Register<ResultWriter>().As<IResultWriter>().PerLifetimeScope();
            cntBld.Register<Stopwatch>().As<IStopwatch>().PerDependency();
            cntBld.Register<KnapsackReader>()
                  .As<IKnapsackReader>()
                  .PerLifetimeScope()
                  .WithNamedParameter("file", programArgsParsed.File);

            cntBld.Register<ProgramArgs>().As(programArgsParsed).SingleInstance();

            //small hack for MiniAutFac bug
            cntBld.Register<WriterStub>().As<TextWriter>().As(Console.Out).SingleInstance();
            cntBld.Register<ProgramImpl>().As<IProgramImpl>().PerLifetimeScope();

            cntBld.Register<string[]>().As(programArgs);

            return cntBld;
        }

        internal class WriterStub : TextWriter
        {
            public override Encoding Encoding
            {
                get { throw new NotImplementedException(); }
            }
        }
    }
}
