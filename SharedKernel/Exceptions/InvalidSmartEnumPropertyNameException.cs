namespace SharedKernel.Exceptions
{
    using System;

    [Serializable]
    public class InvalidSmartEnumPropertyName : Exception
    {
        public InvalidSmartEnumPropertyName(string property, string enumVal)
            : base($"The value `{enumVal}` is not valid for property `{property}`.")
        { }
    }
}