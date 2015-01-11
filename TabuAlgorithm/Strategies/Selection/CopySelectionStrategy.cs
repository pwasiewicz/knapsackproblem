namespace TabuAlgorithm.Strategies.Selection
{
    internal class CopySelectionStrategy : ISelectionStrategy
    {
        public void Select(bool[] source, int i, int j)
        {
            source[i] = source[j];
        }
    }
}
