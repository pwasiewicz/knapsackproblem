namespace KnapsackProblem.Services
{
    using KnapsackContract;

    public interface IFactoryResolver
    {
        IKnapsackSolverFactory GetFactory(string name);
    }
}