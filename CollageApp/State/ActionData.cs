using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageApp.State
{
    public class ActionData
    {
        public int ActionId { get; private set; }
        public int UndoActionId { get; private set; }
        public object[] ActionParams { get; private set; }
        public object[] UndoActionParams { get; private set; }

        public ActionData(int actionId, int undoActionId, object[] actionParams, object[] undoActionParams)
        {
            ActionId = actionId;
            UndoActionId = undoActionId;
            ActionParams = actionParams;
            UndoActionParams = undoActionParams;
        }
    }
}
