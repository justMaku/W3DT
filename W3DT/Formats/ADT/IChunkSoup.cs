using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT.Formats.ADT
{
    interface IChunkSoup
    {
        /*
         * Chunks that implement this interface will be fed into the MCNK chunk which was
         * previously found within the file. 
         * 
         * If no MCNK chunk was located prior to this chunk, this chunk will be discarded as
         * this probably means something went wrong.
         * 
         * Expected chunks to use this: MCVT, MCLV, MCCV, MCNR, MCLY, MCRD, MCRW, MCSH, MCAL
         *                              MCLQ, MCSE, MCBB, MCMT, MCDD
         */
    }
}
