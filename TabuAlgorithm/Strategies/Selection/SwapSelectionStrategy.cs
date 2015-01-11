namespace TabuAlgorithm.Strategies.Selection
{
    internal class SwapSelectionStrategy: ISelectionStrategy
    {
        public void Select(bool[] source, int i, int j)
        {
            var tmp = source[i];
            source[i] = source[j];
            source[j] = tmp;
        }
    }
}
