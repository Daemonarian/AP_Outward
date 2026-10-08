using System.Diagnostics;
using System.Text;

namespace Conflux.GraphViz
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
                StandardInputEncoding = new UTF8Encoding(false),
                StandardOutputEncoding = new UTF8Encoding(false),
                StandardErrorEncoding = new UTF8Encoding(false),
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var process = new Process { StartInfo = processInfo };
            process.Start();

            Exception? writeException = null;
            try
            {
                using var stdin = process.StandardInput;
                stdin.Write(dot);
            }
            catch (Exception ex)
            {
                writeException = ex;
            }

            var svgOutput = process.StandardOutput.ReadToEnd();
            var errorOutput = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0 || writeException is not null)
            {
                if (string.IsNullOrWhiteSpace(errorOutput) && writeException is not null)
                {
                    errorOutput = writeException.ToString();
                }

                throw new Exception($"Graphviz error ({process.ExitCode}):\n{errorOutput}");
            }

            return svgOutput;
        }
    }
}
