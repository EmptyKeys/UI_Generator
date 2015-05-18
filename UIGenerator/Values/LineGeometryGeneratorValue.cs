using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EmptyKeys.UserInterface.Generator.Values
{
    /// <summary>
    /// Implements value generator for Line Geometry
    /// </summary>
    public class LineGeometryGeneratorValue : IGeneratorValue
    {
        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public Type ValueType
        {
            get
            {
                return typeof(LineGeometry);
            }
        }

        /// <summary>
        /// Generates code for value
        /// </summary>
        /// <param name="parentClass">The parent class.</param>
        /// <param name="method">The method.</param>
        /// <param name="value">The value.</param>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns></returns>
        public CodeExpression Generate(CodeTypeDeclaration parentClass, CodeMemberMethod method, object value, string baseName, ResourceDictionary dictionary = null)
        {
            CodeExpression valueExpression = null;
            if (value != null)
            {
                var geometryVariable = new CodeVariableDeclarationStatement("LineGeometry", baseName, new CodeObjectCreateExpression("LineGeometry"));
                method.Statements.Add(geometryVariable);
                valueExpression = new CodeVariableReferenceExpression(baseName);

                LineGeometry line = value as LineGeometry;
                CodeComHelper.GeneratePointField(method, valueExpression, line, LineGeometry.StartPointProperty);
                CodeComHelper.GeneratePointField(method, valueExpression, line, LineGeometry.EndPointProperty);
            }

            return valueExpression;
        }
    }
}
