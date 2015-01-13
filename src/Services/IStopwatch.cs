namespace KnapsackProblem.Services
{
    public interface IStopwatch
    {
        void Start();
        void Stop();
        string Elapsed();
    }
}