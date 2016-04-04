using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace EmptyKeys.UserInterface.Generator.Types.Controls.Primitives
{
    /// <summary>
    /// Implements Popup generator
    /// </summary>
    public class PopupGeneratorType : ElementGeneratorType
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
                return typeof(Popup);
            }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="source">source</param>
        /// <returns></returns>
        public override IEnumerable GetChildren(DependencyObject source)
        {
            Popup popup = source as Popup;
            DependencyObject child = popup.Child as UIElement;
            if (child == null)
            {
                return null;
            }

            return new List<DependencyObject> { child };
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

            Popup popup = source as Popup;

            CodeComHelper.GenerateField<bool>(method, fieldReference, source, Popup.IsOpenProperty);
            CodeComHelper.GenerateEnumField<PlacementMode>(method, fieldReference, source, Popup.PlacementProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, Popup.VerticalOffsetProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, Popup.HorizontalOffsetProperty);

            UIElement content = popup.Child as UIElement;
            if (content == null && popup.Child != null)
            {
                CodeComHelper.GenerateField<object>(method, fieldReference, source, Popup.ChildProperty);                
            }

            return fieldReference;
        }
    }
}
