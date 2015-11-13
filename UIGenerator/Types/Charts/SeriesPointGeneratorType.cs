using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EmptyKeys.UserInterface.Designer.Charts;

namespace EmptyKeys.UserInterface.Generator.Types.Charts
{
    /// <summary>
    /// Implements Series Point generator
    /// </summary>
    public class SeriesPointGeneratorType : IGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public virtual Type XamlType
        {
            get { return typeof(SeriesPoint); }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="source">The dependency object</param>
        /// <returns></returns>
        public virtual IEnumerable GetChildren(DependencyObject source)
        {
            return null;
        }

        /// <summary>
        /// Generates control fields
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The initialize method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public virtual CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            SeriesPoint point = source as SeriesPoint;
            string typeName = point.GetType().Name;
            string name = "p_" + ElementGeneratorType.NameUniqueId;
            ElementGeneratorType.NameUniqueId++;
            
            /*
            if (generateField)
            {
                CodeMemberField field = new CodeMemberField(typeName, name);
                classType.Members.Add(field);
            }
             */ 

            CodeComment comment = new CodeComment(name + " point");
            method.Statements.Add(new CodeCommentStatement(comment));

            CodeExpression fieldReference = null;

            //if (!generateField)
            {
                fieldReference = new CodeVariableReferenceExpression(name);
                CodeTypeReference variableType = new CodeTypeReference(typeName);
                CodeVariableDeclarationStatement declaration = new CodeVariableDeclarationStatement(variableType, name);
                declaration.InitExpression = new CodeObjectCreateExpression(typeName);
                method.Statements.Add(declaration);
            }
            /*
            else
            {
                fieldReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), name);
                method.Statements.Add(new CodeAssignStatement(fieldReference, new CodeObjectCreateExpression(typeName)));
            }
            */

            CodeComHelper.GenerateField<float>(method, fieldReference, source, SeriesPoint.ArgumentProperty);
            CodeComHelper.GenerateField<float>(method, fieldReference, source, SeriesPoint.ValueProperty);

            return fieldReference;
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
