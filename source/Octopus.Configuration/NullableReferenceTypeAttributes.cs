using System;
#if !HAS_NULLABLE_REF_TYPES
using System;

/// <summary>
/// These attributes replicate the ones from System.Diagnostics.CodeAnalysis, and are here so we can still compile against the older frameworks.
/// </summary>

namespace Octopus.Configuration
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
    public sealed class NotNullIfNotNullAttribute : Attribute
    {
        public NotNullIfNotNullAttribute(string parameterName)
        {
            this.ParameterName = parameterName;
        }

        public string ParameterName { get; }
    }
}
#endif