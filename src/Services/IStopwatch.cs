namespace KnapsackProblem.Services
{
    internal interface IStopwatch
    {
        void Start();
        void Stop();
        string Elapsed();
    }
}