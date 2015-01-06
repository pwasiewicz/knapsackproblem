namespace KnapsackGeneticAlgorithm
{
    using KnapsackContract;

    public class GeneticAlgorithmFactory : IKnapsackSolverFactory
    {
        public string Name
        {
            get { return "genetic"; }
        }

        public IKnapsackSolver Create(params string[] args)
        {
            return new GeneticAlgorithm(1000, 100, 0.2);
        }
    }
}
