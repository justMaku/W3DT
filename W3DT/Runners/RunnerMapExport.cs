using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using W3DT.Events;
using W3DT.Formats;
using W3DT.Formats.ADT;
using W3DT._3D;
using W3DT.CASC;
using SereniaBLPLib;

namespace W3DT.Runners
{
    public class MapExportException : Exception
    {
        public MapExportException(string message) : base(message) { }
    }

    public class RunnerMapExport : RunnerBase
    {
        private string mapName;
        private string fileName;
        private string filePath;
        private List<Point> includeOnly;
        private Bitmap blank;

        public RunnerMapExport(string mapName, string fileName, List<Point> includeOnly = null)
        {
            this.mapName = mapName;
            this.fileName = fileName;
            this.filePath = Path.GetDirectoryName(fileName);
            this.includeOnly = includeOnly;

            // Blank texture to default back to.
            blank = new Bitmap(256, 256);
            using (Graphics g = Graphics.FromImage(blank))
                g.FillRectangle(Brushes.White, 0, 0, 256, 256);
        }

        private bool ShouldInclude(int x, int y)
        {
            if (includeOnly == null || includeOnly.Count == 0)
                return true;

            return includeOnly.Contains(new Point(x, y));
        }

        public override void Work()
        {
            LogWrite("Beginning export of {0}...", mapName);

            TextureBox texProvider = new TextureBox();
            WaveFrontWriter ob = new WaveFrontWriter(fileName, texProvider, true, true, false);

            try
            {
                if (!Program.IsCASCReady)
                    throw new MapExportException("CASC file engine is not loaded.");

                string wdtPath = string.Format(@"World\Maps\{0}\{0}.wdt", mapName);
                Program.CASCEngine.SaveFileTo(wdtPath, Constants.TEMP_DIRECTORY);

                WDTFile headerFile = new WDTFile(Path.Combine(Constants.TEMP_DIRECTORY, wdtPath));
                headerFile.parse();

                if (!headerFile.Chunks.Any(c => c.ChunkID == Formats.WDT.Chunk_MPHD.Magic))
                    throw new MapExportException("Invalid map header (WDT)");

                // ToDo: Check if world WMO exists and load it.

                // Ensure we have a MAIN chunk before trying to process terrain.
                Formats.WDT.Chunk_MAIN mainChunk = (Formats.WDT.Chunk_MAIN)headerFile.Chunks.Where(c => c.ChunkID == Formats.WDT.Chunk_MAIN.Magic).FirstOrDefault();
                Formats.WDT.Chunk_MPHD headerChunk = (Formats.WDT.Chunk_MPHD)headerFile.Chunks.Where(c => c.ChunkID == Formats.WDT.Chunk_MPHD.Magic).FirstOrDefault();

                if (mainChunk != null && headerChunk != null)
                {
                    // Pre-calculate UV mapping for terrain.
                    UV[,,] uvMaps = new UV[8,8,5];
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            float uTL = 1 - (0.125f * x);
                            float vTL = 1 - (0.125f * y);
                            float uBR = uTL - 0.125f;
                            float vBR = vTL - 0.125f;

                            int aX = 7 - x;
                            uvMaps[aX, y, 0] = new UV(uBR, vTL); // TL
                            uvMaps[aX, y, 1] = new UV(uBR, vBR); // TR
                            uvMaps[aX, y, 2] = new UV(uTL, vTL); // BL
                            uvMaps[aX, y, 3] = new UV(uTL, vBR); // BR
                            uvMaps[aX, y, 4] = new UV(uTL - 0.0625f, vTL - 0.0625f);
                        }
                    }

                    int meshIndex = 1;
                    int wmoIndex = 1;

                    // Create a directory for map data (alpha maps, etc).
                    string dataDirRaw = string.Format("{0}.data", Path.GetFileNameWithoutExtension(fileName));
                    string dataDir = Path.Combine(Path.GetDirectoryName(fileName), dataDirRaw);

                    if (!Directory.Exists(dataDir))
                        Directory.CreateDirectory(dataDir);

                    Dictionary<byte[], uint> texCache = new Dictionary<byte[], uint>(new ByteArrayComparer());

                    for (int y = 0; y < 64; y++)
                    {
                        for (int x = 0; x < 64; x++)
                        {
                            if (mainChunk.map[x, y] && ShouldInclude(x, y))
                            {
                                string pathBase = string.Format(@"World\Maps\{0}\{0}_{1}_{2}", mapName, x, y);
                                string adtPath = pathBase + ".adt";
                                string texPath = pathBase + "_tex0.adt";
                                string objPath = pathBase + "_obj0.adt";

                                Program.CASCEngine.SaveFileTo(adtPath, Constants.TEMP_DIRECTORY);
                                Program.CASCEngine.SaveFileTo(texPath, Constants.TEMP_DIRECTORY);
                                Program.CASCEngine.SaveFileTo(objPath, Constants.TEMP_DIRECTORY);

                                string adtTempPath = Path.Combine(Constants.TEMP_DIRECTORY, adtPath);
                                string texTempPath = Path.Combine(Constants.TEMP_DIRECTORY, texPath);
                                string objTempPath = Path.Combine(Constants.TEMP_DIRECTORY, objPath);

                                try
                                {
                                    ADTFile adt = new ADTFile(adtTempPath, ADTFileType.ROOT);
                                    adt.parse();

                                    ADTFile tex = new ADTFile(texTempPath, ADTFileType.TEX);
                                    tex.parse();

                                    ADTFile obj = new ADTFile(objTempPath, ADTFileType.OBJ);
                                    obj.parse();

                                    // Textures
                                    Chunk_MTEX texChunk = (Chunk_MTEX)tex.getChunk(Chunk_MTEX.Magic);

                                    uint texIndex = 0;
                                    Bitmap[] textureData = new Bitmap[texChunk.textures.count()];
                                    foreach (KeyValuePair<int, string> texture in texChunk.textures.raw())
                                    {
                                        string texFile = texture.Value;
                                        string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, texFile);

                                        if (!File.Exists(tempPath))
                                            Program.CASCEngine.SaveFileTo(texFile, Constants.TEMP_DIRECTORY);

                                        using (BlpFile blp = new BlpFile(File.OpenRead(tempPath)))
                                            textureData[texIndex] = blp.GetBitmap(0);

                                        texIndex++;
                                    }

                                    Chunk_MCNK[] soupChunks = adt.getChunksByID(Chunk_MCNK.Magic).Cast<Chunk_MCNK>().ToArray();
                                    Chunk_MCNK[] layerChunks = tex.getChunksByID(Chunk_MCNK.Magic).Cast<Chunk_MCNK>().ToArray();

                                    // Terrain
                                    for (int i = 0; i < 256; i++)
                                    {
                                        Chunk_MCNK soupChunk = soupChunks[i];
                                        Chunk_MCNK layerChunk = layerChunks[i];

                                        // Terrain chunks
                                        Chunk_MCVT hmChunk = (Chunk_MCVT)soupChunk.getChunk(Chunk_MCVT.Magic);
                                        Chunk_MCNR nChunk = (Chunk_MCNR)soupChunk.getChunk(Chunk_MCNR.Magic);

                                        // Texture chunks
                                        Chunk_MCLY layers = (Chunk_MCLY)layerChunk.getChunk(Chunk_MCLY.Magic);

                                        // Alpha mapping
                                        Chunk_MCAL alphaMapChunk = (Chunk_MCAL)layerChunk.getChunk(Chunk_MCAL.Magic, false);

                                        string texFileName = string.Format("baked_{0}_{1}_{2}.png", x, y, i);
                                        string texFilePath = Path.Combine(dataDir, texFileName);

                                        EventManager.Trigger_LoadingPrompt(string.Format("Rendering tile {0} at {1},{2}...", i + 1, x, y));
                                        uint texFaceIndex = 0;

                                        if (!File.Exists(texFilePath))
                                        {
                                            Bitmap bmpBase = new Bitmap(layers.layers.Length > 0 ? textureData[layers.layers[0].textureID] : blank);
                                            using (Graphics baseG = Graphics.FromImage(bmpBase))
                                            using (ImageAttributes att = new ImageAttributes())
                                            {
                                                att.SetWrapMode(WrapMode.TileFlipXY);
                                                baseG.CompositingQuality = CompositingQuality.HighQuality;
                                                baseG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                                baseG.CompositingMode = CompositingMode.SourceOver;

                                                for (int mI = 1; mI < layers.layers.Length; mI++) // First layer never has an alpha map
                                                {
                                                    byte[,] alphaMap;
                                                    MCLYLayer layer = layers.layers[mI];
                                                    bool headerFlagSet = ((headerChunk.flags & 0x4) == 0x4) || ((headerChunk.flags & 0x80) == 0x80);
                                                    bool layerFlagSet = ((layer.flags & 0x200) == 0x200);
                                                    bool fixAlphaMap = !((soupChunk.flags & 0x200) == 0x200);

                                                    if (layerFlagSet)
                                                        alphaMap = alphaMapChunk.parse(Chunk_MCAL.CompressType.COMPRESSED, layer.ofsMCAL, fixAlphaMap);
                                                    else
                                                        alphaMap = alphaMapChunk.parse(headerFlagSet ? Chunk_MCAL.CompressType.UNCOMPRESSED_4096 : Chunk_MCAL.CompressType.UNCOMPRESSED_2048, layer.ofsMCAL, fixAlphaMap);

                                                    Bitmap bmpRawTex = textureData[layer.textureID];
                                                    Bitmap bmpAlphaMap = new Bitmap(64, 64);
                                                    for (int drawX = 0; drawX < 64; drawX++)
                                                        for (int drawY = 0; drawY < 64; drawY++)
                                                            bmpAlphaMap.SetPixel(drawX, drawY, Color.FromArgb(alphaMap[drawX, drawY], 0, 0, 0));

                                                    Bitmap bmpAlphaMapScaled = new Bitmap(bmpRawTex.Width, bmpRawTex.Height);
                                                    using (Graphics g = Graphics.FromImage(bmpAlphaMapScaled))
                                                    {
                                                        g.CompositingQuality = CompositingQuality.HighQuality;
                                                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                                        g.CompositingMode = CompositingMode.SourceCopy;
                                                        g.DrawImage(bmpAlphaMap, new Rectangle(0, 0, bmpAlphaMapScaled.Width, bmpAlphaMapScaled.Height), 0, 0, bmpAlphaMap.Width, bmpAlphaMap.Height, GraphicsUnit.Pixel, att);
                                                    }
                                                    bmpAlphaMap.Dispose();

                                                    Bitmap bmpTex = new Bitmap(bmpRawTex.Width, bmpRawTex.Height);
                                                    for (int drawX = 0; drawX < bmpRawTex.Width; drawX++)
                                                    {
                                                        for (int drawY = 0; drawY < bmpRawTex.Height; drawY++)
                                                        {
                                                            // Hacky fix to flip the texture.
                                                            // Remove this if we fix the terrain read-order.
                                                            int sourceX = (bmpRawTex.Width - 1) - drawX;
                                                            //int sourceY = (bmpRawTex.Height - 1) - drawY;

                                                            bmpTex.SetPixel(sourceX, drawY, Color.FromArgb(
                                                                bmpAlphaMapScaled.GetPixel(drawX, drawY).A,
                                                                bmpRawTex.GetPixel(drawX, drawY).R,
                                                                bmpRawTex.GetPixel(drawX, drawY).G,
                                                                bmpRawTex.GetPixel(drawX, drawY).B
                                                            ));
                                                        }
                                                    }
                                                    bmpAlphaMapScaled.Dispose();
                                                    baseG.DrawImage(bmpTex, 0, 0, bmpBase.Width, bmpBase.Height);
                                                }

                                                using (MemoryStream str = new MemoryStream())
                                                using (MD5 md5 = MD5.Create())
                                                {
                                                    bmpBase.Save(str, ImageFormat.Png);
                                                    byte[] raw = md5.ComputeHash(str.ToArray());
                                                    uint cacheID;

                                                    if (texCache.TryGetValue(raw, out cacheID))
                                                    {
                                                        // Cache found, use that instead.
                                                        texFaceIndex = cacheID;
                                                    }
                                                    else
                                                    {
                                                        // No cache found, store new.
                                                        bmpBase.Save(texFilePath);
                                                        texProvider.addTexture(-1, Path.Combine(dataDirRaw, texFileName));
                                                        cacheID = (uint)texProvider.LastIndex;

                                                        texFaceIndex = cacheID;
                                                        texCache.Add(raw, cacheID);
                                                    }
                                                }
                                                bmpBase.Dispose();
                                            }
                                        }

                                        Mesh mesh = new Mesh("Terrain Mesh #" + meshIndex);
                                        meshIndex++;

                                        int v = 0;

                                        float pX = soupChunk.position.X;
                                        float pY = -soupChunk.position.Y;
                                        float pZ = soupChunk.position.Z;

                                        int ofs = 10;
                                        for (int sX = 8; sX > 0; sX--)
                                        {
                                            for (int sY = 1; sY < 9; sY++)
                                            {
                                                int cIndex = ofs - 1;
                                                int blIndex = cIndex - 9;
                                                int tlIndex = cIndex + 8;

                                                float tr = hmChunk.vertices[tlIndex + 1];
                                                float tl = hmChunk.vertices[tlIndex];
                                                float br = hmChunk.vertices[blIndex + 1];
                                                float bl = hmChunk.vertices[blIndex];
                                                float c = hmChunk.vertices[cIndex];

                                                float oX = pX + (sX * ADTFile.TILE_SIZE);
                                                float oY = pY + (sY * ADTFile.TILE_SIZE);

                                                // Apply UV mapping
                                                for (int uv = 0; uv < 5; uv++)
                                                    mesh.addUV(uvMaps[sX - 1, sY - 1, uv]);

                                                // Apply verts
                                                mesh.addVert(new Position(oX, tl + pZ, oY));
                                                mesh.addVert(new Position(oX, tr + pZ, oY + ADTFile.TILE_SIZE));
                                                mesh.addVert(new Position(oX + ADTFile.TILE_SIZE, bl + pZ, oY));
                                                mesh.addVert(new Position(oX + ADTFile.TILE_SIZE, br + pZ, oY + ADTFile.TILE_SIZE));
                                                mesh.addVert(new Position(oX + (ADTFile.TILE_SIZE / 2), c + pZ, oY + (ADTFile.TILE_SIZE / 2)));

                                                // Normals
                                                mesh.addNormal(nChunk.normals[tlIndex]);
                                                mesh.addNormal(nChunk.normals[tlIndex + 1]);
                                                mesh.addNormal(nChunk.normals[blIndex]);
                                                mesh.addNormal(nChunk.normals[blIndex + 1]);
                                                mesh.addNormal(nChunk.normals[cIndex]);

                                                // Faces
                                                mesh.addFace(texFaceIndex, v, v + 2, v + 4);
                                                mesh.addFace(texFaceIndex, v + 1, v + 3, v + 4);
                                                mesh.addFace(texFaceIndex, v, v + 1, v + 4);
                                                mesh.addFace(texFaceIndex, v + 2, v + 3, v + 4);

                                                v += 5;
                                                ofs += 1;
                                            }
                                            ofs += 9;
                                        }

                                        ob.addMesh(mesh);
                                    }

                                    // Parse WMO objects that appear in the world.
                                    EventManager.Trigger_LoadingPrompt("Constructing buildings...");

                                    Chunk_MWID wmoIndexChunk = (Chunk_MWID)obj.getChunk(Chunk_MWID.Magic, false);
                                    Chunk_MWMO wmoModelChunk = (Chunk_MWMO)obj.getChunk(Chunk_MWMO.Magic, false);
                                    Chunk_MODF wmoRefChunk = (Chunk_MODF)obj.getChunk(Chunk_MODF.Magic, false);

                                    if (wmoIndexChunk != null && wmoModelChunk != null && wmoRefChunk != null)
                                    {
                                        foreach (Chunk_MODF.MODFEntry entry in wmoRefChunk.entries)
                                        {
                                            string wmoModel = wmoModelChunk.objects.get((int)wmoIndexChunk.offsets[entry.entry]);
                                            List<CASCFile> groupSearch = CASCSearch.Search(Path.Combine(Path.GetDirectoryName(wmoModel), Path.GetFileNameWithoutExtension(wmoModel)), CASCSearch.SearchType.STARTS_WITH);

                                            if (groupSearch.Count > 0)
                                            {
                                                // Set-up root/group files for the WMO.
                                                WMOFile wmo = null;
                                                List<WMOFile> groupFiles = new List<WMOFile>(groupSearch.Count - 1);
                                                string rootName = Path.GetFileName(wmoModel).ToLower();

                                                foreach (CASCFile file in groupSearch)
                                                {
                                                    Program.CASCEngine.SaveFileTo(file.FullName, Constants.TEMP_DIRECTORY);
                                                    string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, file.FullName);

                                                    if (file.FullName.ToLower().EndsWith(rootName))
                                                        wmo = new WMOFile(tempPath, true);
                                                    else
                                                        groupFiles.Add(new WMOFile(tempPath, false));
                                                }

                                                foreach (WMOFile groupFile in groupFiles)
                                                    wmo.addGroupFile(groupFile);

                                                groupFiles.Clear();
                                                wmo.parse();

                                                // Export/register textures needed for this WMO.
                                                Formats.WMO.Chunk_MOTX wmoTexChunk = (Formats.WMO.Chunk_MOTX)wmo.getChunk(Formats.WMO.Chunk_MOTX.Magic);
                                                Dictionary<int, int> wmoTexMap = new Dictionary<int, int>();

                                                foreach (KeyValuePair<int, string> node in wmoTexChunk.textures.raw())
                                                {
                                                    string tempPath = Path.Combine(Constants.TEMP_DIRECTORY, node.Value);
                                                    string dumpPath = Path.Combine(Path.GetDirectoryName(node.Value), Path.GetFileNameWithoutExtension(node.Value) + ".png");

                                                    // Extract
                                                    if (!File.Exists(tempPath))
                                                        Program.CASCEngine.SaveFileTo(node.Value, Constants.TEMP_DIRECTORY);

                                                    // Convert
                                                    using (BlpFile blp = new BlpFile(File.OpenRead(tempPath)))
                                                    using (Bitmap bmp = blp.GetBitmap(0))
                                                    {
                                                        string dumpLoc = Path.Combine(dataDir, dumpPath);
                                                        Directory.CreateDirectory(Path.GetDirectoryName(dumpLoc));
                                                        bmp.Save(dumpLoc);
                                                    }

                                                    // Register
                                                    texProvider.addTexture(-1, Path.Combine(dataDirRaw, dumpPath));
                                                    wmoTexMap.Add(node.Key, texProvider.LastIndex);
                                                }

                                                Formats.WMO.Chunk_MOGN wmoNameChunk = (Formats.WMO.Chunk_MOGN)wmo.getChunk(Formats.WMO.Chunk_MOGN.Magic);
                                                Formats.WMO.Chunk_MOMT wmoMatChunk = (Formats.WMO.Chunk_MOMT)wmo.getChunk(Formats.WMO.Chunk_MOMT.Magic);

                                                foreach (Chunk_Base rawChunk in wmo.getChunksByID(Formats.WMO.Chunk_MOGP.Magic))
                                                {
                                                    Formats.WMO.Chunk_MOGP chunk = (Formats.WMO.Chunk_MOGP)rawChunk;
                                                    string meshName = wmoNameChunk.data.get((int)chunk.groupNameIndex);

                                                    // Skip antiportals.
                                                    if (meshName.ToLower().Equals("antiportal"))
                                                        continue;

                                                    Mesh mesh = new Mesh(string.Format("WMO{0}_{1}", wmoIndex, meshName));

                                                    // Populate mesh with vertices.
                                                    Formats.WMO.Chunk_MOVT vertChunk = (Formats.WMO.Chunk_MOVT)chunk.getChunk(Formats.WMO.Chunk_MOVT.Magic);
                                                    foreach (Position vertPos in vertChunk.vertices)
                                                        mesh.addVert(new Position(entry.position.X + vertPos.X, entry.position.Y + vertPos.Y, entry.position.Z + vertPos.Z));

                                                    // Populate mesh with UVs.
                                                    Formats.WMO.Chunk_MOTV uvChunk = (Formats.WMO.Chunk_MOTV)chunk.getChunk(Formats.WMO.Chunk_MOTV.Magic);
                                                    foreach (UV uv in uvChunk.uvData)
                                                        mesh.addUV(uv);

                                                    // Populate mesh with normals.
                                                    Formats.WMO.Chunk_MONR normChunk = (Formats.WMO.Chunk_MONR)chunk.getChunk(Formats.WMO.Chunk_MONR.Magic);
                                                    foreach (Position norm in normChunk.normals)
                                                        mesh.addNormal(norm);

                                                    // Populate mesh with triangles (faces).
                                                    Formats.WMO.Chunk_MOVI faceChunk = (Formats.WMO.Chunk_MOVI)chunk.getChunk(Formats.WMO.Chunk_MOVI.Magic);
                                                    Formats.WMO.Chunk_MOPY faceMatChunk = (Formats.WMO.Chunk_MOPY)chunk.getChunk(Formats.WMO.Chunk_MOPY.Magic);

                                                    for (int i = 0; i < faceChunk.positions.Length; i++)
                                                    {
                                                        Formats.WMO.FacePosition position = faceChunk.positions[i];
                                                        Formats.WMO.FaceInfo info = faceMatChunk.faceInfo[i];

                                                        if (info.materialID != 0xFF) // 0xFF (255) identifies a collision face.
                                                        {
                                                            Material mat = wmoMatChunk.materials[info.materialID];
                                                            uint texID = (uint)wmoTexMap[(int)mat.texture1.offset];

                                                            mesh.addFace(texID, mat.texture2.colour, position.point1, position.point2, position.point3);
                                                        }
                                                    }

                                                    Log.Write("CreateWMOMesh (ADT): " + mesh.ToAdvancedString());
                                                    ob.addMesh(mesh);
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (ADTException ex)
                                {
                                    LogWrite("Unable to process tile {0},{1} due to exception: {2}", x, y, ex.Message);
                                    LogWrite(ex.StackTrace);
                                }
                            }
                        }
                    }

                    EventManager.Trigger_LoadingPrompt(new Random().Next(100) == 42 ? "Reticulating splines..." : "Writing terrain data to file...");

                    ob.Write();
                    ob.Close();
                }

                // Job's done.
                EventManager.Trigger_MapExportDone(true);
            }
            catch (Exception e)
            {
                ob.Close();
                EventManager.Trigger_MapExportDone(false, e.Message + e.StackTrace);
            }
        }

        private void LogWrite(string message, params object[] data)
        {
            Log.Write("RunnerMapExport: " + message, data);
        }
    }
}
