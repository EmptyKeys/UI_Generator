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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml;
using EmptyKeys.UserInterface.Designer;
using EmptyKeys.UserInterface.Generator.Types;

namespace EmptyKeys.UserInterface.Generator
{
    /// <summary>
    /// Implements main code for UI generator
    /// </summary>
    public class UserInterfaceGenerator
    {
        private readonly string memoryMappedFileName = "GenerationCodeMapppedFile";
        private readonly long nemoryMappedFileCapacity = 1000000;
        private readonly string initComponentsMethodName = "InitializeComponent";
        private readonly string initMethodName = "Initialize";

        private TypeGenerator generator = new TypeGenerator();

        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <param name="inputFileName">Name of the input file.</param>
        /// <param name="inputFileContent">Content of the input file.</param>
        /// <param name="renderMode">The render mode.</param>
        /// <param name="desiredNamespace">The desired namespace.</param>
        /// <returns></returns>
        public string GenerateCode(string inputFileName, string inputFileContent, RenderMode renderMode, string desiredNamespace)
        {
            inputFileContent = RemoveClass(inputFileContent);

            var parserContext = new ParserContext
            {
                BaseUri = new Uri(inputFileName, UriKind.Absolute)                
            };                                   

            object source = null;
            try
            {
                using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(inputFileContent)))
                {
                    source = XamlReader.Load(stream, parserContext);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            if (source == null)
            {
                return "Source is empty. XAML file is not valid.";
            }            

            Console.WriteLine();
            Console.WriteLine("Generating " + inputFileName);

            ElementGeneratorType.NameUniqueId = 0;            
            string className = Path.GetFileNameWithoutExtension(inputFileName);

            CodeNamespace ns = new CodeNamespace(desiredNamespace);
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

            ns.Comments.Add(new CodeCommentStatement("-----------------------------------------------------------", false));
            ns.Comments.Add(new CodeCommentStatement(" ", false));
            ns.Comments.Add(new CodeCommentStatement(" This file was generated, please do not modify.", false));
            ns.Comments.Add(new CodeCommentStatement(" ", false));
            ns.Comments.Add(new CodeCommentStatement("-----------------------------------------------------------", false));            

            CodeMemberMethod initMethod = null;
            if (source is UIRoot)
            {
                initMethod = CreateUIRootClass(ns, classType, renderMode);
                generator.ProcessGenerators(source, classType, initMethod, true);
            }
            else if (source is UserControl)
            {
                initMethod = CreateUserControlClass(ns, classType);
                generator.ProcessGenerators(source, classType, initMethod, true);
            }
            else if (source is ResourceDictionary)
            {
                initMethod = CreateDictionaryClass(ns, classType);

                ResourceDictionary dictionary = source as ResourceDictionary;
                if (dictionary != null)
                {
                    ResourceDictionaryGenerator resourcesGenerator = new ResourceDictionaryGenerator();
                    resourcesGenerator.Generate(dictionary, classType, initMethod, new CodeThisReferenceExpression());
                }
            }
            else
            {
                string errorText = "#error This type is not supported - " + source.GetType();
                Console.WriteLine(errorText);
                return errorText;
            }

            ImageAssets.Instance.GenerateManagerCode(initMethod);
            ImageAssets.Instance.ClearAssets();

            FontGenerator.Instance.GenerateManagerCode(initMethod);

            if (BindingGenerator.Instance.IsEnabled)
            {
                BindingGenerator.Instance.GenerateRegistrationCode(initMethod);
            }

            string resultCode = string.Empty;
            using (CodeDomProvider provider = new Microsoft.CSharp.CSharpCodeProvider())
            {
                string mappedFileName = memoryMappedFileName + className;
                using (MemoryMappedFile mappedFile = MemoryMappedFile.CreateNew(mappedFileName, nemoryMappedFileCapacity))
                {
                    MemoryMappedViewStream stream = mappedFile.CreateViewStream();

                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        provider.GenerateCodeFromNamespace(ns, sw, new CodeGeneratorOptions());
                    }

                    stream = mappedFile.CreateViewStream();

                    TextReader tr = new StreamReader(stream);
                    resultCode = tr.ReadToEnd();
                    resultCode = resultCode.Trim(new char());

                    tr.Close();
                }
            }

            return resultCode;
        }

        private string RemoveClass(string inputFileContent)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(inputFileContent);

            if (xml.DocumentElement.Attributes["x:Class"] != null)
            {
                xml.DocumentElement.Attributes.Remove(xml.DocumentElement.Attributes["x:Class"]);
            }

