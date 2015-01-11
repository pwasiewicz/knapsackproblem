namespace KnapsackProblem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using KnapsackContract;

    public class FactoryResolver
    {
        private readonly IEnumerable<IKnapsackSolverFactory> factories;

        public FactoryResolver(IEnumerable<IKnapsackSolverFactory> factories)
        {
            this.factories = factories;
        }

        public IKnapsackSolverFactory GetFactory(string name)
        {
            var loweredName = name.ToLowerInvariant();
            return
                this.factories.FirstOrDefault(factory =>
                                              factory.Name.ToLowerInvariant()
                                                     .Equals(loweredName));
        }
    }
}
