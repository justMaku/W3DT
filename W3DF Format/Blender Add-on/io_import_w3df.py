bl_info = {
	'name': 'Import Warcraft 3D Toolkit models (.w3df)',
	'author': 'Kruithne',
	'version': (1, 0, 0),
	'blender': (2, 73, 0),
	'api': 36302,
	'location': 'File > Import > Warcraft 3D Toolkit Model (.w3df)',
	'description': 'Import Warcraft 3D Toolkit models (.w3df)',
	'warning': '',
	'wiki_url': 'https://github.com/Kruithne/W3DT',
	'category': 'Import-Export'}

import bpy
import os
import struct

def MagicToInt32(Value):
	return struct.unpack('<I', Value)[0]

class CDataBinary:
	def __init__(This, File):
		This.File = File
	def ReadString(This, StringLen):
		return struct.unpack('<' + str(StringLen) + 's', This.File.read(StringLen))[0].decode('ascii')
	def ReadUInt8(This):
		return struct.unpack('<B', This.File.read(1))[0]
	def ReadSInt8(This):
		return struct.unpack('<b', This.File.read(1))[0]
	def ReadUInt16(This):
		return struct.unpack('<H', This.File.read(2))[0]
	def ReadSInt16(This):
		return struct.unpack('<h', This.File.read(2))[0]
	def ReadUInt32(This):
		return struct.unpack('<I', This.File.read(4))[0]
	def ReadSInt32(This):
		return struct.unpack('<i', This.File.read(4))[0]
	def ReadFloat32(This):
		return struct.unpack('<f', This.File.read(4))[0]

class CMesh:
	class CVertex:
		def __init__(This):
			This.Position = [0.0, 0.0, 0.0]
			This.BoneWeights = [0, 0, 0, 0]
			This.BoneIndices = [0, 0, 0, 0]
			This.Normal = [0.0, 0.0, 0.0]
			This.Texture = [0.0, 0.0]
	
	class CTriangle:
		def __init__(This):
			This.A = 0
			This.B = 0
			This.C = 0
	
	def __init__(This):
		This.Name = "Unnamed Mesh"
		This.VertexList = []
		This.TriangleList = []

class CBone:
	def __init__(This):
		This.Index = 0
		This.Parent = -1
		This.Position = [0.0, 0.0, 0.0]

class CAttachment:
	def __init__(This):
		This.ID = 0
		This.Parent = -1
		This.Position = [0.0, 0.0, 0.0]
		This.Scale = 1.0

def DoImportFile(FileName):
	MeshList = []
	BoneList = []
	AttachmentList = []
	
	File = open(FileName, 'rb')
	DataBinary = CDataBinary(File)
	
	MgkFile = DataBinary.ReadUInt32()
	if MgkFile != MagicToInt32(b'W3DF'):
		return

	print("Importing W3DF model...")
	MeshCount = DataBinary.ReadUInt32()
	print(str(MeshCount) + " meshes found...")
	
	for i in range(0, MeshCount):
		MgkMesh = DataBinary.ReadUInt32()
		if MgkMesh != MagicToInt32(b'MESH'):
			return

		Mesh = CMesh()
		MeshNameLength = DataBinary.ReadUInt8()
		Mesh.Name = DataBinary.ReadString(MeshNameLength) + "." + str(i)
		print("Building mesh " + Mesh.Name  + "...")

		MeshChunkCount = DataBinary.ReadUInt32()

		for c in range(0, MeshChunkCount):
			MgkChunk = DataBinary.ReadUInt32()

			if MgkChunk == MagicToInt32(b'VERT'):
				print("Processing VERT chunk...")
				VertexCount = DataBinary.ReadUInt32() // 12

				for v in range(0, VertexCount):
					Vertex = CMesh.CVertex()
					Vertex.Position[0] = DataBinary.ReadFloat32()
					Vertex.Position[1] = DataBinary.ReadFloat32()
					Vertex.Position[2] = DataBinary.ReadFloat32()
					Mesh.VertexList.append(Vertex)

			elif MgkChunk == MagicToInt32(b'NORM'):
				print("Processing NORM chunk...")
				NormalCount = DataBinary.ReadUInt32() // 12

				for n in range(0, NormalCount):
					Mesh.VertexList[n].Normal[0] = DataBinary.ReadFloat32()
					Mesh.VertexList[n].Normal[1] = DataBinary.ReadFloat32()
					Mesh.VertexList[n].Normal[2] = DataBinary.ReadFloat32()

			elif MgkChunk == MagicToInt32(b'FACE'):
				print("Processing FACE chunk...")
				FaceCount = DataBinary.ReadUInt32() // 12

				for f in range(0, FaceCount):
					Face = CMesh.CTriangle()
					Face.A = DataBinary.ReadUInt32()
					Face.B = DataBinary.ReadUInt32()
					Face.C = DataBinary.ReadUInt32()
					Mesh.TriangleList.append(Face)

		MeshList.append(Mesh)
	
