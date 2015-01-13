namespace KnapsackProblem.Services
{
    using KnapsackContract;
    using System.IO;

    public class ResultWriter : IResultWriter
    {
        public void Write(TextWriter outWriter, KnapsackItem[] items)
        {
            var totalWeight = 0;
            var totalCost = 0;

            outWriter.WriteLine("Result:");

            foreach (var item in items)
            {
                totalCost += item.Cost;
                totalWeight += item.Weight;

                outWriter.WriteLine("Weight: {0}, Cost: {1}", item.Weight, item.Cost);
            }

            outWriter.WriteLine();
            outWriter.WriteLine("Total cost: {0}", totalCost);
            outWriter.WriteLine("Total weight: {0}", totalWeight);
        }
    }
}
