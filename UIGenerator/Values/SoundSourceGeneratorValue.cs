using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EmptyKeys.UserInterface.Designer;

namespace EmptyKeys.UserInterface.Generator.Values
{
    /// <summary>
    /// Implements generator for Sound Source Collection value
    /// </summary>
    public class SoundSourceGeneratorValue : IGeneratorValue
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
                return typeof(SoundSourceCollection);
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
            return GetSoundSourceCollectionValueExpression(method, value, baseName);  
        }

        private static CodeExpression GetSoundSourceCollectionValueExpression(CodeMemberMethod method, object value, string baseName)
        {
            string collVar = baseName + "_sounds";
            CodeVariableDeclarationStatement collection =
                    new CodeVariableDeclarationStatement("var", collVar, new CodeObjectCreateExpression("SoundSourceCollection"));
            method.Statements.Add(collection);
            CodeComHelper.GenerateSoundSources(method, value as SoundSourceCollection, collVar);
            return new CodeVariableReferenceExpression(collVar);
        }         
    }
}
