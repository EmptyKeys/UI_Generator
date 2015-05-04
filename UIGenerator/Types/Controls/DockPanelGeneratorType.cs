using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmptyKeys.UserInterface.Generator.Types.Controls
{
    /// <summary>
    /// Implements DockPanel control generator
    /// </summary>
    public class DockPanelGeneratorType : PanelGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get { return typeof(DockPanel); }
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
            DockPanel dockPanel = source as DockPanel;
            CodeComHelper.GenerateField<bool>(initMethod, fieldReference, source, DockPanel.LastChildFillProperty);
            return fieldReference;
        }        
    }
}
