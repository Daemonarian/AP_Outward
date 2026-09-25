using System;
using System.Collections.Generic;
using System.Text;

namespace Conflux.Schema.Deserializer
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class ConfluxPolymorphicAttribute : Attribute
    {
    }
}
