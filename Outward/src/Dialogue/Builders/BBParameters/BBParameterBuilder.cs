using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue.Builders.BBParameters
{
    internal abstract class BBParameterBuilder
    {
        public static IBBParameterBuilder<Character> Instigator => new BBParameterBuilder<Character> { Name = "gInstigator", TargetVariableID = "4677abc8-6894-4a7e-b0ee-d72b68ca9559" };

        public static IBBParameterBuilder<T> FromValue<T>(T value) => new BBParameterBuilder<T> { Value = value };
    }

    internal class BBParameterBuilder<T> : IBBParameterBuilder<T>
    {
        public string Name { get; set; }

        public string TargetVariableID { get; set; }

        public T Value { get; set; }

        public BBParameter<T> BuildBBParameter(IDialoguePatchContext context)
        {
            return new BBParameter<T>
            {
                _name = Name,
                _targetVariableID = TargetVariableID,
                _value = Value,
            };
        }
    }
}
