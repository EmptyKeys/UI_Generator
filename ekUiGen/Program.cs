using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EmptyKeys.UserInterface.Generator;
using Mono.Options;

namespace ekUiGen
{
    class Program
    {        
        [STAThread]
        static int Main(string[] args)
        {
            Console.WriteLine("Empty Keys (c) 2014 User Interface Generator Console v" + Assembly.GetExecutingAssembly().GetName().Version.ToString());

            bool showHelp = false;
            bool IgnoreImageAssets = false;
            bool IgnoreFontAssets = false;
            string inputDirectory = string.Empty;
            string outputDirectory = string.Empty;
            string assetOutputDirectory = string.Empty;
            string assetInputDirectory = string.Empty;
            RenderMode renderMode = RenderMode.SunBurn;

            var optionSet = new OptionSet()
                .Add("?|help|h", "Command line help", o => showHelp = o != null)
                .Add<string>("i|input=", "Input directory with XAML files", o => inputDirectory = o)
                .Add<string>("o|output=", "Output directory for .cs files", o => outputDirectory = o)
                .Add("no-copy-images", "Do not copy generated image assets (ignores input asset directory)", o => IgnoreImageAssets = o != null)
                .Add("no-fonts", "Do not generate font assets (may lead to broken output)", o => IgnoreFontAssets = o != null)
                .Add("ignore-assets", "Ignore all asset files and just generate .xaml.cs files", o => IgnoreImageAssets = IgnoreFontAssets = o != null)
                .Add<string>("ia=", "Input asset directory to copy images from", o => assetInputDirectory = o)
                .Add<string>("oa=", "Output Asset directory for generated sprite fonts and images", o => assetOutputDirectory = o)
                .Add<RenderMode>("render|rendermode|rm=", "Render mode (SunBurn/MonoGame)", o => renderMode = o);
                
            try
            {
                optionSet.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                ShowHelp(optionSet);
                return -1;
            }

            if (showHelp)
            {
                ShowHelp(optionSet);
                return 0;
            }
                        
            if (string.IsNullOrEmpty(inputDirectory))
            {
                Console.WriteLine("ERROR: You must specify an input directory.");
                ShowHelp(optionSet);
                return -1;
            }

            if (!Directory.Exists(inputDirectory))
            {
                Console.WriteLine("ERROR: The specified input directory does not exist: " + inputDirectory);
                ShowHelp(optionSet);
                return -1;
            }

            if (string.IsNullOrEmpty(outputDirectory))
            {
                Console.WriteLine("ERROR: You must specify a code output directory.");
                ShowHelp(optionSet);
                return -1;
            }
            
            if (!(IgnoreFontAssets && IgnoreImageAssets) && string.IsNullOrEmpty(assetOutputDirectory))
                Console.WriteLine("WARNING: No asset output directory specified. No image or font files will be created (specify --ignore-assets if this was intentional).");

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            if (!(IgnoreFontAssets && IgnoreImageAssets) && !string.IsNullOrEmpty(assetOutputDirectory) && !Directory.Exists(assetOutputDirectory))
            {
                try
                {
                    Directory.CreateDirectory(assetOutputDirectory);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error creating asset output directory: " + e.Message);
                    return -1;
                }
            }

            foreach (var file in Directory.EnumerateFiles(inputDirectory, "*.xaml", SearchOption.AllDirectories))
            {
                string relativeDirectory = file.Remove(0, inputDirectory.Length).TrimStart(Path.DirectorySeparatorChar);
                string outputFile = Path.Combine(outputDirectory, relativeDirectory) + ".cs";

                try
                {
                    Generate(file, outputFile, renderMode);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return -2;
                }
            }
            
            if(!IgnoreFontAssets && !string.IsNullOrEmpty(assetOutputDirectory))
                FontGenerator.Instance.GenerateFontAssets(assetOutputDirectory, renderMode);

            if (!IgnoreImageAssets)
            {
                bool result;
                if(assetInputDirectory.Length > 0)
                    result = ImageAssets.Instance.CopyImagesToAssetDirectory(assetOutputDirectory, assetInputDirectory);
                else
                    result = ImageAssets.Instance.CopyImagesToAssetDirectory(assetOutputDirectory);

                if (!result)
                    return -3;
            }

            return 0;
        }

        private static void Generate(string xamlFile, string outputFile, RenderMode renderMode)
        {
            string xaml = string.Empty;
            using (TextReader tr = File.OpenText(xamlFile))
            {
                xaml = tr.ReadToEnd();
            }

            UserInterfaceGenerator generator = new UserInterfaceGenerator();
            string generatedCode = generator.GenerateCode(xamlFile, xaml, renderMode);

            using (StreamWriter outfile = new StreamWriter(outputFile))
            {
                outfile.Write(generatedCode);
            }
        }

        private static void ShowHelp(OptionSet optionSet)
        {
            optionSet.WriteOptionDescriptions(Console.Out);
        }
    }
}
