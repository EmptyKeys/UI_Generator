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
            string inputDirectory = string.Empty;
            string outputDirectory = string.Empty;
            string assetsDirectory = string.Empty;
            string desiredNamespace = "EmptyKeys.UserInterace.Generated";
            RenderMode renderMode = RenderMode.SunBurn;

            var optionSet = new OptionSet()
                .Add("?|help|h", "Command line help", o => showHelp = o != null)
                .Add<string>("i|input=", "Input directory with XAML files", o => inputDirectory = o)
                .Add<string>("o|output=", "Output directory for .cs files", o => outputDirectory = o)
                .Add<string>("oa=", "Output Asset directory for generated sprite fonts and images", o => assetsDirectory = o)
                .Add<RenderMode>("rm=", 
                    String.Format("Render mode ({0})", String.Join(", ", Enum.GetNames(typeof(RenderMode)))),
                    o => renderMode = o)
                .Add<string>("ns|namespace:", "The namespace to generate the code under",  o => desiredNamespace = o);

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

            if (args.Length < 3)
            {
                Console.WriteLine("ERROR: Some argument is missing.");
                ShowHelp(optionSet);
                return -1;
            }

            if (string.IsNullOrEmpty(inputDirectory) || !Directory.Exists(inputDirectory))
            {
                Console.WriteLine("ERROR: Input directory does not exist.");
                ShowHelp(optionSet);
                return -1;
            }

            if (string.IsNullOrEmpty(outputDirectory))
            {
                Console.WriteLine("ERROR: Empty output directory argument.");
                ShowHelp(optionSet);
                return -1;
            }

            if (string.IsNullOrEmpty(assetsDirectory))
            {
                Console.WriteLine("ERROR: Empty assets directory argument.");
                ShowHelp(optionSet);
                return -1;
            }
            if (string.IsNullOrEmpty(desiredNamespace))
            {
                Console.WriteLine("ERROR: Cannot have an empty namespace.");
                ShowHelp(optionSet);
                return -1;
            }

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            if (!Directory.Exists(assetsDirectory))
            {
                Directory.CreateDirectory(assetsDirectory);
            }

            foreach (var file in Directory.EnumerateFiles(inputDirectory, "*.xaml", SearchOption.AllDirectories))
            {
                string relativeDirectory = file.Remove(0, inputDirectory.Length).TrimStart(Path.DirectorySeparatorChar);
                string outputFile = Path.Combine(outputDirectory, relativeDirectory) + ".cs";

                try
                {
                    Generate(file, outputFile, renderMode, desiredNamespace);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return -2;
                }
            }

            FontGenerator.Instance.GenerateFontAssets(assetsDirectory, renderMode);
            bool result = ImageAssets.Instance.CopyImagesToAssetDirectory(assetsDirectory);
            if (!result)
            {
                return -3;
            }

            return 0;
        }

        private static void Generate(string xamlFile, string outputFile, RenderMode renderMode, string desiredNamespace)
        {
            string xaml = string.Empty;
            using (TextReader tr = File.OpenText(xamlFile))
            {
                xaml = tr.ReadToEnd();
            }

            UserInterfaceGenerator generator = new UserInterfaceGenerator();
            string generatedCode = generator.GenerateCode(xamlFile, xaml, renderMode, desiredNamespace);

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
