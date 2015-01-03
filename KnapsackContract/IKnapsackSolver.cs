namespace KnapsackContract
{
    public interface IKnapsackSolver
    {
        void Init(KnapsackConfiguration configuration);

        KnapsackItem[] Solve();
    }
}
