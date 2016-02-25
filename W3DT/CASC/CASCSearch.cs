using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3DT.CASC
{
    public class CASCSearch
    {
        public enum SearchType
        {
            COMPLETE,
            STARTS_WITH,
            ENDS_WITH
        }

        public static List<CASCFile> Search(string file, SearchType type = SearchType.COMPLETE)
        {
            List<CASCFile> found = new List<CASCFile>();

            if (Program.IsCASCReady)
                Explore(Program.Root, file.ToLower(), type, found);

            return found;
        }

        private static void Explore(CASCFolder folder, string findFile, SearchType type, List<CASCFile> found)
        {
            foreach (KeyValuePair<string, ICASCEntry> node in folder.Entries)
            {
                ICASCEntry entry = node.Value;

                if (entry is CASCFolder)
                {
                    Explore((CASCFolder)entry, findFile, type, found);
                }
                else if (entry is CASCFile)
                {
                    CASCFile file = (CASCFile)entry;
                    string fileName = file.FullName.ToLower();

                    if (type == SearchType.COMPLETE && fileName.Equals(fileName))
                    {
                        found.Add(file);
                        break;
                    }

                    if (type == SearchType.STARTS_WITH && fileName.StartsWith(findFile))
                    {
                        found.Add(file);
                        continue;
                    }

                    if (type == SearchType.ENDS_WITH && fileName.EndsWith(findFile))
                    {
                        found.Add(file);
                        continue;
                    }
                }
            }
        }
    }
}
