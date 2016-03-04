using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
            Console.WriteLine("Empty Keys (c) 2015 User Interface Generator Console v" + Assembly.GetExecutingAssembly().GetName().Version.ToString());

            bool showHelp = false;
            bool ignoreImageAssets = false;
            bool ignoreFontAssets = false;
            bool generateBindings = false;
            string inputDirectory = string.Empty;
            string outputDirectory = string.Empty;
            string assetOutputDirectory = string.Empty;
            string assetInputDirectory = string.Empty;
            RenderMode renderMode = RenderMode.SunBurn;
            string desiredNamespace = string.Empty;
            string buildDir = string.Empty;
            string defaultAssembly = string.Empty;

            var optionSet = new OptionSet()
                .Add("?|help|h", "Command line help", o => showHelp = o != null)
                .Add<string>("i|input=", "Input directory with XAML files", o => inputDirectory = o)
                .Add<string>("o|output=", "Output directory for .cs files", o => outputDirectory = o)
                .Add("no-copy-images", "Do not copy generated image assets (ignores input asset directory)", o => ignoreImageAssets = o != null)
                .Add("no-fonts", "Do not generate font assets (may lead to broken output)", o => ignoreFontAssets = o != null)
                .Add("ignore-assets", "Ignore all asset files and just generate .xaml.cs files", o => ignoreImageAssets = ignoreFontAssets = o != null)
                .Add<string>("ia=", "Input asset directory to copy images from", o => assetInputDirectory = o)
                .Add<string>("oa=", "Output Asset directory for generated sprite fonts and images", o => assetOutputDirectory = o)
                .Add<RenderMode>("rm=",
                    String.Format("Render mode ({0})", String.Join(", ", Enum.GetNames(typeof(RenderMode)))),
                    o => renderMode = o)
                .Add<string>("ns|namespace=", "The namespace to generate the code under", o => desiredNamespace = o)
                .Add<string>("bd|buildDir=", "Directory for additional assemblies", o => buildDir = o)
                .Add("generate-bindings", "Generate data bindings", o => generateBindings = o != null)
                .Add<string>("da|defaultAssembly=", "Assembly name to use for clr-namespaces without an assembly", o => defaultAssembly = o);

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

            if (!(ignoreFontAssets && ignoreImageAssets) && string.IsNullOrEmpty(assetOutputDirectory))
            {
                Console.WriteLine("WARNING: No asset output directory specified. No image or font files will be created (specify --ignore-assets if this was intentional).");
            }

            if (string.IsNullOrEmpty(desiredNamespace))
            {
                desiredNamespace = "EmptyKeys.UserInterface.Generated";                
            }

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            if (!(ignoreFontAssets && ignoreImageAssets) && !string.IsNullOrEmpty(assetOutputDirectory) && !Directory.Exists(assetOutputDirectory))
            {
                try
                {
                    Directory.CreateDirectory(assetOutputDirectory);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error creating asset output directory: " + e.Message);
                    return -1;
                }
            }

            if (!string.IsNullOrEmpty(buildDir))
            {
                Console.WriteLine("Copy of additional assemblies...");                
                CopyDirectory(buildDir, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), true);
            }

            BindingGenerator.Instance.IsEnabled = generateBindings;

            foreach (var file in Directory.EnumerateFiles(inputDirectory, "*.xaml", SearchOption.AllDirectories))
            {
                if (BindingGenerator.Instance.IsEnabled)
                {
                    BindingGenerator.Instance.GenerateNamespace(desiredNamespace + "." + Path.GetFileNameWithoutExtension(file) + "_Bindings");
                }

                string relativeDirectory = file.Remove(0, inputDirectory.Length).TrimStart(Path.DirectorySeparatorChar);
                string outputFile = Path.Combine(outputDirectory, relativeDirectory) + ".cs";                

                try
                {
                    Generate(file, outputFile, renderMode, desiredNamespace, defaultAssembly);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return -2;
                }

                if (generateBindings)
                {
                    outputFile = Path.Combine(outputDirectory, relativeDirectory) + "_bindings.cs";
                    BindingGenerator.Instance.GenerateFile(outputFile);
                }
            }

            if (!ignoreFontAssets && !string.IsNullOrEmpty(assetOutputDirectory))
            {
                FontGenerator.Instance.GenerateFontAssets(assetOutputDirectory, renderMode);
            }

            if (!ignoreImageAssets)
            {
                bool result;
                if (!string.IsNullOrWhiteSpace(assetInputDirectory))
                {
                    result = ImageAssets.Instance.CopyImagesToAssetDirectory(assetOutputDirectory, assetInputDirectory);
                }
                else
                {
                    result = ImageAssets.Instance.CopyImagesToAssetDirectory(assetOutputDirectory);
                }

                if (!result)
                {
                    return -3;
                }
            }

            return 0;
        }

        private static void Generate(string xamlFile, string outputFile, RenderMode renderMode, string desiredNamespace, string defaultAssembly)
        {
            string xaml = string.Empty;
            using (TextReader tr = File.OpenText(xamlFile))
            {
                xaml = tr.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(defaultAssembly))
                xaml = Regex.Replace(xaml,
                    @"xmlns(:\w+)?=\""clr-namespace:([.\w]+)(;assembly=)?\""",
                    $@"xmlns$1=""clr-namespace:$2;assembly=" + defaultAssembly + '"');

            UserInterfaceGenerator generator = new UserInterfaceGenerator();
            string generatedCode = string.Empty;
            try
            {
                generatedCode = generator.GenerateCode(xamlFile, xaml, renderMode, desiredNamespace);
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

        private static void ShowHelp(OptionSet optionSet)
        {
            optionSet.WriteOptionDescriptions(Console.Out);
        }

        private static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
