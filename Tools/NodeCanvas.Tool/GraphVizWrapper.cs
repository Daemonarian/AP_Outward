using System.Diagnostics;

namespace NodeCanvas.Tool
{
    internal static class GraphVizWrapper
    {
        private const string DotExePath = "dot";

        public static string GenerateSvgFromDot(string dot)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = DotExePath,
                Arguments = "-Tsvg",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var process = new Process { StartInfo = processInfo };
            process.Start();

            using (var stdin = process.StandardInput)
            {
                stdin.Write(dot);
            }

            var svgOutput = process.StandardOutput.ReadToEnd();
            var errorOutput = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception($"Graphviz error ({process.ExitCode}):\n{errorOutput}");
            }

            return svgOutput;
        }
    }
}
