using EmptyKeys.UserInterface.Designer;
using System;
using System.CodeDom;
using System.Windows;

namespace EmptyKeys.UserInterface.Generator.Types.Controls
{
    /// <summary>
    /// Implements generator for Radial Panel
    /// </summary>
    /// <seealso cref="EmptyKeys.UserInterface.Generator.Types.PanelGeneratorType" />
    public class RadialPanelGeneratorType : PanelGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get { return typeof(RadialPanel); }
        }

        /// <summary>
        /// Generates the specified initialize method.
        /// </summary>
        /// <param name="source">The dependency object</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="initMethod">The initialize method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod initMethod, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, initMethod, generateField);

            return fieldReference;
        }
    }
}
