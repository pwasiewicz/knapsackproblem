namespace TabuAlgorithm
{
    using System;
    using KnapsackContract;

    internal class TabuMove
    {
        private static readonly Random Random = new Random();

        private readonly int tabuElementIndex;
        private readonly int sourceElementIndex;

        private TabuMove(int tabuElementIndex, int sourceElementIndex)
        {
            this.tabuElementIndex = tabuElementIndex;
            this.sourceElementIndex = sourceElementIndex;
        }

        public int Element
        {
            get { return this.tabuElementIndex; }
        }

        public int Source
        {
            get { return this.sourceElementIndex; }
        }

        public static TabuMove Next(KnapsackConfiguration configuration)
        {
            return new TabuMove(Random.Next(configuration.ItemsLength), Random.Next(configuration.ItemsLength));
        }

        private bool Equals(TabuMove other)
        {
            return this.sourceElementIndex == other.sourceElementIndex &&
                   this.tabuElementIndex == other.tabuElementIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && this.Equals((TabuMove)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.sourceElementIndex * 397) ^ this.tabuElementIndex;
            }
        }
    }
}