#	BoneCount = DataBinary.ReadUInt32()
#	for i in range(0, BoneCount):
#		Bone = CBone()
#		Bone.Index = DataBinary.ReadUInt16()
#		Bone.Parent = DataBinary.ReadSInt16()
#		Bone.Position[0] = DataBinary.ReadFloat32()
#		Bone.Position[1] = DataBinary.ReadFloat32()
#		Bone.Position[2] = DataBinary.ReadFloat32()
#		BoneList.append(Bone)
	
#	AttachmentCount = DataBinary.ReadUInt32()
#	for i in range(0, AttachmentCount):
#		Attachment = CAttachment()
#		Attachment.ID = DataBinary.ReadUInt32()
#		Attachment.Parent = DataBinary.ReadSInt16()
#		Attachment.Position[0] = DataBinary.ReadFloat32()
#		Attachment.Position[1] = DataBinary.ReadFloat32()
#		Attachment.Position[2] = DataBinary.ReadFloat32()
#		Attachment.Scale = DataBinary.ReadFloat32()
#		AttachmentList.append(Attachment)
	
	File.close()
	bpy.ops.object.select_all(action = 'DESELECT') # deselect everything
	
	bpy.ops.object.add(type = 'ARMATURE', enter_editmode = True, location = (0.0, 0.0, 0.0))
	BArmature = bpy.context.object
#	for Bone in BoneList: # add bones to armature.
#		BEditBone = BArmature.data.edit_bones.new('Bone' + str('%03d' % Bone.Index))
#		BEditBone.head.x = -Bone.Position[1]
#		BEditBone.head.y = Bone.Position[0]
#		BEditBone.head.z = Bone.Position[2]
#		BEditBone.tail.x = BEditBone.head.x
#		BEditBone.tail.y = BEditBone.head.y + 0.1
#		BEditBone.tail.z = BEditBone.head.z
#	for Bone in BoneList: # link children to parents
#		if Bone.Parent >= 0:
#			BEditBone = BArmature.data.edit_bones['Bone' + str('%03d' % Bone.Index)]
#			BEditBone.parent = BArmature.data.edit_bones['Bone' + str('%03d' % Bone.Parent)]
	bpy.context.scene.update() # update scene.
	bpy.ops.object.mode_set(mode = 'OBJECT') # return to object mode.
