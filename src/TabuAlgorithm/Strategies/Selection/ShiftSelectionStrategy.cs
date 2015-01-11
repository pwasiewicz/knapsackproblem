namespace TabuAlgorithm.Strategies.Selection
{
    using global::TabuAlgorithm.Helpers;

    internal class ShiftSelectionStrategy : ISelectionStrategy
    {
        public string Name
        {
            get { return "shift"; }
        }

        public void Select(bool[] source, int i, int j)
        {
            i = (i + 1)%source.Length;

            source.ShiftElement(j, i);
        }
    }
}
