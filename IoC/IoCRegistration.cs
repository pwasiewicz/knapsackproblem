namespace KnapsackProblem.IoC
{
    using KnapsackContract;
    using KnapsackGeneticAlgorithm;
    using KnapsackProblem.Services;
    using MiniAutFac;
    using TabuAlgorithm;

    internal class IoCRegistration
    {
        public static void Register(ContainerBuilder cntBld)
        {
            cntBld.Register<TabuAlgorithmFactory>()
                  .As<IKnapsackSolverFactory>()
                  .PerLifetimeScope();

            cntBld.Register<GeneticAlgorithmFactory>()
                  .As<IKnapsackSolverFactory>()
                  .PerLifetimeScope();

            cntBld.Register<FactoryResolver>().PerLifetimeScope();
        }
    }
}
