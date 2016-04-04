using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements UI Generator for Track WPF control
    /// </summary>
    public class TrackGeneratorType : ElementGeneratorType
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
                return typeof(Track);
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
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, method, generateField);

            Track track = source as Track;
            if (track != null)
            {
                CodeComHelper.GenerateField<bool>(method, fieldReference, source, Track.IsDirectionReversedProperty);

                TypeGenerator generator = new TypeGenerator();

                if (track.IncreaseRepeatButton != null)
                {
                    CodeExpression incButton = generator.ProcessGenerators(track.IncreaseRepeatButton, classType, method, false);
                    method.Statements.Add(new CodeAssignStatement(
                        new CodeFieldReferenceExpression(fieldReference, "IncreaseRepeatButton"), incButton));
                }

                if (track.DecreaseRepeatButton != null)
                {
                    CodeExpression decButton = generator.ProcessGenerators(track.DecreaseRepeatButton, classType, method, false);
                    method.Statements.Add(new CodeAssignStatement(
                        new CodeFieldReferenceExpression(fieldReference, "DecreaseRepeatButton"), decButton));
                }

                if (track.Thumb != null)
                {
                    CodeExpression thumb = generator.ProcessGenerators(track.Thumb, classType, method, false);
                    method.Statements.Add(new CodeAssignStatement(
                        new CodeFieldReferenceExpression(fieldReference, "Thumb"), thumb));
                }
            }

            return fieldReference;
        }
    }
}
