using System;

namespace Innahema.Ioc.Common.Attributes
{
    /// <summary>
    /// Specifys that this implementation would be default, if multiple services implements same interface
    /// </summary>
    public class DefaultImplementation : Attribute
    {
    }
}