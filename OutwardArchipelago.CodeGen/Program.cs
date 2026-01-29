using System.Globalization;
using System.Text;
using System.Text.Json;
using CommandLine;

namespace OutwardArchipelago.CodeGen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(GenerateCode);
        }

        static void GenerateCode(CommandLineOptions opts)
        {
            // parse the Archipelgo version

            var archipelagoVersionFile = Path.Join(AppContext.BaseDirectory, "archipelago_version.txt");
            var archipelagoVersion = File.ReadAllText(archipelagoVersionFile).Trim();

            // parse the APWorld item ids

#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances

            var apworldIdsJsonFile = Path.Join(AppContext.BaseDirectory, "apworld_ids.json");
            var jsonText = File.ReadAllText(apworldIdsJsonFile);
            var apworldIds = JsonSerializer.Deserialize<APWorldIDs>(jsonText, options) ?? throw new Exception("Failed to load the APWorld IDs.");

            // convert keys into PascalCase for C#

            CultureInfo keyCulture = CultureInfo.InvariantCulture;
            apworldIds.Items = ConvertKeysSnakeToPascalCase(apworldIds.Items, keyCulture);
            apworldIds.Locations = ConvertKeysSnakeToPascalCase(apworldIds.Locations, keyCulture);

            // generate code

            var sb = new StringBuilder();

            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("");

            if (!string.IsNullOrEmpty(opts.Namespace))
            {
                sb.AppendLine($"namespace {opts.Namespace}");
                sb.AppendLine("{");
            }

            sb.AppendLine($"    {opts.AccessModifier} class {opts.InfoClassName}");
            sb.AppendLine("    {");
            sb.AppendLine($"        public const string ArchipelagoVersion = \"{archipelagoVersion}\";");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine($"    {opts.AccessModifier} class {opts.ItemClassName}");
            sb.AppendLine("    {");

            foreach (var pair in apworldIds.Items)
            {
                sb.AppendLine($"        {opts.AccessModifier} const long {pair.Key} = 0x{pair.Value:X16};");
            }

            sb.AppendLine("");
            sb.AppendLine($"        {opts.AccessModifier} static readonly IReadOnlyList<long> All = new[]");
            sb.AppendLine("        {");

            foreach (var key in apworldIds.Items.Keys)
            {
                sb.AppendLine($"            {key},");
            }

            sb.AppendLine("        };");
            sb.AppendLine("    }");
            sb.AppendLine("");
            sb.AppendLine($"    {opts.AccessModifier} class {opts.LocationClassName}");
            sb.AppendLine("    {");

            foreach (var pair in apworldIds.Locations)
            {
                sb.AppendLine($"        {opts.AccessModifier} const long {pair.Key} = 0x{pair.Value:X16};");
            }

            sb.AppendLine("");
            sb.AppendLine($"        {opts.AccessModifier} static readonly IReadOnlyList<long> All = new[]");
            sb.AppendLine("        {");

            foreach (var key in apworldIds.Locations.Keys)
            {
                sb.AppendLine($"            {key},");
            }

            sb.AppendLine("        };");
            sb.AppendLine("    }");

            if (!string.IsNullOrEmpty(opts.Namespace))
            {
                sb.AppendLine("}");
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

        static Dictionary<string, T> ConvertKeysSnakeToPascalCase<T>(Dictionary<string, T> source, CultureInfo culture)
        {
            return source.ToDictionary(pair => SnakeToPascalCase(pair.Key, culture), pair => pair.Value);
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
