using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Generator.Types.Controls
{
    /// <summary>
    /// Implements generator for DataGridColumn type
    /// </summary>
    public class DataGridColumnGeneratorType : IGeneratorType
    {
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        public static string ColumnName { get; set; }

        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public virtual Type XamlType
        {
            get
            {
                return typeof(DataGridColumn);
            }
        }

        /// <summary>
        /// Generates control fields
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public virtual CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            DataGridColumn column = source as DataGridColumn;
            string typeName = source.GetType().Name;
            string name = ColumnName;

            CodeExpression fieldReference = new CodeVariableReferenceExpression(name);
            CodeTypeReference variableType = new CodeTypeReference(typeName);
            CodeVariableDeclarationStatement declaration = new CodeVariableDeclarationStatement(variableType, name);
            declaration.InitExpression = new CodeObjectCreateExpression(typeName);
            method.Statements.Add(declaration);
            
            if (CodeComHelper.IsValidForFieldGenerator(source.ReadLocalValue(DataGridColumn.WidthProperty)))
            {
                DataGridLength value = (DataGridLength)source.GetValue(DataGridColumn.WidthProperty);
                CodeTypeReferenceExpression dglType = new CodeTypeReferenceExpression("DataGridLength");

                switch (value.UnitType)
                {
                    case DataGridLengthUnitType.Auto:
                        // AUTO is default value so we don't need to generate any code
                        break;
                    case DataGridLengthUnitType.Pixel:
                        method.Statements.Add(new CodeAssignStatement(
                            new CodeFieldReferenceExpression(fieldReference, DataGridColumn.WidthProperty.Name),
                            new CodePrimitiveExpression((float)value.Value)));
                        break;
                    case DataGridLengthUnitType.SizeToCells:
                        method.Statements.Add(new CodeAssignStatement(
                            new CodeFieldReferenceExpression(fieldReference, DataGridColumn.WidthProperty.Name),                            
                            new CodeFieldReferenceExpression(dglType, "SizeToCells")));
                        break;
                    case DataGridLengthUnitType.SizeToHeader:
                        method.Statements.Add(new CodeAssignStatement(
                            new CodeFieldReferenceExpression(fieldReference, DataGridColumn.WidthProperty.Name),
                            new CodeFieldReferenceExpression(dglType, "SizeToHeader")));
                        break;
                    case DataGridLengthUnitType.Star:
                        method.Statements.Add(new CodeAssignStatement(
                            new CodeFieldReferenceExpression(fieldReference, DataGridColumn.WidthProperty.Name),
                            new CodeObjectCreateExpression("DataGridLength", 
                                new CodePrimitiveExpression(Convert.ToSingle(value.Value)),
                                new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("DataGridLengthUnitType"), "Star"))));
                        break;
                    default:
                        break;
                }                
            }            
            
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, DataGridColumn.MaxWidthProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, DataGridColumn.MinWidthProperty);
            CodeComHelper.GenerateEnumField<Visibility>(method, fieldReference, source, DataGridColumn.VisibilityProperty);
            CodeComHelper.GenerateField<string>(method, fieldReference, source, DataGridColumn.SortMemberPathProperty);

            UIElement header = column.Header as UIElement;
            if (header != null)
            {
                TypeGenerator headerGenerator = new TypeGenerator();
                CodeExpression headerExpr = headerGenerator.ProcessGenerators(header, classType, method, false);

                method.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(fieldReference, "Header"), headerExpr));
            }
            else if (column.Header != null)
            {
                CodeComHelper.GenerateField<object>(method, fieldReference, source, DataGridColumn.HeaderProperty);
                // TODO content can be another class, so this will not work
            }

            CodeComHelper.GenerateTemplateStyleField(classType, method, fieldReference, source, DataGridColumn.HeaderStyleProperty, name + "_h");

            return fieldReference;
        }


        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="source">The dependency object</param>
        /// <returns></returns>
        public IEnumerable GetChildren(DependencyObject source)
        {
            return null;
        }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        /// <param name="method">The initialize method.</param>
        /// <param name="index">The index.</param>
        public void AddChild(CodeExpression parent, CodeExpression child, CodeMemberMethod method, int index)
        {            
        }
    }
}
