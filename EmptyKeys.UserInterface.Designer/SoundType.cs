using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Sound Types for UI
    /// </summary>
    public enum SoundType
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// The buttons click
        /// </summary>
        ButtonsClick,

        /// <summary>
        /// The buttons hover sound
        /// </summary>
        ButtonsHover,

        /// <summary>
        /// The ListBox move
        /// </summary>
        ListBoxMove,

        /// <summary>
        /// The ListBox select
        /// </summary>
        ListBoxSelect,

        /// <summary>
        /// The text box key press
        /// </summary>
        TextBoxKeyPress,

        /// <summary>
        /// The tab control move
        /// </summary>
        TabControlMove,

        /// <summary>
        /// The tab control select
        /// </summary>
        TabControlSelect,

        /// <summary>
        /// The CheckBox hover
        /// </summary>
        CheckBoxHover,

        /// <summary>
        /// The RadioButton hover
        /// </summary>
        RadioButtonHover,

        /// <summary>
        /// Sound type when focus is changed
        /// </summary>
        FocusChanged
    }
}
