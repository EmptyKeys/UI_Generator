using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Content Presenter control generator
    /// </summary>
    public class ContentPresenterGeneratorType : ElementGeneratorType
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
                return typeof(ContentPresenter);
            }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="source">source</param>
        /// <returns></returns>
        public override IEnumerable GetChildren(DependencyObject source)
        {
            ContentPresenter presenter = source as ContentPresenter;
            DependencyObject child = presenter.Content as UIElement;
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
            initMethod.Statements.Insert(index, new CodeAssignStatement(new CodeFieldReferenceExpression(parent, "Content"), child));
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

            ContentPresenter presenter = source as ContentPresenter;

            UIElement content = presenter.Content as UIElement;
            if (content == null && presenter.Content != null)
            {
                CodeComHelper.GenerateField<object>(initMethod, fieldReference, source, ContentPresenter.ContentProperty);
                // TODO content can be another class, so this will not work
            }
                     
            CodeComHelper.GenerateField<string>(initMethod, fieldReference, presenter, ContentPresenter.ContentStringFormatProperty);
            CodeComHelper.GenerateTemplateStyleField(classType, initMethod, fieldReference, source, ContentPresenter.ContentTemplateProperty);

            return fieldReference;
        }
    }
}
