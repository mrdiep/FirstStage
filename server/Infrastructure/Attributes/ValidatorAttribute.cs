using System;

namespace Infrastructure
{
    public class EnableValidatorAttribute : Attribute
    {        
        public EnableValidatorAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
