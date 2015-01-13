namespace KnapsackProblem.Services
{
    using KnapsackContract;

    public interface IResultWriter
    {
        void Write(int currentCase, KnapsackItem[] items, IStopwatch watcher);
    }
}