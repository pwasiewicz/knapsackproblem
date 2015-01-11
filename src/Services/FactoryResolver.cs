namespace KnapsackProblem.Services
{
    using KnapsackContract;
    using System.Collections.Generic;
    using System.Linq;

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
