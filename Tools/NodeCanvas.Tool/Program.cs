using CommandLine;

namespace NodeCanvas.Tool
{
    internal class Program
    {
        private const string StdFileName = "-";

        /// <summary>
        /// Command-line options for use with CommandLineParser.
        /// </summary>
        public class Options
        {
            [Option('i', "input", Required = false, Default = StdFileName,
                HelpText = $"The input JSON file. If no input is specified, or the special value \"{StdFileName}\", then input is read from standard input.")]
            public string Input { get; set; } = StdFileName;

            [Option('o', "output", Required = false, Default = StdFileName,
                HelpText = $"The output file. If no output is specified, or the special value \"{StdFileName}\", then output is written to standard output.")]
            public string Output { get; set; } = StdFileName;

            [Option('f', "format", Required = false, Default = FormatOptions.Template,
                HelpText = $"The output format.")]
            public FormatOptions Format { get; set; } = FormatOptions.Dot;

            /// <summary>
            /// Possible output formats.
            /// </summary>
            public enum FormatOptions
            {
                Template,
                Dot,
                SVG,
            }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Main);
        }

        static void Main(Options options)
        {
            // read the input file

            string rawInput;
            if (string.Equals(options.Input, StdFileName, StringComparison.Ordinal))
            {
                rawInput = Console.In.ReadToEnd();
            }
            else
            {
                rawInput = File.ReadAllText(options.Input);
            }

            // parse the input

            var template = GraphTemplateSerializer.Deserialize(rawInput);

            // convert to output format

            string rawOutput;
            if (options.Format == Options.FormatOptions.Template)
            {
                rawOutput = GraphTemplateSerializer.Serialize(template);
            }
            else
            {
                var dot = GraphVizConverter.ToGraphViz(template.Graph);
                if (options.Format == Options.FormatOptions.Dot)
                {
                    rawOutput = dot;
                }
                else if (options.Format == Options.FormatOptions.SVG)
                {
                    rawOutput = GraphVizWrapper.GenerateSvgFromDot(dot);
                }
                else
                {
                    throw new Exception($"unexpected format option: {options.Format}");
                }
            }

            // write the output

            if (string.Equals(options.Output, StdFileName, StringComparison.Ordinal))
            {
                Console.Out.Write(rawOutput);
            }
            else
            {
                File.WriteAllText(options.Output, rawOutput);
            }
        }
    }
}
