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
        private static EventHandler _FileExploreHit;
        private static EventHandler _FileExploreDone;
        private static EventHandler _FileExtractComplete;
        private static EventHandler _ExportBLPtoPNGComplete;

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

        public static event EventHandler FileExploreHit
        {
            add
            {
                TargetCheck(value.Target);
                _FileExploreHit = (EventHandler)Delegate.Combine(_FileExploreHit, value);
            }

            remove
            {
                _FileExploreHit = (EventHandler)Delegate.Remove(_FileExploreHit, value);
            }
        }

        public static event EventHandler FileExploreDone
        {
            add
            {
                TargetCheck(value.Target);
                _FileExploreDone = (EventHandler)Delegate.Combine(_FileExploreDone, value);
            }

            remove
            {
                _FileExploreDone = (EventHandler)Delegate.Remove(_FileExploreDone, value);
            }
        }

        public static event EventHandler FileExtractComplete
        {
            add
            {
                TargetCheck(value.Target);
                _FileExtractComplete = (EventHandler)Delegate.Combine(_FileExtractComplete, value);
            }

            remove
            {
                _FileExtractComplete = (EventHandler)Delegate.Remove(_FileExtractComplete, value);
            }
        }

        public static event EventHandler ExportBLPtoPNGComplete
        {
            add
            {
                TargetCheck(value.Target);
                _ExportBLPtoPNGComplete = (EventHandler)Delegate.Combine(_ExportBLPtoPNGComplete, value);
            }

            remove
            {
                _ExportBLPtoPNGComplete = (EventHandler)Delegate.Remove(_ExportBLPtoPNGComplete, value);
            }
        }

        public static void Trigger_CDNScanDone(CDNScanDoneArgs args)
        {
                TriggerEvent(_CDNScanDone, args);
        }

        public static void Trigger_UpdateDownloadDone(UpdateDownloadDoneArgs args)
        {
            TriggerEvent(_UpdateDownloadDone, args);
        }

        public static void Trigger_UpdateCheckDone(UpdateCheckDoneArgs args)
        {
            TriggerEvent(_UpdateCheckDone, args);
        }

        public static void Trigger_LoadStepDone()
        {
            TriggerEvent(_LoadStepDone, new EventArgs());
        }

        public static void Trigger_CASCLoadDone(CASCLoadDoneArgs args)
        {
            Program.CASC_LOADING = false;
            TriggerEvent(_CASCLoadDone, args);
        }

        public static void Trigger_CASCLoadStart()
        {
            Program.CASC_LOADING = true;
            TriggerEvent(_CASCLoadStart, new EventArgs());
        }

        public static void Trigger_FileExploreHit(FileExploreHitArgs args)
        {
            TriggerEvent(_FileExploreHit, args);
        }

        public static void Trigger_FileExploreDone(FileExploreDoneArgs args)
        {
            TriggerEvent(_FileExploreDone, args);
        }

        public static void Trigger_FileExtractComplete(FileExtractArgs args)
        {
            TriggerEvent(_FileExtractComplete, args);
        }

        public static void Trigger_ExportBLPtoPNGComplete(ExportBLPtoPNGArgs args)
        {
            TriggerEvent(_ExportBLPtoPNGComplete, args);
        }

        private static void TriggerEvent(EventHandler handler, EventArgs args)
        {
            if (handler == null)
                return;

            foreach (EventHandler listener in handler.GetInvocationList())
            {
                try
                {
                    var capture = listener;
                    ISynchronizeInvoke sync;

                    if (listener.Target is Explorer)
                        sync = (ISynchronizeInvoke)((Explorer)listener.Target).target;
                    else
                        sync = (ISynchronizeInvoke)listener.Target;

                    sync.Invoke(
                        (Action)(() =>
                            {
                                capture(null, args);
                            }), null);
                }
                catch (InvalidOperationException)
                {
                    // Most likely caused by a hooked window being closed
                    // before a linked task was completed; ignore gracefully.
                }
            }
        }

        private static void TargetCheck(object target)
        {
            if (!(target is ISynchronizeInvoke) && !(target is Explorer))
                throw new ArgumentException();
        }
    }
}
