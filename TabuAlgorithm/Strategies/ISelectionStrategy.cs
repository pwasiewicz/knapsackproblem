namespace TabuAlgorithm.Strategies
{
    internal interface ISelectionStrategy
    {
        void Select(bool[] source, int i, int j);
    }
}
