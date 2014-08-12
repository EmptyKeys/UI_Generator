using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Items Control generator
    /// </summary>
    public class ItemsControlGeneratorType : ControlGeneratorType
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
                return typeof(ItemsControl);
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

            ItemsControl itemsControl = source as ItemsControl;
            CodeComHelper.GenerateTemplateStyleField(classType, method, fieldReference, source, ItemsControl.ItemsPanelProperty);
            CodeComHelper.GenerateTemplateStyleField(classType, method, fieldReference, source, ItemsControl.ItemTemplateProperty);

            if (itemsControl.Items.Count > 0)
            {
                TypeGenerator itemGenerator = new TypeGenerator();

                CodeMemberMethod itemsMethod = new CodeMemberMethod();
                itemsMethod.Name = "Get_" + itemsControl.Name + "_Items";
                itemsMethod.ReturnType = new CodeTypeReference(typeof(ObservableCollection<object>));
                classType.Members.Add(itemsMethod);

                CodeVariableDeclarationStatement collection = new CodeVariableDeclarationStatement(
                    typeof(ObservableCollection<object>), "items", new CodeObjectCreateExpression(typeof(ObservableCollection<object>)));
                itemsMethod.Statements.Add(collection);
                CodeVariableReferenceExpression itemsVar = new CodeVariableReferenceExpression("items");
                foreach (var item in itemsControl.Items)
                {
                    CodeExpression itemExpr = itemGenerator.ProcessGenerators(item, classType, itemsMethod, false);

                    if (itemExpr != null)
                    {
                        CodeMethodInvokeExpression addItem = new CodeMethodInvokeExpression(itemsVar, "Add", itemExpr);
                        itemsMethod.Statements.Add(addItem);
                    }
                }

                CodeMethodReturnStatement returnStatement = new CodeMethodReturnStatement(itemsVar);
                itemsMethod.Statements.Add(returnStatement);

                method.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(fieldReference, "ItemsSource"),
                    new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), itemsMethod.Name)));
            }

            return fieldReference;
        }
    }
}
