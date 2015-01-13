namespace KnapsackProblem.Services
{
    internal class Stopwatch : IStopwatch
    {
        private readonly System.Diagnostics.Stopwatch internalStopwatch;

        public Stopwatch()
        {
            this.internalStopwatch = new System.Diagnostics.Stopwatch();
        }

        public void Start()
        {
            this.internalStopwatch.Restart();
        }

        public void Stop()
        {
            this.internalStopwatch.Stop();
        }

        public string Elapsed()
        {
            return this.internalStopwatch.Elapsed.ToString("G");
        }

        public override string ToString()
        {
            return this.Elapsed();
        }
    }
}
