namespace KnapsackProblem.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ReadingConfigurationException : Exception
    {
        public ReadingConfigurationException() { }
        public ReadingConfigurationException(string message) : base(message) { }
        public ReadingConfigurationException(string message, Exception inner) : base(message, inner) { }

        protected ReadingConfigurationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}
