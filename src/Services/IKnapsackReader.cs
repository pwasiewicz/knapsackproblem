namespace KnapsackProblem.Services
{
    using KnapsackContract;

    public interface IKnapsackReader
    {
        KnapsackConfiguration[] ReadConfiguration();
    }
}