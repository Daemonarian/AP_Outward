using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NodeCanvas.Tool.Schema;

namespace NodeCanvas.Tool
{
    internal class Program
    {
        private static readonly JsonSerializerOptions NodeCanvasSerializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            AllowOutOfOrderMetadataProperties = true,
        };

        static void Main(string[] args)
        {
            var rawJson = File.ReadAllText("C:\\Users\\Zack Pepin\\source\\repos\\AP_Outward\\Mod\\assets\\plugins\\graphs\\Dialogue_RolandArgenson_Neut_Initial.json", Encoding.UTF8);
            var graphTemplate = JsonSerializer.Deserialize<GraphReplacementTemplate>(rawJson, NodeCanvasSerializerOptions) ?? throw new Exception("Failed to deserialize graph replacement template."); ;

            var graphVizCode = GraphVizConverter.ToGraphViz(graphTemplate.Graph);

            var svgPath = Path.GetTempFileName();
            var dotProcessInfo = new ProcessStartInfo
            {
                FileName = "dot",
                Arguments = $"-Tsvg -o \"{svgPath}\"",
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using (var process = Process.Start(dotProcessInfo) ?? throw new Exception("Failed to start dot process."))
            {
                using (var streamWriter = process.StandardInput)
                {
                    streamWriter.Write(graphVizCode);
                }

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine(graphVizCode);
                    throw new Exception("Graphviz exited with non-zero exit code.");
                }
            }

            var svgUri = new Uri(svgPath);
            var firefoxProcessInfo = new ProcessStartInfo
            {
                FileName = "C:\\Program Files\\Mozilla Firefox\\firefox.exe",
                Arguments = $"\"{svgUri}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using (var process = Process.Start(firefoxProcessInfo) ?? throw new Exception("Failed to start firefox process."))
            {
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine(svgPath);
                    throw new Exception("Firefox exited with non-zero exit code.");
                }
            }
        }
    }
}
