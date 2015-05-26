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
    /// Implements Headered Items Control generator
    /// </summary>
    public class HeaderedItemsControlGeneratorType : ItemsControlGeneratorType
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
                return typeof(HeaderedItemsControl);
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

            HeaderedItemsControl control = source as HeaderedItemsControl;

            UIElement header = control.Header as UIElement;
            if (header != null)
            {
                TypeGenerator headerGenerator = new TypeGenerator();
                CodeExpression headerExpr = headerGenerator.ProcessGenerators(header, classType, initMethod, false);

                initMethod.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(fieldReference, "Header"), headerExpr));
            }            
            else if (control.Header != null)
            {
                CodeComHelper.GenerateField<object>(initMethod, fieldReference, source, HeaderedContentControl.HeaderProperty);
                // TODO content can be another class, so this will not work
            }

            return fieldReference;
        }
    }
}
