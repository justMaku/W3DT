using System;
using System.ComponentModel;
using System.Drawing;
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
        private static EventHandler _MapBuildDone;
        private static EventHandler _MapExportDone;
        private static EventHandler _MinimapTileDone;
        private static EventHandler _LoadingPrompt;
        private static EventHandler _MapExportDone2D;
        private static EventHandler _ModelViewerBackgroundChanged;

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

        public static event EventHandler MapBuildDone
        {
            add
            {
                TargetCheck(value.Target);
                _MapBuildDone = (EventHandler)Delegate.Combine(_MapBuildDone, value);
            }

            remove
            {
                _MapBuildDone = (EventHandler)Delegate.Remove(_MapBuildDone, value);
            }
        }

        public static event EventHandler MapExportDone
        {
            add
            {
                TargetCheck(value.Target);
                _MapExportDone = (EventHandler)Delegate.Combine(_MapExportDone, value);
            }

            remove
            {
                _MapExportDone = (EventHandler)Delegate.Remove(_MapExportDone, value);
            }
        }

        public static event EventHandler MinimapTileDone
        {
            add
            {
                TargetCheck(value.Target);
                _MinimapTileDone = (EventHandler)Delegate.Combine(_MinimapTileDone, value);
            }

            remove
            {
                _MinimapTileDone = (EventHandler)Delegate.Remove(_MinimapTileDone, value);
            }
        }

        public static event EventHandler LoadingPrompt
        {
            add
            {
                TargetCheck(value.Target);
                _LoadingPrompt = (EventHandler)Delegate.Combine(_LoadingPrompt, value);
            }

            remove
            {
                _LoadingPrompt = (EventHandler)Delegate.Remove(_LoadingPrompt, value);
            }
        }

        public static event EventHandler MapExportDone2D
        {
            add
            {
                TargetCheck(value.Target);
                _MapExportDone2D = (EventHandler)Delegate.Combine(_MapExportDone2D, value);
            }

            remove
            {
                _MapExportDone2D = (EventHandler)Delegate.Remove(_MapExportDone2D, value);
            }
        }

        public static event EventHandler ModelViewerBackgroundChanged
        {
            add
            {
                TargetCheck(value.Target);
                _ModelViewerBackgroundChanged = (EventHandler)Delegate.Combine(_ModelViewerBackgroundChanged, value);
            }

            remove
            {
                _ModelViewerBackgroundChanged = (EventHandler)Delegate.Remove(_ModelViewerBackgroundChanged, value);
            }
        }

        public static void Trigger_CDNScanDone(string bestHost, string hostPath)
        {
                TriggerEvent(_CDNScanDone, new CDNScanDoneArgs(bestHost, hostPath));
        }

        public static void Trigger_UpdateDownloadDone(bool success)
        {
            TriggerEvent(_UpdateDownloadDone, new UpdateDownloadDoneArgs(success));
        }

        public static void Trigger_UpdateCheckDone(LatestReleaseData data)
        {
            TriggerEvent(_UpdateCheckDone, new UpdateCheckDoneArgs(data));
        }

        public static void Trigger_LoadStepDone()
        {
            TriggerEvent(_LoadStepDone, new EventArgs());
        }

        public static void Trigger_CASCLoadDone(bool success)
        {
            Program.CASC_LOADING = false;
            TriggerEvent(_CASCLoadDone, new CASCLoadDoneArgs(success));
        }

        public static void Trigger_CASCLoadStart()
        {
            Program.CASC_LOADING = true;
            TriggerEvent(_CASCLoadStart, new EventArgs());
        }

        public static void Trigger_FileExploreHit(string id, CASC.CASCFile entry)
        {
            TriggerEvent(_FileExploreHit, new FileExploreHitArgs(id, entry));
        }

        public static void Trigger_FileExploreDone(string id)
        {
            TriggerEvent(_FileExploreDone, new FileExploreDoneArgs(id));
        }

        public static void Trigger_FileExtractComplete(bool success, int runnerID)
        {
            TriggerEvent(_FileExtractComplete, new FileExtractArgs(success, runnerID));
        }

        public static void Trigger_FileExtractComplete(CASC.CASCFile file, bool success, int runnerID)
        {
            TriggerEvent(_FileExtractComplete, new FileExtractCompleteArgs(file, success, runnerID));
        }

        public static void Trigger_FileExtractUnsafeComplete(string file, bool success, int runnerID)
        {
            TriggerEvent(_FileExtractComplete, new FileExtractCompleteUnsafeArgs(file, success, runnerID));
        }

        public static void Trigger_ExportBLPtoPNGComplete(bool success)
        {
            TriggerEvent(_ExportBLPtoPNGComplete, new ExportBLPtoPNGArgs(success));
        }

        public static void Trigger_MapBuildDone(System.Drawing.Bitmap bitmap)
        {
            TriggerEvent(_MapBuildDone, new MapBuildDoneArgs(bitmap));
        }

        public static void Trigger_MapExportDone(bool success, string error = null)
        {
            TriggerEvent(_MapExportDone, new MapExportDoneArgs(success, error));
        }

        public static void Trigger_MinimapTileDone(Runners.MapTileXY position, Runners.MapTileBounds bounds, string image, uint index)
        {
            TriggerEvent(_MinimapTileDone, new MinimapTileReadyArgs(position, bounds, image, index));
        }

        public static void Trigger_LoadingPrompt(string message)
        {
            TriggerEvent(_LoadingPrompt, new LoadingPromptArgs(message));
        }

        public static void Trigger_MapExportDone2D()
        {
            TriggerEvent(_MapExportDone2D, new EventArgs());
        }

        public static void Trigger_ModelViewerBackgroundChanged(Color colour)
        {
            TriggerEvent(_ModelViewerBackgroundChanged, new ModelViewerBackgroundChangedArgs(colour));
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
