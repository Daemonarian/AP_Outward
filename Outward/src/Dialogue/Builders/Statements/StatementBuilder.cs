using NodeCanvas.DialogueTrees;

namespace OutwardArchipelago.Dialogue.Builders.Statements
{
    internal class StatementBuilder : IStatementBuilder
    {
        public string LocalizationKey { get; set; }

        public GlobalAudioManager.Sounds Audio { get; set; } = GlobalAudioManager.Sounds.NONE;

        public Statement BuildStatement(IDialoguePatchContext context)
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
