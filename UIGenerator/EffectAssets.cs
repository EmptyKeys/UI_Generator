using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyKeys.UserInterface.Generator
{
    /// <summary>
    /// Implements Effect Assets store
    /// </summary>
    public sealed class EffectAssets
    {
        private static EffectAssets singleton = new EffectAssets();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static EffectAssets Instance
        {
            get
            {
                return singleton;
            }
        }

        private List<string> effectAssets = new List<string>();

        /// <summary>
        /// Prevents a default instance of the <see cref="EffectAssets"/> class from being created.
        /// </summary>
        private EffectAssets()
        {
        }

        /// <summary>
        /// Adds the effect.
        /// </summary>
        /// <param name="assetName">Name of the asset.</param>
        public void AddEffect(string assetName)
        {
            if (!effectAssets.Contains(assetName))
            {
                effectAssets.Add(assetName);                
            }
        }

        /// <summary>
        /// Generates the manager code.
        /// </summary>
        /// <param name="method">The method.</param>
        public void GenerateManagerCode(CodeMemberMethod method)
        {
            foreach (var asset in effectAssets)
            {
                method.Statements.Add(new CodeMethodInvokeExpression(
                    new CodeFieldReferenceExpression(
                        new CodeTypeReferenceExpression("EffectManager"), "Instance"),
                        "AddEffect",
                        new CodePrimitiveExpression(asset)));
            }
        }

        /// <summary>
        /// Clears the assets.
        /// </summary>
        public void ClearAssets()
        {
            effectAssets.Clear();
        }
    }
}
