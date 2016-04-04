using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Generator
{
    /// <summary>
    /// Describes Generator Type interface
    /// </summary>
    public interface IGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        Type XamlType
        {
            get;
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="source">The dependency object</param>
        /// <returns></returns>
        IEnumerable GetChildren(DependencyObject source);

        /// <summary>
        /// Generates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField);

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        /// <param name="method">The initialize method.</param>
        /// <param name="index">The index.</param>
        void AddChild(CodeExpression parent, CodeExpression child, CodeMemberMethod method, int index);
    }
}
