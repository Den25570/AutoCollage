using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageApp.State
{
    public class AppState
    {
        private const int maxSize = 200;
        private List<ActionData> actionsStack = new List<ActionData>();
        private Dictionary<int, Delegate> delegates;

        private int revertedCount = 0;

        public void AddDelegate(int id, Delegate del)
        {
            delegates.Add(id, del);
        }

        public void AddAction(int id, int undoId, object[] actionParams, object[] undoActionParams)
        {
            if (!delegates.ContainsKey(id))
                throw new ArgumentException($"Delegate with id: {id} not resigtered.");

            actionsStack.RemoveRange(actionsStack.Count - revertedCount, revertedCount);
            revertedCount = 0;

            if (actionsStack.Count == maxSize)
                actionsStack.RemoveAt(0);
            actionsStack.Add(new ActionData(id, undoId, actionParams, undoActionParams));
        }

        public void LoadAction()
        {
            if (revertedCount > 0)
            {
                ActionData lastAction = actionsStack.ElementAt(actionsStack.Count() - revertedCount--);

                Delegate actionDel;
                if (delegates.TryGetValue(lastAction.ActionId, out actionDel))
                {
                    actionDel.DynamicInvoke(lastAction.ActionParams);
                }
                else
                    throw new Exception($"Delegate with id: {lastAction.ActionId} not resigtered.");
            }         
        }

        public void UndoAction()
        {
            ActionData lastAction = actionsStack.ElementAt(actionsStack.Count() - 1 - revertedCount++);

            Delegate actionDel;
            if (delegates.TryGetValue(lastAction.UndoActionId, out actionDel))
            {
                actionDel.DynamicInvoke(lastAction.UndoActionParams);
            }
            else
                throw new Exception($"Delegate with id: {lastAction.UndoActionId} not resigtered.");
        }
    }
}
