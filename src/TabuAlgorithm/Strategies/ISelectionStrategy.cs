namespace TabuAlgorithm.Strategies
{
    internal interface ISelectionStrategy
    {
        string Name { get; }
        void Select(bool[] source, int i, int j);
    }
}
