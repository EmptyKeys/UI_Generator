using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EmptyKeys.UserInterface.Designer.Charts;

namespace EmptyKeys.UserInterface.Generator.Types.Charts
{
    /// <summary>
    /// Implements Chart control generator
    /// </summary>
    public class ChartGeneratorType : ControlGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get
            {
                return typeof(Chart);
            }
        }

        /// <summary>
        /// Generates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, method, generateField);

            Chart chart = source as Chart;
            CodeComHelper.GenerateField<float>(method, fieldReference, source, Chart.TickmarkLengthProperty);
            CodeComHelper.GenerateField<float>(method, fieldReference, source, Chart.TickmarkThicknessProperty);
            CodeComHelper.GenerateField<float>(method, fieldReference, source, Chart.AxisThicknessProperty);
            CodeComHelper.GenerateBrushField(method, fieldReference, source, Chart.AxisBrushProperty);
            CodeComHelper.GenerateField<bool>(method, fieldReference, source, Chart.AxisVisibleProperty);
            CodeComHelper.GenerateField<float>(method, fieldReference, source, Chart.AxisYMajorUnitProperty);
            CodeComHelper.GenerateField<float>(method, fieldReference, source, Chart.AxisXMajorUnitProperty);
            CodeComHelper.GenerateField<bool>(method, fieldReference, source, Chart.CursorLinesEnabledProperty);
            CodeComHelper.GenerateBrushField(method, fieldReference, source, Chart.CursorLinesBrushProperty);
            CodeComHelper.GenerateField<float>(method, fieldReference, source, Chart.AxisLabelMarginProperty);
            CodeComHelper.GenerateField<bool>(method, fieldReference, source, Chart.AxisLabelsVisibleProperty);
            CodeComHelper.GenerateField<bool>(method, fieldReference, source, Chart.AxisInterlacedProperty);
            CodeComHelper.GenerateBrushField(method, fieldReference, source, Chart.AxisInterlacedBrushProperty);
            CodeComHelper.GenerateField<string>(method, fieldReference, source, Chart.AxisLabelFormatProperty);
            
            return fieldReference;
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="source">The dependency object</param>
        /// <returns></returns>
        public override IEnumerable GetChildren(DependencyObject source)
        {
            Chart chart = source as Chart;
            return chart.Series;
        }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        /// <param name="initMethod">The initialize method.</param>
        /// <param name="index">The index.</param>
        public override void AddChild(CodeExpression parent, CodeExpression child, CodeMemberMethod initMethod, int index)
        {
            initMethod.Statements.Insert(index, new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeFieldReferenceExpression(parent, "Series"), "Add", child)));
        }
    }
}
