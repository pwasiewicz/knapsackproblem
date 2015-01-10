namespace TabuAlgorithm
{
    using System.IO;
    using KnapsackContract;

    public class TabuAlgorithmFactory : IKnapsackSolverFactory
    {
        public string Name
        {
            get { return "tabu"; }
        }
        public IKnapsackSolver Create(TextWriter outWriter, params string[] args)
        {
            return new TabuAlgorithm(10, 10, 10);
        }
    }
}
