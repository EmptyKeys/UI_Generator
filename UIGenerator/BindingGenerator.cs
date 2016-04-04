using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using EmptyKeys.UserInterface.Designer;

namespace EmptyKeys.UserInterface.Generator
{
    /// <summary>
    /// Implements binding generator
    /// </summary>
    public sealed class BindingGenerator
    {
        private static readonly char[] partSeparator = new char[] { '.' };
        private static readonly char[] indexStartChars = new char[] { '[' };
        private static readonly char[] indexEndChars = new char[] { ']' };
        private static readonly char[] indexSeparator = new char[] { ',' };
        private static readonly char[] paramStartChars = new char[] { '(' };
        private static readonly char[] paramEndChars = new char[] { ')' };

        private static BindingGenerator singleton = new BindingGenerator();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static BindingGenerator Instance
        {
            get
            {
                return singleton;
            }
        }

        private readonly string memoryMappedFileName = "GenerationCodeMapppedFile_Bindings";
        private readonly long nemoryMappedFileCapacity = 1000000;        

        private CodeNamespace ns;
        private List<Tuple<Type, string, string>> propertyInfos;
        private SortedSet<int> generated;

        /// <summary>
        /// Gets or sets the type of the active data.
        /// </summary>
        /// <value>
        /// The type of the active data.
        /// </value>
        public Type ActiveDataType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="BindingGenerator"/> class from being created.
        /// </summary>
        private BindingGenerator()
        {
        }

        /// <summary>
        /// Generates the namespace.
        /// </summary>
        /// <param name="desiredNamespace">The desired namespace.</param>
        public void GenerateNamespace(string desiredNamespace)
        {
            propertyInfos = new List<Tuple<Type, string, string>>();
            generated = new SortedSet<int>();

            ns = new CodeNamespace(desiredNamespace);
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.CodeDom.Compiler"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections.ObjectModel"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Charts"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Data"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Controls"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Controls.Primitives"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Input"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Interactions.Core"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Interactivity"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Media"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Media.Animation"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Media.Imaging"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Shapes"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Renderers"));
            ns.Imports.Add(new CodeNamespaceImport("EmptyKeys.UserInterface.Themes"));

