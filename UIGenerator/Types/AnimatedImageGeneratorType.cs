using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EmptyKeys.UserInterface.Designer;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Image control generator
    /// </summary>
    public class AnimatedImageGeneratorType : ImageGeneratorType
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
                return typeof(AnimatedImage);
            }
        }

        /// <summary>
        /// Generates control fields
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The initialize method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, method, generateField);            

            CodeComHelper.GenerateField<int>(method, fieldReference, source, AnimatedImage.FrameWidthProperty);
            CodeComHelper.GenerateField<int>(method, fieldReference, source, AnimatedImage.FrameHeightProperty);
            CodeComHelper.GenerateField<int>(method, fieldReference, source, AnimatedImage.FramesPerSecondProperty);
            
            return fieldReference;
        }        
    }
}