#	
#	# instantiate attachments
#	for Attachment in AttachmentList:
#		bpy.ops.object.add(type = 'EMPTY', location = (0.0, 0.0, 0.0))
#		BAttachment = bpy.context.object
#		BAttachment.name = 'Attach' + str('%02d' % Attachment.ID)
#		BBone = BArmature.data.bones['Bone' + str('%03d' % Attachment.Parent)]
#		BAttachment.location.x = -Attachment.Position[1] - BBone.head_local[0]
#		BAttachment.location.y = Attachment.Position[0] - BBone.head_local[1] - 0.1
#		BAttachment.location.z = Attachment.Position[2] - BBone.head_local[2]
#		if Attachment.Parent >= 0:
#			BAttachment.parent = BArmature
#			BAttachment.parent_bone = 'Bone' + str('%03d' % Attachment.Parent)
#			BAttachment.parent_type = 'BONE'
#			BAttachment.empty_draw_size = 0.1
	
	# instantiate meshes
	for Mesh in MeshList:
		bpy.ops.object.add(type = 'MESH', location = (0.0, 0.0, 0.0))
		BMesh = bpy.context.object
		BMesh.name = Mesh.Name
		BMeshData = BMesh.data
		BMeshData.name = BMesh.name
		BMeshData.vertices.add(len(Mesh.VertexList)) # add vertices to mesh data.
		for i, Vertex in enumerate(Mesh.VertexList):
			BVertex = BMeshData.vertices[i]
			BVertex.co.x = Vertex.Position[1]
			BVertex.co.y = Vertex.Position[0]
			BVertex.co.z = Vertex.Position[2]
			BVertex.normal.x = Vertex.Normal[1]
			BVertex.normal.y = Vertex.Normal[0]
			BVertex.normal.z = Vertex.Normal[2]

		BMeshData.tessfaces.add(len(Mesh.TriangleList)) # add triangles to mesh data.
		BMeshTextureFaceLayer = BMeshData.tessface_uv_textures.new(name = 'UVMap')
		for i, Triangle in enumerate(Mesh.TriangleList):
			BFace = BMeshData.tessfaces[i]
			BFace.vertices = [Triangle.A, Triangle.B, Triangle.C]	# reverse the wind order so normals point out.
			#BMeshTextureFace = BMeshTextureFaceLayer.data[i]
			#BMeshTextureFace.uv1[0] = Mesh.VertexList[Triangle.A].Texture[0]
			#BMeshTextureFace.uv1[1] = 1.0 - Mesh.VertexList[Triangle.A].Texture[1]
			#BMeshTextureFace.uv2[0] = Mesh.VertexList[Triangle.B].Texture[0]
			#BMeshTextureFace.uv2[1] = 1.0 - Mesh.VertexList[Triangle.B].Texture[1]
			#BMeshTextureFace.uv3[0] = Mesh.VertexList[Triangle.C].Texture[0]
			#BMeshTextureFace.uv3[1] = 1.0 - Mesh.VertexList[Triangle.C].Texture[1]
			BFace.use_smooth = True

#		for Bone in BoneList:
#			BVertexGroup = BMesh.vertex_groups.new('Bone' + str('%03d' % Bone.Index))
#
#		for i, Vertex in enumerate(Mesh.VertexList):
#			BVertex = BMeshData.vertices[i]
#			for j in range(0, 4):
#				if Vertex.BoneWeights[j] > 0:
#					BVertexGroup = BMesh.vertex_groups['Bone' + str('%03d' % Vertex.BoneIndices[j])]
#					BVertexGroup.add([i], float(Vertex.BoneWeights[j])/255.0, 'ADD')
#
		BMeshData.update()
#		BArmatureModifier = BMesh.modifiers.new('Armature', 'ARMATURE')
#		BArmatureModifier.object = BArmature
#		BArmatureModifier.use_bone_envelopes = False
#		BArmatureModifier.use_vertex_groups = True
		BMesh.parent = BArmature
		BMesh.select = False
	
	BArmature.select = True
	bpy.context.scene.objects.active = BArmature
	
	print(FileName + ' imported successfully.')


class M2IImporter(bpy.types.Operator):
	'''Import W3DF File'''
	bl_idname = 'import.w3df'
	bl_label = 'Import W3DF'
	
	filepath = bpy.props.StringProperty(name = 'File Path', description = 'Filepath of the W3DF file to import', maxlen = 1024, default = '')
	check_existing = bpy.props.BoolProperty(name = 'Check Existing', description = 'Show warning when over-writing files', default = True, options = {'HIDDEN'})
	filter_glob = bpy.props.StringProperty(default = '*.w3df', options = {'HIDDEN'})
	
	def execute(self, context):
		FilePath = self.properties.filepath

		if not FilePath.lower().endswith('.w3df'):
			FilePath += '.w3df'

		DoImportFile(FilePath)
		return {'FINISHED'}
	
	def invoke(self, context, event):
		WindowManager = context.window_manager
		WindowManager.fileselect_add(self)
		return {'RUNNING_MODAL'}


def menu_func(self, context):
	default_path = os.path.splitext(bpy.data.filepath)[0] + '.w3df'
	self.layout.operator(M2IImporter.bl_idname, text = 'Warcraft 3D Toolkit Model (.w3df)').filepath = default_path


def register():
	bpy.utils.register_module(__name__)
	bpy.types.INFO_MT_file_import.prepend(menu_func)


def unregister():
	bpy.utils.unregister_module(__name__)
	bpy.types.INFO_MT_file_import.remove(menu_func)


if __name__ == '__main__':
	register()
