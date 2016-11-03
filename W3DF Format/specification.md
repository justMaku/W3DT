#W3DF File Format Specification

W3DF are 3D models exported from W3DT including vertex, face, texture, bone and animation data. The intention of this format is to greatly reduce the complication needed for scripts to import models from World of Warcraft into common 3D applications.

Files produced from W3DT in this format will use the little-endian byte-order. The specification for this format may expand as more features are added, thus it has been designed in a chunked structure to improve backward compatibility in this scenario.

### Header

```
0x00000000	UInt32	FileMagic		// Always 1178874711, abort reading if incorrect.
0x00000004	UInt32	MeshCount		// Number of meshes included in the file.
```

### MESH Chunk

Starting at `0x00000008` and onwards will contain MESH chunks; there will be `MeshCount` amount of these chunks present. The structure of these chunks is outlined below.

```
UInt32		MeshChunkMagic		// Always 1213416781, indicating the start of a MESH chunk.
UInt8		MeshNameLength		// Length of the name of the mesh, which will follow instantly after.
String		MeshName			// Name of the mesh, exactly MeshNameLength bytes in length.
UInt32		SubChunkCount		// Defines how many sub chunks the mesh has (useful if skipping).
```

Every MESH chunk will start with the above header. The data that follows will be `SubChunkCount` amount of sub-chunks, which should be read depending on their magic value. See below for the different structures for sub-chunks.

### VERT Sub-chunk

Contains positions of the vertices to use in this model.

```
UInt32		ChunkMagic		// 1414677846 - Indicates this chunk is a VERT chunk.
UInt32		ChunkSize		// Exact count of bytes in this chunks data (non-inclusive of the size or magic).
float[3]	Vertices		// (ChunkSize / 12) sets of 3 floats (X, Y, Z).
```

### NORM Sub-chunk

Contains normals for this model. Relative to the correlating index in the VERT chunk.

```
UInt32		ChunkMagic		// 1297239886 - Indicates this chunk is a NORM chunk.
UInt32		ChunkSize		// Exact count of bytes in this chunks data (non-inclusive of the size or magic).
float[3]	Normal			// (ChunkSize / 12) sets of 3 floats (X, Y Z).
```

### FACE Sub-chunk

Contains faces (triangles) for this model. Each point is an index into the VERT chunk.

```
UInt32		ChunkMagic		// 1162035526 - Indicates this chunk is a FACE chunk.
UInt32		ChunkSize		// Exact count of bytes in this chunks data (non-inclusive of the size or magic).
UInt32[3]	Points			// (ChunkSize / 12) sets of 3 UInt32 values (A, B, C).
