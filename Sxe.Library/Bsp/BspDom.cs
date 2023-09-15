using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics.CodeAnalysis;


using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;

#if !XBOX

using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
#endif

using Sxe.Library.Services;

#region FxCop Suppression
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Planes")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Planes")]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#LightMaps")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#LightMaps")]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Models")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Models")]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Vertexes")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Vertexes")]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Leafs")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Leafs")]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Faces")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Faces")]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Nodes")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Nodes")]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#LeafFaces")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#LeafFaces")]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#MeshVertexes")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#MeshVertexes")]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#LightVolumes")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#LightVolumes")]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Textures")]
[module: SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Sxe.Library.Bsp.BspDom.#Textures")]
#endregion

namespace Sxe.Library.Bsp
{
    /// <summary>
    /// The document object model for all BSPs
    /// </summary>
    public class BspDom
    {

        //This is the major data that defines the BSP DOM
        string entityData;
        BspTextures[] textures;
        BspPlane[] planes;
        BspNode[] nodes;
        BspLeaf[] leafs;
        BspLeafFaces[] leafFaces;
        BspModels[] models;
        BspVertexes[] vertexes;
        BspMeshVertexes[] meshVertexes;
        BspFace[] faces;
        BspLightMap[] lightMaps;
        BspLightVolume[] lightVolumes;
        BspVisibilityData visibilityData;

        List<BspTextures> textureCollection;
        List<BspPlane> planeCollection;
        List<BspNode> nodeCollection;
        List<BspLeaf> leafCollection;
        List<BspLeafFaces> leafFaceCollection;
        List<BspModels> modelCollection;
        List<BspVertexes> vertexCollection;
        List<BspMeshVertexes> meshVertexCollection;
        List<BspFace> faceCollection;
        List<BspLightMap> lightMapCollection;
        List<BspLightVolume> lightVolumeCollection;

        #region Fields
        public string EntityData
        {
            get { return entityData; }
            set { entityData = value; }
        }
        public List<BspTextures> Textures
        {
            get { return textureCollection; }
            set { textureCollection = value; }
        }
        public List<BspPlane> Planes
        {
            get { return planeCollection; }
            set { planeCollection = value; }
        }
        public List<BspNode> Nodes
        {
            get { return nodeCollection; }
            set { nodeCollection = value; }
        }
        public List<BspLeaf> Leafs
        {
            get { return leafCollection; }
            set { leafCollection = value; }
        }
        public List<BspLeafFaces> LeafFaces
        {
            get { return leafFaceCollection; }
            set { leafFaceCollection = value; }
        }
        public List<BspModels> Models
        {
            get { return modelCollection; }
            set { modelCollection = value; }
        }
        public List<BspVertexes> Vertexes
        {
            get { return vertexCollection; }
            set { vertexCollection = value; }
        }
        public List<BspMeshVertexes> MeshVertexes
        {
            get { return meshVertexCollection; }
            set { meshVertexCollection = value; }
        }
        public List<BspFace> Faces
        {
            get { return faceCollection; }
            set { faceCollection = value; }
        }
        public List<BspLightMap> LightMaps
        {
            get { return lightMapCollection; }
            set { lightMapCollection = value; }
        }
        public List<BspLightVolume> LightVolumes
        {
            get { return lightVolumeCollection; }
            set { lightVolumeCollection = value; }
        }
        public BspVisibilityData Visibility
        {
            get { return visibilityData; }
            set { visibilityData = value; }
        }


        #endregion




        private BspDom()
        {
        }

#if !XBOX
        private BspDom(FileStream fileStream, BinaryReader reader, IBspParser parser)
        {
            BspHeader header = parser.ReadHeader(fileStream, reader);
            this.entityData = parser.ReadEntities(fileStream, reader, header);
            this.textures = parser.ReadTextures(fileStream, reader, header);
            this.planes = parser.ReadPlanes(fileStream, reader, header);
            this.nodes = parser.ReadNodes(fileStream, reader, header);
            this.leafs = parser.ReadLeafs(fileStream, reader, header);
            this.leafFaces = parser.ReadLeafFaces(fileStream, reader, header);
            this.models = parser.ReadModels(fileStream, reader, header);
            this.vertexes = parser.ReadVertexes(fileStream, reader, header);
            this.meshVertexes = parser.ReadMeshVertexes(fileStream, reader, header);
            this.faces = parser.ReadFaces(fileStream, reader, header);
            this.lightMaps = parser.ReadLightMaps(fileStream, reader, header);
            this.lightVolumes = parser.ReadLightVolumes(fileStream, reader, header);
            this.visibilityData = parser.ReadVisibilityData(fileStream, reader, header);


        }
#endif

        //Initialize should be called after either constructor
        void Initialize()
        {
            textureCollection = new List<BspTextures>(textures);
            planeCollection = new List<BspPlane>(planes);
            nodeCollection = new List<BspNode>(nodes);
            leafCollection = new List<BspLeaf>(leafs);
            leafFaceCollection = new List<BspLeafFaces>(leafFaces);
            modelCollection = new List<BspModels>(models);
            vertexCollection = new List<BspVertexes>(vertexes);
            meshVertexCollection = new List<BspMeshVertexes>(meshVertexes);
            faceCollection = new List<BspFace>(faces);
            lightMapCollection = new List<BspLightMap>(lightMaps);
            lightVolumeCollection = new List<BspLightVolume>(lightVolumes);
        }

        public void DebugPrint(ILogService log)
        {
            log.Print("**BSP DATA**");
            log.Print("[Entities] " + entityData);
            foreach (BspTextures t in textures)
                t.DebugPrint(log);
            foreach (BspPlane p in planes)
                p.DebugPrint(log);
            foreach (BspNode n in nodes)
                n.DebugPrint(log);
            foreach (BspLeaf l in leafs)
                l.DebugPrint(log);
            foreach (BspLeafFaces l in leafFaces)
                l.DebugPrint(log);
            foreach (BspModels m in models)
                m.DebugPrint(log);
            foreach (BspVertexes v in vertexes)
                v.DebugPrint(log);
            foreach (BspMeshVertexes m in meshVertexes)
                m.DebugPrint(log);
            foreach (BspFace f in faces)
                f.DebugPrint(log);
            foreach (BspLightMap l in lightMaps)
                l.DebugPrint(log);
            foreach (BspLightVolume v in lightVolumes)
                v.DebugPrint(log);

            visibilityData.DebugPrint(log);

        }

#if !XBOX
        public void Write(ContentWriter writer)
        {

            MemoryStream ms = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(BspDom),
                new Type[] 
                {
                    typeof(BspTextures),
                    typeof(BspPlane),
                    typeof(BspModels),
                    typeof(BspVertexes),
                    typeof(BspMeshVertexes),
                    typeof(BspFace),
                    typeof(BspLeaf),
                    typeof(BspLeafFaces),
                    typeof(BspLightMap),
                    typeof(BspLightVolume),
                    typeof(BspNode),
                    typeof(BspVisibilityData)
                });
            xs.Serialize(ms, this);


            int size = (int)ms.Position;
            ms.Position = 0;
            byte[] buf = new byte[size];
            ms.Read(buf, 0, size);

            writer.Write(size);
            writer.Write(buf, 0, size);


            //Let's try and automate this through serialization
            //MemoryStream ms = new MemoryStream();
            //BinaryFormatter bs = new BinaryFormatter();
            //bs.Serialize(ms, this);

            //int size = (int)ms.Position;
            //ms.Position = 0;
            //byte[] buf = new byte[size];
            //ms.Read(buf, 0, size);

            //Write the size and results
            //writer.Write(size);
            //writer.Write(buf, 0, size);
        }

        public static BspDom CreateFromFile(FileStream fileStream, BinaryReader reader, IBspParser parser)
        {
            BspDom returnBsp = new BspDom(fileStream, reader, parser);
            returnBsp.Initialize();
            return returnBsp;
        }

#endif
        public static BspDom Read(ContentReader cr)
        {
            int size = cr.ReadInt32();
            byte[] buf = new byte[size];

            buf = cr.ReadBytes(size);

            MemoryStream ms = new MemoryStream(buf, 0, size);
            XmlSerializer xs = new XmlSerializer(typeof(BspDom));

            byte[] bytes = ms.ToArray();
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0) bytes[i] = 1;
            }

            string sz = System.Text.ASCIIEncoding.ASCII.GetString(bytes, 0, bytes.Length);

         

            BspDom bd = (BspDom)xs.Deserialize(ms);
            //bd.Initialize();
            return bd;

            //int size = cr.ReadInt32();
            //byte[] buf = new byte[size];

            //buf = cr.ReadBytes(size);

            //MemoryStream ms = new MemoryStream(buf, 0, size);
            //BinaryFormatter bf = new BinaryFormatter();
            //BspDom bd = (BspDom)bf.Deserialize(ms);
            //bd.Initialize();
            return bd;
        }




        

    }
}
