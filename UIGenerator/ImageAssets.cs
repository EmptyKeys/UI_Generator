using System;
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

        private ImageAssets()
        {
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="fileName">Name of the asset.</param>
        public void AddImage(string fileName)
        {
            if (!imageAssets.Contains(fileName))
            {
                imageAssets.Add(fileName);
            }
        }

        /// <summary>
        /// Copies the images to asset directory.
        /// </summary>
        /// <param name="assetsDirectory">The assets directory.</param>
        public bool CopyImagesToAssetDirectory(string assetsDirectory)
        {
            foreach (string asset in imageAssets)
            {
                string source = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), asset);
                string target = Path.Combine(assetsDirectory, asset);

                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(target)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(target));
                    }

                    File.Copy(source, target, true);
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
