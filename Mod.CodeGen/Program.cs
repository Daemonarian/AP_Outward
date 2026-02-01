using System.Globalization;
using System.Text;
using System.Text.Json;
using CommandLine;

namespace OutwardArchipelago.CodeGen
{
    internal class Program
    {
        static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        };

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(GenerateCode);
        }

        static void GenerateCode(CommandLineOptions opts)
        {
            var culture = CultureInfo.CurrentCulture;

            // parse the APWorld item ids

            var apworldJsonFile = Path.Join(AppContext.BaseDirectory, "apworld_info.json");
            var apworldJsonText = File.ReadAllText(apworldJsonFile);
            var apworld = JsonSerializer.Deserialize<APWorld>(apworldJsonText, _jsonSerializerOptions) ?? throw new Exception("Failed to load the APWorld info.");

            // generate code

            var sb = new StringBuilder();

            sb.AppendLine($"using System.Collections.Generic;");

            if (!string.IsNullOrEmpty(opts.Namespace))
            {
                sb.AppendLine($"namespace {opts.Namespace}");
                sb.AppendLine($"{{");
            }

            sb.AppendLine($"    {opts.AccessModifier} static partial class {opts.Class}");
            sb.AppendLine($"    {{");
            sb.AppendLine($"        public const string ArchipelagoVersion = \"{Escape(apworld.ArchipelagoVersion)}\";");
            sb.AppendLine($"        public const string Game = \"{Escape(apworld.Game)}\";");
            sb.AppendLine($"        public sealed partial class Item");
            sb.AppendLine($"        {{");

            foreach (var item in apworld.Items)
            {
                sb.AppendLine($"            public static readonly Item {SnakeToPascalCase(item.Key, culture)} = new(0x{item.Id:X16}, \"{Escape(item.Name)}\");");
            }

            sb.AppendLine($"            public static readonly IReadOnlyDictionary<long, Item> ById = new Dictionary<long, Item>");
            sb.AppendLine($"            {{");

            foreach (var item in apworld.Items)
            {
                sb.AppendLine($"                {{ 0x{item.Id:X16}, {SnakeToPascalCase(item.Key, culture)} }},");
            }

            sb.AppendLine($"            }};");
            sb.AppendLine($"        }}");
            sb.AppendLine($"        public sealed partial class Location");
            sb.AppendLine($"        {{");

            foreach (var location in apworld.Locations)
            {
                sb.AppendLine($"            public static readonly Location {SnakeToPascalCase(location.Key, culture)} = new(0x{location.Id:X16}, \"{Escape(location.Name)}\");");
            }

            sb.AppendLine($"            public static readonly IReadOnlyDictionary<long, Location> ById = new Dictionary<long, Location>");
            sb.AppendLine($"            {{");

            foreach (var location in apworld.Locations)
            {
                sb.AppendLine($"                {{ 0x{location.Id:X16}, {SnakeToPascalCase(location.Key, culture)} }},");
            }

            sb.AppendLine($"            }};");
            sb.AppendLine($"        }}");
            sb.AppendLine($"    }}");

            if (!string.IsNullOrEmpty(opts.Namespace))
            {
                sb.AppendLine($"}}");
            }

            // write code to file

            if (opts.OutputPath == "-")
            {
                Console.Write(sb.ToString());
            }
            else
            {
                File.WriteAllText(opts.OutputPath, sb.ToString());
            }
        }

        static string Escape(string str)
        {
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t").Replace("\b", "\\b");
        }

        static string SnakeToPascalCase(string value, CultureInfo culture)
        {
            return string.Join("", value.Split("_").Select(x => Capitalize(x, culture)));
        }

        static string Capitalize(string text, CultureInfo culture)
        {
            return string.IsNullOrEmpty(text) ? text : char.ToUpper(text[0], culture) + text[1..].ToLower(culture);
        }
    }
}
