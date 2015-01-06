namespace TabuAlgorithm
{
    using KnapsackContract;

    public class TabuAlgorithmFactory : IKnapsackSolverFactory
    {
        public string Name
        {
            get { return "tabu"; }
        }
        public IKnapsackSolver Create(params string[] args)
        {
            return new TabuAlgorithm();
        }
    }
}
