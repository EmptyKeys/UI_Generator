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
    /// Implements Grid control generator
    /// </summary>
    public class GridGeneratorType : PanelGeneratorType
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
                return typeof(Grid);
            }
        }

        /// <summary>
        /// Generates control fields
        /// </summary>
        /// <param name="source">The source</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="initMethod">The initialize method.</param>
        /// <param name="generateField"></param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod initMethod, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, initMethod, generateField);

            Grid grid = source as Grid;
            int rowIndex = 0;
            foreach (var row in grid.RowDefinitions)
            {
                string rowName = string.Format("row_{0}_{1}", grid.Name, rowIndex);
                CodeVariableDeclarationStatement rowVar = new CodeVariableDeclarationStatement(
                    "RowDefinition", rowName, new CodeObjectCreateExpression("RowDefinition"));
                initMethod.Statements.Add(rowVar);

                CodeVariableReferenceExpression rowRef = new CodeVariableReferenceExpression(rowName);

                CodeComHelper.GenerateGridLengthField(initMethod, rowRef, row, RowDefinition.HeightProperty);
                CodeComHelper.GenerateFieldDoubleToFloat(initMethod, rowRef, row, RowDefinition.MinHeightProperty);
                CodeComHelper.GenerateFieldDoubleToFloat(initMethod, rowRef, row, RowDefinition.MaxHeightProperty);

                CodeMethodInvokeExpression addRow = new CodeMethodInvokeExpression(fieldReference, "RowDefinitions.Add", rowRef);
                initMethod.Statements.Add(addRow);

                rowIndex++;
            }

            int columnIndex = 0;
            foreach (var col in grid.ColumnDefinitions)
            {
                string colName = string.Format("col_{0}_{1}", grid.Name, columnIndex);

                CodeVariableDeclarationStatement colVar = new CodeVariableDeclarationStatement(
                    "ColumnDefinition", colName, new CodeObjectCreateExpression("ColumnDefinition"));
                initMethod.Statements.Add(colVar);

                CodeVariableReferenceExpression colRef = new CodeVariableReferenceExpression(colName);

                CodeComHelper.GenerateGridLengthField(initMethod, colRef, col, ColumnDefinition.WidthProperty);
                CodeComHelper.GenerateFieldDoubleToFloat(initMethod, colRef, col, ColumnDefinition.MinWidthProperty);
                CodeComHelper.GenerateFieldDoubleToFloat(initMethod, colRef, col, ColumnDefinition.MaxWidthProperty);

                CodeMethodInvokeExpression addCol = new CodeMethodInvokeExpression(fieldReference, "ColumnDefinitions.Add", colRef);
                initMethod.Statements.Add(addCol);

                columnIndex++;
            }


            return fieldReference;
        }
    }
}
