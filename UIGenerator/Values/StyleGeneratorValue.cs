using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Generator.Values
{
    /// <summary>
    /// Implements generator for Style
    /// </summary>
    public class StyleGeneratorValue : IGeneratorValue
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
                return typeof(Style);
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
            return GetStyleValueExpression(parentClass, method, value, baseName, dictionary);
        }

        private static CodeExpression GetStyleValueExpression(CodeTypeDeclaration parentClass, CodeMemberMethod method, object value, string baseName, ResourceDictionary dictionary)
        {
            Style style = value as Style;
            string variableName = baseName + "_s";
            Type targetType = style.TargetType;
            CodeVariableDeclarationStatement styleVar = null;
            if (targetType == typeof(IFrameworkInputElement))
            {
                styleVar = new CodeVariableDeclarationStatement("Style", variableName, new CodeObjectCreateExpression("Style"));
                targetType = null;
            }
            else
            {
                if (style.BasedOn != null)
                {
                    if (dictionary != null)
                    {
                        object key = FindResourceKey(dictionary, style.BasedOn);
                        if (key == null)
                        {
                            key = style.BasedOn.TargetType;
                        }
                        CodeExpression keyExpression = CodeComHelper.GetResourceKeyExpression(key);
                        string basedOnVarName = variableName + "_bo";

                        CodeArrayIndexerExpression initExpr = null;
                        if (method.Parameters.Count == 1 && method.Parameters[0].Name == "elem")
                        {
                            initExpr = new CodeArrayIndexerExpression(
                                new CodeFieldReferenceExpression(new CodeVariableReferenceExpression(method.Parameters[0].Name), "Resources"), keyExpression);
                        }
                        else
                        {
                            initExpr = new CodeArrayIndexerExpression(new CodeThisReferenceExpression(), keyExpression);
                        }

                        method.Statements.Add(new CodeVariableDeclarationStatement("var", basedOnVarName, initExpr));

                        styleVar =
                            new CodeVariableDeclarationStatement("Style", variableName,
                                new CodeObjectCreateExpression("Style", new CodeTypeOfExpression(targetType.Name), new CodeSnippetExpression(basedOnVarName + " as Style")));
                    }
                    else
                    {
                        CodeSnippetStatement warning = new CodeSnippetStatement("#warning Style BasedOn is supported only in Dictionary.");
                        method.Statements.Add(warning);
                    }
                }

                if (styleVar == null)
                {
                    styleVar = new CodeVariableDeclarationStatement("Style", variableName, new CodeObjectCreateExpression("Style", new CodeTypeOfExpression(targetType.Name)));
                }
            }

            method.Statements.Add(styleVar);

            int setterIndex = 0;
            foreach (var setterBase in style.Setters)
            {
                Setter setter = setterBase as Setter;
                if (setter != null)
                {
                    string setterVarName = variableName + "_S_" + setterIndex;

                    CodeComHelper.GenerateSetter(parentClass, method, targetType, setter, setterVarName);

                    CodeMethodInvokeExpression addSetter = new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression(variableName), "Setters.Add", new CodeVariableReferenceExpression(setterVarName));
                    method.Statements.Add(addSetter);

                    setterIndex++;
                }
            }

            CodeComHelper.GenerateTriggers(parentClass, method, variableName, targetType, style.Triggers);

            return new CodeVariableReferenceExpression(variableName);
        }

        private static object FindResourceKey(ResourceDictionary dictionary, object value)
        {
            object key = null;
            foreach (DictionaryEntry entry in dictionary)
            {
                if (entry.Value.Equals(value))
                {
                    key = entry.Key;
                    break;
                }
            }

            foreach (ResourceDictionary merge in dictionary.MergedDictionaries)
            {
                key = FindResourceKey(merge, value);
                if (key != null)
                {
                    break;
                }
            }

            return key;
        }
    }
}
