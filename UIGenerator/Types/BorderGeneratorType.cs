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

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Border generator
    /// </summary>
    public class BorderGeneratorType : ElementGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get { return typeof(Border); }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public override IEnumerable GetChildren(DependencyObject source)
        {
            Border border = source as Border;
            DependencyObject child = border.Child;
            return new List<DependencyObject> { child };
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

            Border border = source as Border;
            CodeComHelper.GenerateBrushField(initMethod, fieldReference, source, Border.BackgroundProperty);
            CodeComHelper.GenerateBrushField(initMethod, fieldReference, source, Border.BorderBrushProperty);
            CodeComHelper.GenerateThicknessField(initMethod, fieldReference, source, Border.BorderThicknessProperty);
            CodeComHelper.GenerateThicknessField(initMethod, fieldReference, source, Border.PaddingProperty);

            return fieldReference;
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
            initMethod.Statements.Insert(index, new CodeAssignStatement(new CodeFieldReferenceExpression(parent, "Child"), child));
        }
    }
}
