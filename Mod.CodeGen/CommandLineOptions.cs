using CommandLine;

namespace OutwardArchipelago.CodeGen
{
    public class CommandLineOptions
    {
        [Option('o', "output", Required = false, Default = "-", HelpText = "Output file path.")]
        public string OutputPath { get; set; } = string.Empty;

        [Option("namespace", Required = false, Default = "OutwardArchipelago.Archipelago", HelpText = "The namespace of the generated classes.")]
        public string Namespace { get; set; } = string.Empty;

        [Option("class", Required = false, Default = "APWorld", HelpText = "The name of the generated class containing the APWorld info.")]
        public string Class { get; set; } = string.Empty;

        [Option("access-modifier", Required = false, Default = "internal", HelpText = "The access modifier to use when declaring the classes and constants.")]
        public string AccessModifier { get; set; } = string.Empty;
    }
}
