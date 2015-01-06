namespace KnapsackContract
{
    public interface IKnapsackSolverFactory
    {
        string Name { get; }

        IKnapsackSolver Create(params string[] args);
    }
}
