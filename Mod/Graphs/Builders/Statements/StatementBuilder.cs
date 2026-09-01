using NodeCanvas.DialogueTrees;

namespace OutwardArchipelago.Graphs.Builders.Statements
{
    internal class StatementBuilder : IStatementBuilder
    {
        public string LocalizationKey { get; set; }

        public GlobalAudioManager.Sounds Audio { get; set; } = GlobalAudioManager.Sounds.NONE;

        public Statement BuildStatement(IGraphPatchContext context)
        {
            return new Statement
            {
                text = OutwardArchipelagoMod.Instance.GetLocalizedModString(LocalizationKey),
                audio = Audio,
                meta = $"{OutwardArchipelagoMod.GUID}.{LocalizationKey}",
            };
        }
    }
}
