namespace KnapsackContract
{
    using System.IO;

    public interface IKnapsackSolverFactory
    {
        string Name { get; }

        IKnapsackSolver Create(TextWriter outWriter, params string[] args);
    }
}
