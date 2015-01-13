namespace KnapsackProblem.IoC
{
    using KnapsackContract;
    using KnapsackGeneticAlgorithm;
    using MiniAutFac;
    using Services;
    using TabuAlgorithm;

    internal class IoCRegistration
    {
        public static ContainerBuilder Register(ContainerBuilder cntBld, string[] programArgs)
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

            cntBld.Register<string[]>().As(programArgs);

            return cntBld;
        }
    }
}
