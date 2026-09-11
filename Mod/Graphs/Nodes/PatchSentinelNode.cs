using System;
using NodeCanvas.Framework;
using ParadoxNotion;

namespace OutwardArchipelago.Graphs.Nodes
{
    internal class PatchSentinelNode : Node
    {
        public override int maxInConnections => -1;

        public override int maxOutConnections => 0;

        public override Type outConnectionType => typeof(Connection);

        public override bool allowAsPrime => false;

        public override Alignment2x2 commentsAlignment => Alignment2x2.Right;

        public override Alignment2x2 iconAlignment => Alignment2x2.Bottom;
    }
}