            return xml.OuterXml;
        }

        private CodeMemberMethod CreateDictionaryClass(CodeNamespace ns, CodeTypeDeclaration classType)
        {
            classType.BaseTypes.Add(new CodeTypeReference("ResourceDictionary"));
            classType.TypeAttributes = TypeAttributes.Sealed | TypeAttributes.Public;
            ns.Types.Add(classType);

            CodeTypeReference typeRef = new CodeTypeReference(classType.Name);
            CodeMemberField singleton = new CodeMemberField(typeRef, "singleton");
            singleton.Attributes = MemberAttributes.Static | MemberAttributes.Private;
            singleton.InitExpression = new CodeObjectCreateExpression(classType.Name);
            classType.Members.Add(singleton);

            CodeMemberProperty instance = new CodeMemberProperty();
            instance.Attributes = MemberAttributes.Public | MemberAttributes.Static;
            instance.Name = "Instance";
            instance.Type = typeRef;
            instance.GetStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression(singleton.Name)));
            classType.Members.Add(instance);

            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;

            CodeMethodInvokeExpression initMethodCall = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                "InitializeResources",
                new CodeExpression[] { });
            constructor.Statements.Add(initMethodCall);
            classType.Members.Add(constructor);

            CodeMemberMethod initMethod = new CodeMemberMethod();
            initMethod.Name = "InitializeResources";
            classType.Members.Add(initMethod);

            return initMethod;
        }

        private CodeMemberMethod CreateUserControlClass(CodeNamespace ns, CodeTypeDeclaration classType)
        {
            classType.BaseTypes.Add(new CodeTypeReference("UserControl"));
            classType.IsPartial = true;
            ns.Types.Add(classType);

            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;

            constructor.Statements.Add(new CodeVariableDeclarationStatement("Style", "style",
                    new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("UserControlStyle"), "CreateUserControlStyle")));

            constructor.Statements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("style"), "TargetType"),
                new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetType")));

            constructor.Statements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "Style"),
                new CodeVariableReferenceExpression("style")));

            CodeMethodInvokeExpression initMethodCall = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                initComponentsMethodName,
                new CodeExpression[] { });
            constructor.Statements.Add(initMethodCall);
            classType.Members.Add(constructor);

            CodeMemberMethod initMethod = new CodeMemberMethod();
            initMethod.Name = initComponentsMethodName;
            classType.Members.Add(initMethod);

            return initMethod;
        }

        private CodeMemberMethod CreateUIRootClass(CodeNamespace ns, CodeTypeDeclaration classType, RenderMode renderMode)
        {
            classType.BaseTypes.Add(new CodeTypeReference("UIRoot"));
            classType.IsPartial = true;
            ns.Types.Add(classType);

            CodeMethodInvokeExpression initMethodCall = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                initMethodName,
                new CodeExpression[] { });

            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            constructor.BaseConstructorArgs.Add(new CodeSnippetExpression(string.Empty));
            constructor.Statements.Add(initMethodCall);
            classType.Members.Add(constructor);

            CodeConstructor constructorWithParams = new CodeConstructor();
            constructorWithParams.Attributes = MemberAttributes.Public;
            constructorWithParams.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "width"));
            constructorWithParams.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "height"));
            constructorWithParams.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("width"));
            constructorWithParams.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("height"));
            constructorWithParams.Statements.Add(initMethodCall);
            classType.Members.Add(constructorWithParams);

            CodeMemberMethod initMethod = new CodeMemberMethod();
            initMethod.Name = initMethodName;
            classType.Members.Add(initMethod);

            initMethod.Statements.Add(new CodeVariableDeclarationStatement("Style", "style",
                    new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("RootStyle"), "CreateRootStyle")));

            initMethod.Statements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("style"), "TargetType"),
                new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetType")));

            initMethod.Statements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "Style"),
                new CodeVariableReferenceExpression("style")));

            CodeMethodInvokeExpression initComponentMethodCall = new CodeMethodInvokeExpression(
                new CodeThisReferenceExpression(),
                initComponentsMethodName,
                new CodeExpression[] { });
            initMethod.Statements.Add(initComponentMethodCall);

            CodeMemberMethod initComponentsMethod = new CodeMemberMethod();
            initComponentsMethod.Name = initComponentsMethodName;
            classType.Members.Add(initComponentsMethod);

            return initComponentsMethod;
        }
    }
}
