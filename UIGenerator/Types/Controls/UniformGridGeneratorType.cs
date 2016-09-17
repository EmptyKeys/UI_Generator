using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace EmptyKeys.UserInterface.Generator.Types.Controls
{
    /// <summary>
    /// Implements generator for Uniform Grid control
    /// </summary>
    /// <seealso cref="EmptyKeys.UserInterface.Generator.Types.PanelGeneratorType" />
    public class UniformGridGeneratorType : PanelGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get { return typeof(UniformGrid); }
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

            UniformGrid uniGrid = source as UniformGrid;                        
            CodeComHelper.GenerateField<int>(initMethod, fieldReference, source, UniformGrid.FirstColumnProperty);
            CodeComHelper.GenerateField<int>(initMethod, fieldReference, source, UniformGrid.RowsProperty);
            CodeComHelper.GenerateField<int>(initMethod, fieldReference, source, UniformGrid.ColumnsProperty);

            return fieldReference;
        }
    }
}
