from common import to_csharp_string_literal, outward_location_file, outward_item_file, write_code_file
from schema import load_data

data_schema = load_data()

write_code_file(outward_location_file, f"""
using System.Collections.Generic;
using System.Linq;

namespace OutwardArchipelago.Archipelago.Data
{{
    internal class ArchipelagoLocationData
    {{
        {'\n        '.join(f"public static readonly ArchipelagoLocationData {location.key.pascal} = new({location.code}, {to_csharp_string_literal(location.name)});" for location in sorted(data_schema.locations, key=lambda x: x.code))}

        public static readonly IReadOnlyDictionary<long, ArchipelagoLocationData> ByID = new ArchipelagoLocationData[] {{
            {'\n            '.join(f'{location.key.pascal},' for location in sorted(data_schema.locations, key=lambda x: x.key.pascal))}
        }}.ToDictionary((location) => location.ID);

        public long ID {{ get; private set; }}

        public string Name {{ get; private set; }}

        private ArchipelagoLocationData(long id, string name)
        {{
            ID = id;
            Name = name;
        }}

        public override string ToString() => $"Location: {{Name}} ({{ID}})";
    }}
}}
""")

write_code_file(outward_item_file, f"""
using System.Collections.Generic;
using System.Linq;

namespace OutwardArchipelago.Archipelago.Data
{{
    internal class ArchipelagoItemData
    {{
        {'\n        '.join(f"public static readonly ArchipelagoItemData {item.key.pascal} = new({item.code}, {to_csharp_string_literal(item.name)}, {item.outward.generate_giver()});" for item in sorted(data_schema.items, key=lambda x: x.code))}

        public static readonly IReadOnlyDictionary<long, ArchipelagoItemData> ByID = new ArchipelagoItemData[] {{
            {'\n            '.join(f'{item.key.pascal},' for item in sorted(data_schema.items, key=lambda x: x.key.pascal))}
        }}.ToDictionary((item) => item.ID);

        public long ID {{ get; private set; }}

        public string Name {{ get; private set; }}

        public IOutwardGiver Giver {{ get; private set; }}

        private ArchipelagoItemData(long id, string name, IOutwardGiver giver)
        {{
            ID = id;
            Name = name;
            Giver = giver;
        }}

        public override string ToString() => $"Item: {{Name}} ({{ID}})";
    }}
}}
""")
