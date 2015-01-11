using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TabuAlgorithm.Tests
{
    using KnapsackContract;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var testMethod = new TabuAlgorithm(10, 1000, 20);

            testMethod.Init(new KnapsackConfiguration(new[]
                                                      {
                                                          new KnapsackItem(2, 3),
                                                          new KnapsackItem(6, 10),
                                                          new KnapsackItem(1, 3),
                                                          new KnapsackItem(10, 30),
                                                          new KnapsackItem(7, 10),
                                                          new KnapsackItem(15, 1),
                                                          new KnapsackItem(5, 8),
                                                          new KnapsackItem(6, 4),
                                                          new KnapsackItem(4, 8),
                                                          new KnapsackItem(8, 7),
                                                          new KnapsackItem(3, 9)
                                                      }, 20));

            var result = testMethod.Solve();
        }
    }
}
