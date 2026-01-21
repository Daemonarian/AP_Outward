using NodeCanvas.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchOriginalNodeFactory : IDialoguePatchNodeFactory
    {
        public int OriginalNodeID { get; private set; }

        public DialoguePatchOriginalNodeFactory(int originalNodeID)
        {
            OriginalNodeID = originalNodeID;
        }

        public Node CreateNode(IDialoguePatchContext context)
        {
            return context.NodesByID[OriginalNodeID];
        }
    }
}
