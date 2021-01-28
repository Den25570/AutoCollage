using System;
using System.Collections.Generic;
using System.Linq;
using CollageApp.State;
using System.Threading.Tasks;
using System.Drawing;

namespace CollageApp.State
{
    public class AppState
    {
        private const int maxSize = 200;
        private List<ActionData> actionsStack = new List<ActionData>();
        private Dictionary<DelegateEnum, Action<object[]>> delegates = new Dictionary<DelegateEnum, Action<object[]>>();

        private int revertedCount = 0;

        public AppState()
        {
       
            delegates.Add(DelegateEnum.LoadImages, AppState.LoadImages);
            delegates.Add(DelegateEnum.UnloadImages, AppState.UnloadImages);
            delegates.Add(DelegateEnum.ChangeFieldProperties, AppState.ChangeFieldProperties);
            delegates.Add(DelegateEnum.ChangeImagePos, AppState.ChangeImagePos);
            delegates.Add(DelegateEnum.ChangeImageProperties, AppState.ChangeImageProperties);
        }

        public void AddAction(DelegateEnum action, DelegateEnum undoAction, object[] actionParams, object[] undoActionParams)
        {
            if (!delegates.ContainsKey(action))
                throw new ArgumentException($"Delegate with id: {action} not registered.");

            actionsStack.RemoveRange(actionsStack.Count - revertedCount, revertedCount);
            revertedCount = 0;

            if (actionsStack.Count == maxSize)
                actionsStack.RemoveAt(0);
            actionsStack.Add(new ActionData(action, undoAction, actionParams, undoActionParams));
        }

        public void LoadAction()
        {
            if (revertedCount > 0)
            {
                ActionData lastAction = actionsStack.ElementAt(actionsStack.Count() - revertedCount--);

                Action<object[]> actionDel;
                if (delegates.TryGetValue(lastAction.Action, out actionDel))
                {
                    actionDel.Invoke(lastAction.ActionParams);
                }
                else
                    throw new Exception($"Delegate with id: {lastAction.Action} not registered.");
            }         
        }

        public void UndoAction()
        {
            if (actionsStack.Count - revertedCount > 0)
            {
                ActionData lastAction = actionsStack.ElementAt(actionsStack.Count() - 1 - revertedCount++);

                Action<object[]> actionDel;
                if (delegates.TryGetValue(lastAction.UndoAction, out actionDel))
                {
                    actionDel.Invoke(lastAction.UndoActionParams);
                }
                else
                    throw new Exception($"Delegate with id: {lastAction.UndoAction} not registered.");
            }            
        }

        public static void LoadImages(object[] objects)
        {
            MainForm form = (objects[0] as MainForm);
            string[] fulePaths = (objects[1] as string[]);

            form.LoadImages(fulePaths);
        }

        public static void UnloadImages(object[] objects)
        {
            MainForm form = (objects[0] as MainForm);

            form.UnloadImages();
        }

        public static void ChangeImagePos(object[] objects)
        {
            MainForm form = (objects[0] as MainForm);
            int index1 = (int)(objects[1]);
            int index2 = (int)(objects[2]);

            form.imageProcessor.SwapImages(index1, index2);
        }

        public static void ChangeImageProperties(object[] objects)
        {
            MainForm form = (objects[0] as MainForm);
            ImageInfo image = (objects[1] as ImageInfo);
            RectangleF srcRect = (RectangleF)objects[2];
            ImageFormatType imageFormatType = (ImageFormatType)objects[3];
            Boolean isHidden = (Boolean)objects[4];

            form.imageProcessor.ChangeImageProperties(image, srcRect, imageFormatType, isHidden);
        }
        public static void ChangeFieldProperties(object[] objects)
        {
            MainForm form = (objects[0] as MainForm);
            object[] properies = objects[1] as object[];

            form.template.ChangeTemplateProperties(properies);
        }
    }
}
