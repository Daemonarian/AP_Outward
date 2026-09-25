using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.Connections;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema;

namespace Conflux.Schema.Context
{
    internal abstract class NodeCanvasGraphContext(ConfluxScript script) : INodeCanvasGraphContext
    {
        private readonly ConfluxScript _script = script;

        public ConfluxScript Script => _script;

        public BBParameter<T> BuildBBParameter<T>(string name)
        {
            if (Script.Blackboard.TryGetValue(name, out var id))
            {
                return new BBParameter<T>
                {
                    Name = name,
                    TargetVariableID = id,
                };
            }

            if (Script.LocalBlackboard.Variables.TryGetValue(name, out var variable))
            {
                return new BBParameter<T>
                {
                    Name = name,
                    TargetVariableID = variable.ID,
                };
            }

            throw new Exception($"Could not find blackboard variable named '{name}'.");
        }

        public abstract string GraphType { get; }

        public abstract Type NodeBaseType { get; }

        public abstract Type ConnectionBaseType { get; }

        public abstract Connection BuildConnection(Node source, Node target);

        public abstract Node BuildTerminalNode();

        public abstract Node BuildIfNode(ConditionTask condition);

        public abstract Node BuildDoNode(ActionTask action);
    }
}
