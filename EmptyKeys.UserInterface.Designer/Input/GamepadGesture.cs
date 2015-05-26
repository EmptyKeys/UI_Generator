using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmptyKeys.UserInterface.Designer.Input
{
    /// <summary>
    /// Implements fake Gamepad Gesture for XAML designer
    /// </summary>
    public class GamepadGesture : InputGesture
    {
        /// <summary>
        /// Gets the game pad input.
        /// </summary>
        /// <value>
        /// The game pad input.
        /// </value>
        public GamepadInput GamepadInput
        {
            get;
            internal set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GamepadGesture"/> class.
        /// </summary>
        /// <param name="gamepadInput">The game pad input.</param>
        public GamepadGesture(GamepadInput gamepadInput)
        {
            GamepadInput = gamepadInput;
        }

        /// <summary>
        /// When overridden in a derived class, determines whether the specified <see cref="T:System.Windows.Input.InputGesture" /> matches the input associated with the specified <see cref="T:System.Windows.Input.InputEventArgs" /> object.
        /// </summary>
        /// <param name="targetElement">The target of the command.</param>
        /// <param name="inputEventArgs">The input event data to compare this gesture to.</param>
        /// <returns>
        /// true if the gesture matches the input; otherwise, false.
        /// </returns>
        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            return true;
        }
    }
}
