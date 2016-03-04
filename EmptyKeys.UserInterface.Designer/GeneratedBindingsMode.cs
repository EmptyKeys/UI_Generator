using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Describes modes for binding generator
    /// </summary>
    public enum GeneratedBindingsMode
    {
        // Bindings are generated
        Generated,
        // Bindings are ignored and using reflection
        Reflection,
        // Bindings can use both, reflection and generated
        Mixed,
        // Bindings with custom type
        Manual
    }
}
