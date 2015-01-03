namespace KnapsackContract.Utils
{
    using System;

    public class ArrayUtils
    {
        public static void MemSet<T>(T[] array, T value)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            int block = 32, index = 0;
            var length = Math.Min(block, array.Length);

            while (index < length)
            {
                array[index++] = value;
            }

            length = array.Length;
            while (index < length)
            {
                Buffer.BlockCopy(array, 0, array, index, Math.Min(block, length - index));
                index += block;
                block *= 2;
            }
        }
    }
}
