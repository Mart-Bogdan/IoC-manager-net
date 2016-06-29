using System;

namespace Innahema.Ioc.Common.Attributes
{
    public class BindsToAttribute : Attribute
    {
        public Type TypeToBind { get; private set; }

        public BindsToAttribute(Type typeToBind)
        {
            TypeToBind = typeToBind;
        }
    }
}