namespace TabuAlgorithm.Strategies.Selection
{
    internal class CopySelectionStrategy : ISelectionStrategy
    {
        public string Name
        {
            get { return "copy"; }
        }

        public void Select(bool[] source, int i, int j)
        {
            source[i] = source[j];
        }
    }
}
