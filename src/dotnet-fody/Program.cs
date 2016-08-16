using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace dotnet_fody
{
    public class Program
    {
        public class BuildEngine : IBuildEngine
        {
            public void LogErrorEvent(BuildErrorEventArgs e)
            {
                Console.Error.WriteLine(e.Message);
            }

            public void LogWarningEvent(BuildWarningEventArgs e)
            {
                Console.WriteLine(e.Message);
            }

            public void LogMessageEvent(BuildMessageEventArgs e)
            {
                Console.WriteLine(e.Message);
            }

            public void LogCustomEvent(CustomBuildEventArgs e)
            {
                Console.WriteLine(e.Message);
            }

            public bool BuildProjectFile(string projectFileName, string[] targetNames, IDictionary globalProperties,
                IDictionary targetOutputs)
            {
                throw new NotImplementedException();
            }

            public bool ContinueOnError { get; } = true;
            public int LineNumberOfTaskNode { get; } = 0;
            public int ColumnNumberOfTaskNode { get; } = 0;
            public string ProjectFileOfTaskNode { get; } = "";
        }

        public static void Main(string[] args)
        {
            var outputDir = args[0];
            var outputFile = args[1];
            var configuration = args[2];
            var targetFramework = args[3];

            var projectDirectory = Path.GetDirectoryName(FindProjectJson(outputDir));
            var processor = new Processor
            {
                Logger = new BuildLogger
                {
                    BuildEngine = new BuildEngine(),
                },
                AssemblyFilePath = outputFile,
                IntermediateDirectory = Path.Combine(projectDirectory, "obj"),
                KeyFilePath = null, // TODO
                SignAssembly = false, // TODO
                ProjectDirectory = projectDirectory,
                References = "", // TODO
                SolutionDirectory = Path.GetDirectoryName(FindSolution(outputDir)),
                ReferenceCopyLocalPaths = new List<string>(), // TODO: Read project.json?
                DefineConstants = new List<string>(), // TODO: Read project.json 
                NuGetPackageRoot = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), ".nuget", "packages")
            };
            var success = processor.Execute();
            if (success)
            {
                var weavers = processor.Weavers.Select(x => x.AssemblyName);
                Console.WriteLine("Executed Weavers: " + string.Join(";", weavers));
            }
            else
            {
                Console.WriteLine("Weaving failed");
            }
        }

        private static string FindSolution(string dir)
        {
            if (dir == Path.GetPathRoot(dir))
                throw new Exception("Could not find solution");

            return Directory.EnumerateFiles(dir, "*.sln").FirstOrDefault()
                   ?? FindSolution(Path.GetDirectoryName(dir));
        }


        private static string FindProjectJson(string dir)
        {
            if (dir == Path.GetPathRoot(dir))
                throw new Exception("Could not find project");

            return Directory.EnumerateFiles(dir, "project.json").FirstOrDefault()
                   ?? FindProjectJson(Path.GetDirectoryName(dir));
        }
    }
}
