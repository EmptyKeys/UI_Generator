using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmptyKeys.UserInterface.Generator
{
    /// <summary>
    /// Implements Image assets store
    /// </summary>
    public sealed class ImageAssets
    {
        private static ImageAssets singleton = new ImageAssets();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ImageAssets Instance
        {
            get
            {
                return singleton;
            }
        }

        private List<string> imageAssets = new List<string>();
        private List<string> files = new List<string>();

        private ImageAssets()
        {
        }

        /// <summary>
        /// Adds the image asset
        /// </summary>
        /// <param name="assetName">Name of the asset.</param>
        /// <param name="extension">The extension.</param>
        public void AddImage(string assetName, string extension)
        {
            if (!imageAssets.Contains(assetName))
            {
                imageAssets.Add(assetName);
                string assetFile = assetName + extension;
                if (!files.Contains(assetFile))
                {
                    files.Add(assetFile);
                }
            }
        }

        /// <summary>
        /// Generates the manager code.
        /// </summary>
        /// <param name="method">The method.</param>
        public void GenerateManagerCode(CodeMemberMethod method)
        {
            foreach (var asset in imageAssets)
            {                
                method.Statements.Add(new CodeMethodInvokeExpression(
                    new CodeFieldReferenceExpression(
                        new CodeTypeReferenceExpression("ImageManager"), "Instance"),
                        "AddImage",
                        new CodePrimitiveExpression(asset)));
            }
        }

        /// <summary>
        /// Clears the assets.
        /// </summary>
        public void ClearAssets()
        {
            imageAssets.Clear();
        }

        /// <summary>
        /// Copies the images from the current working directory to the specified asset target directory.
        /// </summary>
        /// <param name="TargetDir">The assets directory.</param>
        public bool CopyImagesToAssetDirectory(string TargetDir)
        {
            string SourceDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return CopyImagesToAssetDirectory(TargetDir, SourceDir);
        }

        /// <summary>
        /// Copies the images from the specified source directory to the specified asset target directory.
        /// </summary>
        /// <param name="TargetDir">The assets directory.</param>
        /// <param name="SourceDir">The directory to copy assets from.</param>
        /// <returns></returns>
        public bool CopyImagesToAssetDirectory(string TargetDir, string SourceDir)
        {
            foreach (string asset in files)
            {
                string sourceFile = Path.Combine(SourceDir, asset);
                string target = Path.Combine(TargetDir, asset);

                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(target)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(target));
                    }

                    File.Copy(sourceFile, target, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while copying image asset: " + asset);
                    Console.WriteLine(ex);
                    return false;
                }
            }

            return true;
        }
    }
}
