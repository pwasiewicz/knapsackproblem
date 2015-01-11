namespace TabuAlgorithm.Helpers
{
    using System;

    internal static class ArrayHelpers
    {
        public static void ShiftElement<T>(this T[] array, int oldIndex, int newIndex)
        {
            if (oldIndex == newIndex)
            {
                return;
            }

            var tmp = array[oldIndex];
            if (newIndex < oldIndex)
            {
                Array.Copy(array, newIndex, array, newIndex + 1, oldIndex - newIndex);
            }
            else
            {
                Array.Copy(array, oldIndex + 1, array, oldIndex, newIndex - oldIndex);
            }
            array[newIndex] = tmp;
        }
    }
}
