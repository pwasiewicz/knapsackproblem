namespace KnapsackContract
{
    public interface IKnapsackSolver
    {
        void Init(KnapsackConfiguration conf);

        KnapsackItem[] Solve();
    }
}
