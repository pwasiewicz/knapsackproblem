namespace KnapsackProblem.Services
{
    using System.IO;
    using KnapsackContract;

    public interface IResultWriter
    {
        void Write(TextWriter outWriter, KnapsackItem[] items);
    }
}