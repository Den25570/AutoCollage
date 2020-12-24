using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageApp.State
{
    public class ActionData
    {
        public DelegateEnum Action { get; private set; }
        public DelegateEnum UndoAction { get; private set; }
        public object[] ActionParams { get; private set; }
        public object[] UndoActionParams { get; private set; }

        public ActionData(DelegateEnum action, DelegateEnum undoAction, object[] actionParams, object[] undoActionParams)
        {
            Action = action;
            UndoAction = undoAction;
            ActionParams = actionParams;
            UndoActionParams = undoActionParams;
        }
    }
}
