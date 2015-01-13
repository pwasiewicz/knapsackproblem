namespace KnapsackProblem.Services
{
    using KnapsackContract;
    using System.IO;

    public class ResultWriter : IResultWriter
    {
        private readonly TextWriter outWriter;

        public ResultWriter(TextWriter outWriter)
        {
            this.outWriter = outWriter;
        }

        public void Write(int currentCase, KnapsackItem[] items, IStopwatch watcher)
        {
            var totalWeight = 0;
            var totalCost = 0;

            outWriter.WriteLine("=====");
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
    }
}