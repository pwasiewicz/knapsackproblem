namespace KnapsackProblem.IoC
{
    using System;
    using System.IO;
    using Arguments;
    using KnapsackContract;
    using KnapsackGeneticAlgorithm;
    using MiniAutFac;
    using MiniAutFac.Interfaces;
    using Services;
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

            cntBld.Register<ILifetimeScope>().As(ctx => ctx.CurrentLifetimeScope).PerDependency();
            cntBld.Register<TextWriter>().As(Console.Out).SingleInstance();
            cntBld.Register<ProgramImpl>().As<IProgramImpl>().PerLifetimeScope();

            cntBld.Register<string[]>().As(programArgs);

            return cntBld;
        }
    }
}
