namespace TabuAlgorithm.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class SelectionStrategyGetter
    {
        private IDictionary<string, ISelectionStrategy> selectionStrategies;

        public ISelectionStrategy Get(string name)
        {
            if (this.selectionStrategies == null)
            {
                this.Evalute();
            }

            return this.selectionStrategies.ContainsKey(name) ? this.selectionStrategies[name] : null;
        }

        public ISelectionStrategy this[string name]
        {
            get { return this.Get(name); }
        }

        public bool HasStrategy(string name)
        {
            if (this.selectionStrategies == null)
            {
                this.Evalute();
            }

            return this.selectionStrategies.ContainsKey(name);
        }

        private void Evalute()
        {
            this.selectionStrategies = new Dictionary<string, ISelectionStrategy>();

            var types =
                Assembly.GetExecutingAssembly()
                        .GetTypes()
                        .Where(
                            type =>
                            type.IsAssignableFrom(typeof (ISelectionStrategy)) && type.IsClass && !type.IsAbstract);

            foreach (var instance in types.Select(type => (ISelectionStrategy) Activator.CreateInstance(type)))
            {
                this.selectionStrategies.Add(instance.Name, instance);
            }
        }
    }
}
