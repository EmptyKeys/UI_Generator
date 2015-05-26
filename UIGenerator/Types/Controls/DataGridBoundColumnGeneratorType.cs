using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EmptyKeys.UserInterface.Generator.Types.Controls
{
    /// <summary>
    /// Implements generator for Data Grind bound column
    /// </summary>
    public class DataGridBoundColumnGeneratorType : DataGridColumnGeneratorType
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
                return typeof(DataGridBoundColumn);
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

            DataGridBoundColumn column = source as DataGridBoundColumn;
            CodeComHelper.GenerateTemplateStyleField(classType, initMethod, fieldReference, source, DataGridBoundColumn.ElementStyleProperty, ColumnName + "_e");

            Binding commandBindingExpr = column.Binding as Binding;
            if (commandBindingExpr != null)
            {
                CodeVariableReferenceExpression bindingVar = CodeComHelper.GenerateBinding(initMethod, commandBindingExpr, ColumnName + "_b");

                var statement = new CodeAssignStatement(new CodeFieldReferenceExpression(fieldReference, "Binding"), bindingVar);
                initMethod.Statements.Add(statement);
            }

            return fieldReference;
        }
    }
}
