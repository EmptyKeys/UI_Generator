using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using EmptyKeys.UserInterface.Generator.Types.Controls;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Data Grid generator
    /// </summary>
    public class DataGridGeneratorType : SelectorGeneratorType
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
                return typeof(DataGrid);
            }
        }

        /// <summary>
        /// Generates code
        /// </summary>
        /// <param name="source">The dependence object</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="initMethod">The initialize method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod initMethod, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, initMethod, generateField);

            DataGrid grid = source as DataGrid;
            CodeComHelper.GenerateEnumField<DataGridSelectionMode>(initMethod, fieldReference, source, DataGrid.SelectionModeProperty);
            CodeComHelper.GenerateField<bool>(initMethod, fieldReference, source, DataGrid.AutoGenerateColumnsProperty);
            CodeComHelper.GenerateField<bool>(initMethod, fieldReference, source, DataGrid.EnableRowVirtualizationProperty);
            CodeComHelper.GenerateEnumField<ScrollBarVisibility>(initMethod, fieldReference, source, DataGrid.HorizontalScrollBarVisibilityProperty);
            CodeComHelper.GenerateEnumField<ScrollBarVisibility>(initMethod, fieldReference, source, DataGrid.VerticalScrollBarVisibilityProperty);

            if (grid.Columns.Count > 0)
            {
                TypeGenerator colGenerator = new TypeGenerator();
                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    var column = grid.Columns[i];
                    DataGridColumnGeneratorType.ColumnName = grid.Name + "_Col" + i;
                    CodeExpression expr = colGenerator.ProcessGenerators(column, classType, initMethod, false);
                    if (expr == null)
                    {
                        continue;
                    }

                    initMethod.Statements.Add(new CodeMethodInvokeExpression(fieldReference, "Columns.Add", expr));
                }                
            }

            return fieldReference;
        }
    }
}
