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
        /// <param name="generateField"></param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod initMethod, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, initMethod, generateField);

            DataGrid grid = source as DataGrid;
            CodeComHelper.GenerateEnumField<DataGridSelectionMode>(initMethod, fieldReference, source, DataGrid.SelectionModeProperty);
            CodeComHelper.GenerateField<bool>(initMethod, fieldReference, source, DataGrid.AutoGenerateColumnsProperty);
            CodeComHelper.GenerateEnumField<ScrollBarVisibility>(initMethod, fieldReference, source, DataGrid.HorizontalScrollBarVisibilityProperty);
            CodeComHelper.GenerateEnumField<ScrollBarVisibility>(initMethod, fieldReference, source, DataGrid.VerticalScrollBarVisibilityProperty);

            return fieldReference;
        }
    }
}
