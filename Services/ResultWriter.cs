namespace KnapsackProblem.Services
{
    using System.IO;
    using KnapsackContract;

    public class ResultWriter
    {
        public void Write(TextWriter outWriter, KnapsackItem[] items)
        {
            outWriter.WriteLine("Result:");

            foreach (var item in items)
            {
                outWriter.WriteLine("Weight: {0}, Cost: {1}", item.Weight, item.Cost);
            }
        }
    }
}
