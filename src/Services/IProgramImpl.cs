namespace KnapsackProblem.Services
{
    using KnapsackContract;

    public interface IProgramImpl
    {
        void Run(IKnapsackSolverFactory factoryResolver);
    }
}
