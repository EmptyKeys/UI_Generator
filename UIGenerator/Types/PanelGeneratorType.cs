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
    /// Implements Panel control generator
    /// </summary>
    public class PanelGeneratorType : ElementGeneratorType
    {

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="source">The dependency object</param>
        /// <returns></returns>
        public override IEnumerable GetChildren(DependencyObject source)
        {
            Panel panel = source as Panel;
            if (!panel.IsItemsHost)
            {
                return panel.Children;
            }
            else
            {
                return null;
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

            Panel panel = source as Panel;
            CodeComHelper.GenerateBrushField(initMethod, fieldReference, source, Panel.BackgroundProperty);
            CodeComHelper.GenerateField<bool>(initMethod, fieldReference, source, Panel.IsItemsHostProperty);
            
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
            initMethod.Statements.Insert(index, new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeFieldReferenceExpression(parent, "Children"), "Add", child)));
        }
    }
}
