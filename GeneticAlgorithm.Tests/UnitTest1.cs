namespace GeneticAlgorithm.Tests
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using KnapsackContract;
    using KnapsackGeneticAlgorithm;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var testMethod = new GeneticAlgorithm(10000, 5, 0.1);

            testMethod.Init(new KnapsackConfiguration
                            {
                                KnapsackVolume = 20,
                                Items = new KnapsackItem[]
                                        {
                                            new KnapsackItem
                                            {
                                                Cost = 3,
                                                Weight = 2
                                            },
                                            new KnapsackItem
                                            {
                                                Cost = 10,
                                                Weight = 5
                                            },
                                            new KnapsackItem
                                            {
                                                Cost = 3,
                                                Weight = 1
                                            },
                                            new KnapsackItem
                                            {
                                                Cost = 30,
                                                Weight = 10
                                            },
                                            new KnapsackItem
                                            {
                                                Cost = 10,
                                                Weight = 7
                                            },
                                            new KnapsackItem
                                            {
                                                Cost = 1,
                                                Weight = 15
                                            },
                                            new KnapsackItem
                                            {
                                                Cost = 8,
                                                Weight = 5
                                            },
                                            new KnapsackItem
                                            {
                                                Cost = 4,
                                                Weight = 6
                                            },
                                            new KnapsackItem
                                            {
                                                Cost = 6,
                                                Weight = 4
                                            },
                                            new KnapsackItem
                                            {
                                                Cost = 7,
                                                Weight = 8
                                            },
                                            new KnapsackItem
                                            {
                                                Cost = 9,
                                                Weight = 3
                                            }
                                        }
                            });

            var result = testMethod.Solve();
        }
    }
}
