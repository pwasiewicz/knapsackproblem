namespace KnapsackProblem.Services
{
    using Arguments;
    using KnapsackContract;
    using System.IO;

    public class ResultWriter : IResultWriter
    {
        private readonly TextWriter outWriter;
        private readonly ProgramArgs programArgs;

        public ResultWriter(TextWriter outWriter, ProgramArgs programArgs)
        {
            this.outWriter = outWriter;
            this.programArgs = programArgs;
        }

        public void Write(int currentCase, KnapsackItem[] items, IStopwatch watcher)
        {
            var totalWeight = 0;
            var totalCost = 0;


            foreach (var item in items)
            {
                totalCost += item.Cost;
                totalWeight += item.Weight;
            }

            outWriter.WriteLine("=====");

            if (!this.programArgs.ShortenedOutput)
            {
                outWriter.WriteLine("Result of case {0}:", currentCase);

                foreach (var item in items)
                {
                    totalCost += item.Cost;
                    totalWeight += item.Weight;

                    outWriter.WriteLine("Weight: {0}, Cost: {1}", item.Weight, item.Cost);
                }

                outWriter.WriteLine();
                outWriter.WriteLine("Total cost: {0}", totalCost);
                outWriter.WriteLine("Total weight: {0}", totalWeight);
                outWriter.WriteLine("Total time {0}", watcher);
            }
            else
            {
                outWriter.WriteLine("Result of case {0}: {1} in {2} ms", currentCase, totalCost, watcher.Elapsed());
            }
        }
    }
}