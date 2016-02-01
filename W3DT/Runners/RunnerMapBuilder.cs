using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using W3DT.Events;

namespace W3DT.Runners
{
    public class RunnerMapBuilder : RunnerBase
    {
        private string[] files;

        public RunnerMapBuilder(string[] files)
        {
            this.files = files;
        }

        public override void Work()
        {
            /*
             * ToDo:
             * - Go through each of the files given, construct map.
             * - Sort maps.
             * - Extract map tiles.
             * - Load every tile as BLP.
             * - Stich tiles together on canvas.
             */

            // ToDo: Actually pass a bitmap back here.
            EventManager.Trigger_MapBuildDone(null);
        }
    }
}
