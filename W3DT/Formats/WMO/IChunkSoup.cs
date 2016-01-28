using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.WMO
{
    interface IChunkSoup
    {
        /*
         * Chunks that implement this interface will be fed into the MOGP chunk which was
         * previously found within the group file. This should not be implemented by chunks
         * located in WMO root files.
         * 
         * If no MOGP chunk was located prior to this chunk, this chunk will be discarded as
         * this probably means something went wrong.
         * 
         * Expected chunks to use this: MOPY, MOVI, MOVT, MONR, MOTV, MOBA, MOLR, MODR, MOBN, MOBR,
         *                              MOCV, MLIQ, MORI, MORB, MOTA, MOBS, MOPL.
         */
    }
}
