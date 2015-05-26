using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Generator.Types.Controls
{
    /// <summary>
    /// Implements Tree View Item generator
    /// </summary>
    public class TreeViewItemGeneratorType : HeaderedItemsControlGeneratorType
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
                return typeof(TreeViewItem);
            }
        }

        /// <summary>
        /// Generates code
        /// </summary>
        /// <param name="source">The dependence object</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The initialize method.</param>
        /// <param name="generateField"></param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, method, generateField);

            TreeViewItem item = source as TreeViewItem;
            CodeComHelper.GenerateField<bool>(method, fieldReference, item, TreeViewItem.IsSelectedProperty);

            return fieldReference;
        }
    }
}
