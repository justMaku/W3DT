using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3DT._3D
{
    public class TextureBox : ITextureProvider
    {
        private Dictionary<int, string> fileMapping;
        private int index = 0;
        public int LastIndex { get; private set; }

        public TextureBox()
        {
            fileMapping = new Dictionary<int, string>();
        }

        public void addTexture(int extID, string file)
        {
            if (fileMapping.ContainsValue(file))
            {
                LastIndex = fileMapping.FirstOrDefault(m => m.Value == file).Key;
            }
            else
            {
                fileMapping.Add(index, file);
                LastIndex = index;
                index++;
            }
        }

        public string getFile(int extID)
        {
            return fileMapping.ContainsKey(extID) ? fileMapping[extID] : null;
        }

        public string getFile(uint extID)
        {
            return getFile((int)extID);
        }

        public void clear()
        {
            fileMapping.Clear();
        }
    }
}
