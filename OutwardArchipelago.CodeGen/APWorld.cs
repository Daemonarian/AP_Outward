namespace OutwardArchipelago.CodeGen
{
    public class APWorld
    {
        public string ArchipelagoVersion { get; set; } = string.Empty;

        public string Game { get; set; } = string.Empty;

        public List<Item> Items { get; set; } = [];

        public List<Location> Locations { get; set; } = [];

        public class Item
        {
            public string Key { get; set; } = string.Empty;

            public long Id { get; set; } = 0;

            public string Name { get; set; } = string.Empty;
        }

        public class Location
        {
            public string Key { get; set; } = string.Empty;

            public long Id { get; set; } = 0;

            public string Name { get; set; } = string.Empty;
        }
    }
}
