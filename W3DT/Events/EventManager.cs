using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using W3DT.JSONContainers;

namespace W3DT.Events
{
    static class EventManager
    {
        private static EventHandler _CDNScanDone;
        private static EventHandler _UpdateDownloadDone;
        private static EventHandler _UpdateCheckDone;
        private static EventHandler _LoadStepDone;
        private static EventHandler _CASCLoadDone;
        private static EventHandler _CASCLoadStart;

        public static event EventHandler CDNScanDone
        {
            add
            {
                TargetCheck(value.Target);
                _CDNScanDone = (EventHandler)Delegate.Combine(_CDNScanDone, value);
            }

            remove
            {
                _CDNScanDone = (EventHandler)Delegate.Remove(_CDNScanDone, value);
            }
        }

        public static event EventHandler UpdateDownloadDone
        {
            add
            {
                TargetCheck(value.Target);
                _UpdateDownloadDone = (EventHandler)Delegate.Combine(_UpdateDownloadDone, value);
            }

            remove
            {
                _UpdateDownloadDone = (EventHandler)Delegate.Remove(_UpdateDownloadDone, value);
            }
        }

        public static event EventHandler UpdateCheckDone
        {
            add
            {
                TargetCheck(value.Target);
                _UpdateCheckDone = (EventHandler)Delegate.Combine(_UpdateCheckDone, value);
            }

            remove
            {
                _UpdateCheckDone = (EventHandler)Delegate.Remove(_UpdateCheckDone, value);
            }
        }

        public static event EventHandler LoadStepDone
        {
            add
            {
                TargetCheck(value.Target);
                _LoadStepDone = (EventHandler)Delegate.Combine(_LoadStepDone, value);
            }

            remove
            {
                _LoadStepDone = (EventHandler)Delegate.Remove(_LoadStepDone, value);
            }
        }

        public static event EventHandler CASCLoadDone
        {
            add
            {
                TargetCheck(value.Target);
                _CASCLoadDone = (EventHandler)Delegate.Combine(_CASCLoadDone, value);
            }

            remove
            {
                _CASCLoadDone = (EventHandler)Delegate.Remove(_CASCLoadDone, value);
            }
        }

        public static event EventHandler CASCLoadStart
        {
            add
            {
                TargetCheck(value.Target);
                _CASCLoadStart = (EventHandler)Delegate.Combine(_CASCLoadStart, value);
            }

            remove
            {
                _CASCLoadStart = (EventHandler)Delegate.Remove(_CASCLoadStart, value);
            }
        }

        public static void Trigger_CDNScanDone(CDNScanDoneArgs args)
        {
            TriggerEvent(_CDNScanDone.GetInvocationList(), args);
        }

        public static void Trigger_UpdateDownloadDone(UpdateDownloadDoneArgs args)
        {
            TriggerEvent(_UpdateDownloadDone.GetInvocationList(), args);
        }

        public static void Trigger_UpdateCheckDone(UpdateCheckDoneArgs args)
        {
            TriggerEvent(_UpdateCheckDone.GetInvocationList(), args);
        }

        public static void Trigger_LoadStepDone()
        {
            TriggerEvent(_LoadStepDone.GetInvocationList(), new EventArgs());
        }

        public static void Trigger_CASCLoadDone(CASCLoadDoneArgs args)
        {
            TriggerEvent(_CASCLoadDone.GetInvocationList(), args);
        }

        public static void Trigger_CASCLoadStart()
        {
            TriggerEvent(_CASCLoadStart.GetInvocationList(), new EventArgs());
        }

        private static void TriggerEvent(Delegate[] handlers, EventArgs args)
        {
            foreach (EventHandler handler in handlers)
            {
                var capture = handler;
                var syncObject = (ISynchronizeInvoke)handler.Target;
                syncObject.Invoke(
                    (Action)(() =>
                        {
                            capture(null, args);
                        }), null);
            }
        }

        private static void TargetCheck(object target)
        {
            if (!(target is ISynchronizeInvoke))
                throw new ArgumentException();
        }
    }
}
