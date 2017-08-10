using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using SharpGL;
using SereniaBLPLib;

namespace W3DT._3D
{
    public class TextureManager : ITextureProvider
    {
        private OpenGL gl;
        private Dictionary<int, uint[]> mapping;
        private Dictionary<int, string> fileMapping;

        public TextureManager(OpenGL gl)
        {
            this.gl = gl;
            mapping = new Dictionary<int, uint[]>();
            fileMapping = new Dictionary<int, string>();
        }

        public void addTexture(int extID, string file)
        {
            int width = 0;
            int height = 0;
            byte[] data = null;

            fileMapping.Add(extID, file);

            using (BlpFile raw = new BlpFile(File.OpenRead(file)))
            {
                Bitmap bitmap = raw.GetBitmap(0);
                width = bitmap.Width;
                height = bitmap.Height;

                int length = width * height * 4;
                var rawData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                data = new byte[length];

                Marshal.Copy(rawData.Scan0, data, 0, length);
                bitmap.UnlockBits(rawData);
                bitmap.Dispose();
            }

            uint[] intID = new uint[1];
            gl.GenTextures(1, intID);

            gl.BindTexture(OpenGL.GL_TEXTURE_2D, intID[0]);
            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGBA, width, height, 0, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, data);

            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR_MIPMAP_LINEAR);

            mapping.Add(extID, intID);
            gl.GenerateMipmapEXT(OpenGL.GL_TEXTURE_2D);

            Log.Write("TextureManager: {0} assigned to GL with @ {1}", file, intID[0]);
        }

        public uint getTexture(int extID)
        {
            return mapping.ContainsKey(extID) ? mapping[extID][0] : 0;
        }

        public int getID(uint textureID)
        {
            foreach (KeyValuePair<int, uint[]> node in mapping)
                if (node.Value[0] == textureID)
                    return node.Key;

            return -1;
        }

        public string getFile(uint extID)
        {
            return getFile(getID(extID));
        }

        public string getFile(int extID)
        {
            return fileMapping.ContainsKey(extID) ? fileMapping[extID] : null;
        }

        public void clear()
        {
            foreach (uint[] intID in mapping.Values)
                gl.DeleteTextures(1, intID);

            mapping.Clear();
            fileMapping.Clear();
        }
    }
}
