using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmptyKeys.UserInterface.Designer.Input
{
    /// <summary>
    /// Implements binding for Game Pad input and Input Gesture
    /// </summary>
    public class GamepadBinding : InputBinding
    {
        /// <summary>
        /// The input property
        /// </summary>
        public static readonly DependencyProperty GamepadInputProperty =
            DependencyProperty.Register("GamepadInput", typeof(GamepadInput), typeof(GamepadBinding),
            new FrameworkPropertyMetadata(GamepadInput.None, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnInputChanged)));

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        public GamepadInput GamepadInput
        {
            get { return (GamepadInput)GetValue(GamepadInputProperty); }
            set { SetValue(GamepadInputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GamepadBinding binding = d as GamepadBinding;
            binding.Gesture = new GamepadGesture((GamepadInput) e.NewValue);
        }

        /// <summary>
        /// Gets or sets the gesture.
        /// </summary>
        /// <value>
        /// The gesture.
        /// </value>
        /// <exception cref="System.ArgumentException">Wrong Input Gesture type. Expected GamepadGesture.</exception>
        public override InputGesture Gesture
        {
            get
            {
                return base.Gesture as GamepadGesture;
            }
            set
            {
                GamepadGesture gpGesture = value as GamepadGesture;
                if (gpGesture != null)
                {
                    base.Gesture = value;                    
                }
                else
                {
                    throw new ArgumentException("Wrong Input Gesture type. Expected GamepadGesture.");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GamepadBinding"/> class.
        /// </summary>
        public GamepadBinding()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GamepadBinding"/> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="gesture">The gesture.</param>
        public GamepadBinding(ICommand command, GamepadGesture gesture)
            : base(command, gesture)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GamepadBinding"/> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="input">The input.</param>
        public GamepadBinding(ICommand command, GamepadInput input)
            : this(command, new GamepadGesture(input))
        {
        }        
    }
}
