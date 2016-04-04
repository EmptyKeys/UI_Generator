using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Scroll Viewer control generator
    /// </summary>
    public class ScrollViewerGeneratorType : ContentControlGeneratorType
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
                return typeof(ScrollViewer);
            }
        }

        /// <summary>
        /// Generates code
        /// </summary>
        /// <param name="source">The dependence object</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="initMethod">The initialize method.</param>
        /// <param name="generateField"></param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod initMethod, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, initMethod, generateField);

            ScrollViewer scrollViewer = source as ScrollViewer;
            CodeComHelper.GenerateEnumField<ScrollBarVisibility>(initMethod, fieldReference, source, ScrollViewer.HorizontalScrollBarVisibilityProperty);
            CodeComHelper.GenerateEnumField<ScrollBarVisibility>(initMethod, fieldReference, source, ScrollViewer.VerticalScrollBarVisibilityProperty);
            CodeComHelper.GenerateEnumField<PanningMode>(initMethod, fieldReference, source, ScrollViewer.PanningModeProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(initMethod, fieldReference, source, ScrollViewer.PanningRatioProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(initMethod, fieldReference, source, ScrollViewer.PanningDecelerationProperty);

            return fieldReference;
        }
    }
}