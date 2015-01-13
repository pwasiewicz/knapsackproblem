namespace KnapsackProblem.IoC
{
    using KnapsackContract;
    using KnapsackGeneticAlgorithm;
    using MiniAutFac;
    using Services;
    using TabuAlgorithm;

    internal class IoCRegistration
    {
        public static ContainerBuilder Register(ContainerBuilder cntBld)
        {
            cntBld.Register<TabuAlgorithmFactory>()
                  .As<IKnapsackSolverFactory>()
                  .PerLifetimeScope();

            cntBld.Register<GeneticAlgorithmFactory>()
                  .As<IKnapsackSolverFactory>()
                  .PerLifetimeScope();

            cntBld.Register<FactoryResolver>().PerLifetimeScope();
            cntBld.Register<ResultWriter>().PerLifetimeScope();

            return cntBld;
        }
    }
}
