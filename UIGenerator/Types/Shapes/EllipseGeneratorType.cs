using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace EmptyKeys.UserInterface.Generator.Types.Shapes
{
    /// <summary>
    /// Implements Ellipse shape type generator
    /// </summary>
    public class EllipseGeneratorType : ShapeGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get { return typeof(Ellipse); }
        }

        /// <summary>
        /// Generates control fields
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The initialize method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, method, generateField);

            Ellipse ellipse = source as Ellipse;                       

            return fieldReference;
        }
    }
}
