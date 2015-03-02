using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace EmptyKeys.UserInterface.Generator.Values
{
    /// <summary>
    /// Implements value generator for Timeline animation type
    /// </summary>
    public class TimelineGeneratorValue : IGeneratorValue
    {
        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public virtual Type ValueType
        {
            get
            {
                return typeof(Timeline);
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
        public virtual CodeExpression Generate(CodeTypeDeclaration parentClass, CodeMemberMethod method, object value, string baseName, ResourceDictionary dictionary = null)
        {            
            Timeline timeline = value as Timeline;
            if (timeline == null)
            {
                return null;
            }

            CodeExpression baseValue = GenerateTimeline(method, baseName, timeline);
            return baseValue;
        }

        private static CodeExpression GenerateTimeline(CodeMemberMethod method, string baseName, Timeline timeline)
        {
            string timelineTypeName = timeline.GetType().Name;
            if (timelineTypeName == "DoubleAnimation")
            {
                // TODO check this later, all double values in WPF are float in EK UI, so that's why this is needed
                timelineTypeName = "FloatAnimation";
            }

            var timelineVar = new CodeVariableDeclarationStatement(timelineTypeName, baseName, new CodeObjectCreateExpression(timelineTypeName));
            method.Statements.Add(timelineVar);

            var timelineVarRef = new CodeVariableReferenceExpression(baseName);

            var nameAssign = new CodeAssignStatement(new CodeFieldReferenceExpression(timelineVarRef, "Name"), new CodePrimitiveExpression(baseName));
            method.Statements.Add(nameAssign);

            CodeComHelper.GenerateField<bool>(method, timelineVarRef, timeline, Timeline.AutoReverseProperty);

            if (timeline.Duration != Duration.Automatic)
            {
                if (timeline.Duration == Duration.Forever)
                {
                    CodeSnippetStatement error = new CodeSnippetStatement("#error Duration can not be Duration.Forever value");
                    method.Statements.Add(error);
                    return null;
                }

                var durationTimeSpan = new CodeObjectCreateExpression("TimeSpan",
                    new CodePrimitiveExpression(timeline.Duration.TimeSpan.Days),
                    new CodePrimitiveExpression(timeline.Duration.TimeSpan.Hours),
                    new CodePrimitiveExpression(timeline.Duration.TimeSpan.Minutes),
                    new CodePrimitiveExpression(timeline.Duration.TimeSpan.Seconds),
                    new CodePrimitiveExpression(timeline.Duration.TimeSpan.Milliseconds));

                CodeExpression durationValue = new CodeObjectCreateExpression("Duration", durationTimeSpan);
                var durationAssign = new CodeAssignStatement(new CodeFieldReferenceExpression(timelineVarRef, "Duration"), durationValue);
                method.Statements.Add(durationAssign);
            }

            if (!(timeline.RepeatBehavior.HasCount && timeline.RepeatBehavior.Count == 1))
            {
                CodeExpression repeatValue = null;
                if (timeline.RepeatBehavior == RepeatBehavior.Forever)
                {
                    repeatValue = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("RepeatBehavior"), "Forever");
                }
                else if (timeline.RepeatBehavior.HasCount)
                {
                    repeatValue = new CodeObjectCreateExpression("RepeatBehavior", new CodePrimitiveExpression((float)timeline.RepeatBehavior.Count));
                }
                else
                {
                    var repeatTimeSpan = new CodeObjectCreateExpression("TimeSpan",
                        new CodePrimitiveExpression(timeline.RepeatBehavior.Duration.Days),
                        new CodePrimitiveExpression(timeline.RepeatBehavior.Duration.Hours),
                        new CodePrimitiveExpression(timeline.RepeatBehavior.Duration.Minutes),
                        new CodePrimitiveExpression(timeline.RepeatBehavior.Duration.Seconds),
                        new CodePrimitiveExpression(timeline.RepeatBehavior.Duration.Milliseconds));
                    repeatValue = new CodeObjectCreateExpression("RepeatBehavior", repeatTimeSpan);
                }

                var repeatAssign = new CodeAssignStatement(new CodeFieldReferenceExpression(timelineVarRef, "RepeatBehavior"), repeatValue);
                method.Statements.Add(repeatAssign);
            }


            return timelineVarRef;
        }
    }
}
