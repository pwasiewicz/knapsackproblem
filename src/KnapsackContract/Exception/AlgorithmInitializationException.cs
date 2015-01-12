namespace KnapsackContract.Exception
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AlgorithmInitializationException : Exception
    {
        public AlgorithmInitializationException()
        {
        }

        public AlgorithmInitializationException(string message)
            : base(message)
        {
        }

        public AlgorithmInitializationException(string message, bool customOutput)
            : base(message)
        {
            this.CustomOutput = customOutput;
        }

        public AlgorithmInitializationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public AlgorithmInitializationException(string message, bool customOutput, Exception inner)
            : base(message, inner)
        {
            this.CustomOutput = customOutput;
        }

        protected AlgorithmInitializationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        public bool CustomOutput { get; private set; }
    }
}
