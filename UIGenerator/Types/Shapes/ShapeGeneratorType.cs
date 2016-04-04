using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EmptyKeys.UserInterface.Generator.Types.Shapes
{
    /// <summary>
    /// Implements Shape type generator
    /// </summary>
    public class ShapeGeneratorType : ElementGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get { return typeof(Shape); }
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

            Shape shape = source as Shape;

            CodeComHelper.GenerateEnumField<Stretch>(method, fieldReference, source, Shape.StretchProperty);
            CodeComHelper.GenerateBrushField(method, fieldReference, source, Shape.FillProperty);
            CodeComHelper.GenerateBrushField(method, fieldReference, source, Shape.StrokeProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, Shape.StrokeThicknessProperty);

            return fieldReference;
        }
    }
}
