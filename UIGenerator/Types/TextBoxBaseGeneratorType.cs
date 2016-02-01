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
    /// Implements Text Box base generator
    /// </summary>
    public class TextBoxBaseGeneratorType : ControlGeneratorType
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
                return typeof(TextBoxBase);
            }
        }

        /// <summary>
        /// Generates control fields
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="initMethod">The initialize method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod initMethod, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, initMethod, generateField);

            TextBoxBase textBoxBase = source as TextBoxBase;            
            CodeComHelper.GenerateField<bool>(initMethod, fieldReference, source, TextBoxBase.IsReadOnlyProperty);
            CodeComHelper.GenerateEnumField<ScrollBarVisibility>(initMethod, fieldReference, source, TextBoxBase.HorizontalScrollBarVisibilityProperty);
            CodeComHelper.GenerateEnumField<ScrollBarVisibility>(initMethod, fieldReference, source, TextBoxBase.VerticalScrollBarVisibilityProperty);
            CodeComHelper.GenerateBrushField(initMethod, fieldReference, source, TextBoxBase.CaretBrushProperty);
            CodeComHelper.GenerateBrushField(initMethod, fieldReference, source, TextBoxBase.SelectionBrushProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(initMethod, fieldReference, source, TextBoxBase.SelectionOpacityProperty);
            CodeComHelper.GenerateField<bool>(initMethod, fieldReference, source, TextBoxBase.IsUndoEnabledProperty);
            CodeComHelper.GenerateField<int>(initMethod, fieldReference, source, TextBoxBase.UndoLimitProperty);

            return fieldReference;
        }
    }
}