            ns.Comments.Add(new CodeCommentStatement("-----------------------------------------------------------", false));
            ns.Comments.Add(new CodeCommentStatement(" ", false));
            ns.Comments.Add(new CodeCommentStatement(" This file was generated, please do not modify.", false));
            ns.Comments.Add(new CodeCommentStatement(" ", false));
            ns.Comments.Add(new CodeCommentStatement("-----------------------------------------------------------", false));
        }

        /// <summary>
        /// Generates the registration code.
        /// </summary>
        /// <param name="method">The method.</param>
        public void GenerateRegistrationCode(CodeMemberMethod method)
        {
            // GeneratedPropertyInfo.RegisterGeneratedProperty(Type dataType, string propertyName, Type propertyInfoType)
            // GeneratedPropertyInfo.RegisterGeneratedProperty(typeof(BindPerfData), "ColorA", typeof(BindPerfData_ColorA_Binding));

            foreach (var info in propertyInfos)
            {
                method.Statements.Add(new CodeMethodInvokeExpression(
                    new CodeTypeReferenceExpression("GeneratedPropertyInfo"), "RegisterGeneratedProperty",
                        new CodeTypeOfExpression(info.Item1.FullName), new CodePrimitiveExpression(info.Item2), new CodeTypeOfExpression(info.Item3)));
            }

            propertyInfos.Clear();
        }

        /// <summary>
        /// Generates the binding path.
        /// </summary>
        /// <param name="propertyPath">The property path.</param>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Namespace is empty.</exception>
        public bool GenerateBindingPath(PropertyPath propertyPath, GeneratedBindingsMode mode)
        {
            if (ActiveDataType == null || !IsEnabled)
            {
                return false;
            }

            if (ns == null)
            {
                throw new ArgumentNullException("Namespace is empty.");
            }

            Type dataType = ActiveDataType;
            if (dataType.IsPrimitive)
            {
                return false;
            }

            string path = propertyPath.Path;
            string[] pathParts = path.Split(partSeparator, StringSplitOptions.RemoveEmptyEntries);
            bool isCollectionProperty = false;
            for (int i = 0; i < pathParts.Length; i++)
            {
                string propertyName = pathParts[i];
                int indexStart = propertyName.IndexOfAny(indexStartChars);
                if (indexStart != -1)
                {
                    propertyName = propertyName.Substring(0, indexStart);
                }

                PropertyInfo propertyInfo = dataType.GetRuntimeProperty(propertyName);
                int paramStart = propertyName.IndexOfAny(paramStartChars);
                if (paramStart != -1)
                {
                    int paramEnd = propertyName.IndexOfAny(paramEndChars);
                    string paramIndexString = propertyName.Substring(paramStart + 1, paramEnd - paramStart - 1).Trim();
                    int paramIndex = Convert.ToInt32(paramIndexString);
                    propertyInfo = propertyPath.PathParameters[paramIndex] as PropertyInfo;
                    if (propertyInfo != null)
                    {
                        propertyName = propertyInfo.Name;
                        dataType = propertyInfo.ReflectedType;
                    }
                }

                string className = string.Format("{0}_{1}_PropertyInfo", dataType.Name, propertyName);
                className = Regex.Replace(className, "[`|<|>]", "");                
                CodeTypeDeclaration classType = CreateClass(className);
                
                if (propertyInfo == null)
                {
                    // if mixed mode return with false (binding was not generated) but don't generate error so reflection can be used
                    if (mode == GeneratedBindingsMode.Mixed)
                    {
                        return false;
                    }

                    CodeSnippetTypeMember error = new CodeSnippetTypeMember(string.Format("#error {0} type does not have property {1}", dataType, propertyName));
                    classType.Members.Add(error);
                    ns.Types.Add(classType);
                    return false;
                }

                isCollectionProperty = propertyInfo.PropertyType.IsArray ||
                        (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType) && propertyInfo.PropertyType != typeof(string));
                Type elementType = propertyInfo.PropertyType.GetElementType();
                if (elementType == null && propertyInfo.PropertyType.IsGenericType)
                {
                    elementType = propertyInfo.PropertyType.GetGenericArguments()[0];
                }

                int key = propertyName.GetHashCode() ^ dataType.GetHashCode();
                if (!generated.Contains(key))
                {
                    generated.Add(key);                                       
                    ns.Types.Add(classType);
                    GenerateProperties(classType, propertyInfo);
                    GenerateMethods(dataType, isCollectionProperty, propertyName, classType, propertyInfo, elementType);
                    propertyInfos.Add(new Tuple<Type, string, string>(dataType, propertyName, ns.Name + "." + className));
                }

                if (isCollectionProperty)
                {
                    dataType = elementType;
                }
                else
                {
                    dataType = propertyInfo.PropertyType;
                }
            }

            return true;
        }

        private static void GenerateMethods(Type dataType, bool isCollectionProperty, string propertyName, CodeTypeDeclaration classType, PropertyInfo propertyInfo, Type elementType)
        {
            // public object GetValue(object obj) method
            CodeMemberMethod getValue1 = new CodeMemberMethod();
            getValue1.Name = "GetValue";
            getValue1.ReturnType = new CodeTypeReference(typeof(object));
            getValue1.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            getValue1.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "obj"));
            CodeExpression nonArrayAccessor = null;
            nonArrayAccessor = new CodeFieldReferenceExpression(new CodeCastExpression(dataType, new CodeVariableReferenceExpression("obj")), propertyInfo.Name);
            getValue1.Statements.Add(new CodeMethodReturnStatement(nonArrayAccessor));
            classType.Members.Add(getValue1);

            // public object GetValue(object obj, object[] index) method
            CodeMemberMethod getValue2 = new CodeMemberMethod();
            getValue2.Name = "GetValue";
            getValue2.ReturnType = new CodeTypeReference(typeof(object));
            getValue2.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            getValue2.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "obj"));
            getValue2.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object[]), "index"));
            CodeExpression arrayAccessor = null;
            if (isCollectionProperty)
            {
                arrayAccessor = new CodeIndexerExpression(
                        new CodeFieldReferenceExpression(new CodeCastExpression(dataType, new CodeVariableReferenceExpression("obj")), propertyName),
                        new CodeCastExpression(typeof(int), new CodeIndexerExpression(new CodeVariableReferenceExpression("index"), new CodePrimitiveExpression(0)))
                        );
            }
            else
            {
                arrayAccessor = new CodePrimitiveExpression(null);
            }

            getValue2.Statements.Add(new CodeMethodReturnStatement(arrayAccessor));
            classType.Members.Add(getValue2);

            // public void SetValue(object obj, object value) method
            CodeMemberMethod setValue1 = new CodeMemberMethod();
            setValue1.Name = "SetValue";
            setValue1.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            setValue1.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "obj"));
            setValue1.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "value"));
            if (propertyInfo.CanWrite && propertyInfo.SetMethod.IsPublic)
            {
                setValue1.Statements.Add(new CodeAssignStatement(nonArrayAccessor, new CodeCastExpression(propertyInfo.PropertyType, new CodeVariableReferenceExpression("value"))));
            }

            classType.Members.Add(setValue1);

            // public void SetValue(object obj, object value, object[] index) method
            CodeMemberMethod setValue2 = new CodeMemberMethod();
            setValue2.Name = "SetValue";
            setValue2.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            setValue2.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "obj"));
            setValue2.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object), "value"));
            setValue2.Parameters.Add(new CodeParameterDeclarationExpression(typeof(object[]), "index"));            
            if (isCollectionProperty && propertyInfo.CanWrite && propertyInfo.SetMethod.IsPublic)
            {
                setValue2.Statements.Add(new CodeAssignStatement(arrayAccessor, new CodeCastExpression(elementType, new CodeVariableReferenceExpression("value"))));
            }

            classType.Members.Add(setValue2);            
        }

        private static void GenerateProperties(CodeTypeDeclaration classType, PropertyInfo propertyInfo)
        {
            // PropertyType property
            CodeMemberProperty typeProperty = new CodeMemberProperty();
            typeProperty.Name = "PropertyType";
            typeProperty.Type = new CodeTypeReference(typeof(Type));
            typeProperty.HasGet = true;
            typeProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            typeProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeTypeOfExpression(propertyInfo.PropertyType)));
            classType.Members.Add(typeProperty);

            // IsResolved property
            CodeMemberProperty isResolvedProperty = new CodeMemberProperty();
            isResolvedProperty.Name = "IsResolved";
            isResolvedProperty.Type = new CodeTypeReference(typeof(bool));
            isResolvedProperty.HasGet = true;
            isResolvedProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            isResolvedProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(true)));
            classType.Members.Add(isResolvedProperty);
        }

        private CodeTypeDeclaration CreateClass(string className)
        {
            CodeTypeDeclaration classType = new CodeTypeDeclaration(className);

            GeneratedCodeAttribute generatedCodeAttribute =
            new GeneratedCodeAttribute("Empty Keys UI Generator", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            CodeAttributeDeclaration codeAttrDecl =
                new CodeAttributeDeclaration(generatedCodeAttribute.GetType().Name,
                    new CodeAttributeArgument(
                        new CodePrimitiveExpression(generatedCodeAttribute.Tool)),
                    new CodeAttributeArgument(
                        new CodePrimitiveExpression(generatedCodeAttribute.Version)));
            classType.CustomAttributes.Add(codeAttrDecl);

            classType.BaseTypes.Add(new CodeTypeReference("IPropertyInfo"));

            return classType;
        }

        /// <summary>
        /// Generates the file.
        /// </summary>
        /// <param name="outputFile">The output file.</param>
        /// <exception cref="System.ArgumentNullException">Code not generated. Namespace is empty.</exception>
        public void GenerateFile(string outputFile)
        {
            if (ns == null)
            {
                throw new ArgumentNullException("Code not generated. Namespace is empty.");
            }

            string generatedCode = string.Empty;
            try
            {
                using (CodeDomProvider provider = new Microsoft.CSharp.CSharpCodeProvider())
                {
                    using (MemoryMappedFile mappedFile = MemoryMappedFile.CreateNew(memoryMappedFileName, nemoryMappedFileCapacity))
                    {
                        MemoryMappedViewStream stream = mappedFile.CreateViewStream();

                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            provider.GenerateCodeFromNamespace(ns, sw, new CodeGeneratorOptions());
                        }

                        stream = mappedFile.CreateViewStream();

                        TextReader tr = new StreamReader(stream);
                        generatedCode = tr.ReadToEnd();
                        generatedCode = generatedCode.Trim(new char());

                        tr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                generatedCode = "#error " + ex.Message;
                throw;
            }
            finally
            {
                using (StreamWriter outfile = new StreamWriter(outputFile))
                {
                    outfile.Write(generatedCode);
                }
            }
        }
    }
}
