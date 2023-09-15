using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

#if !XBOX
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System.Runtime.Serialization.Formatters.Binary;
#endif


using System.IO;
using System.Diagnostics.CodeAnalysis;

using Sxe.Library.Services;

#region FxCop
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVertexes.#color")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVertexes.#normal")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVertexes.#position")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVertexes.#texCoords")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLightMap.#endCoords")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLightMap.#startCoords")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLightMap.#map")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLightMap.#lightMapSize")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFaceGroup.#startOffset")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFaceGroup.#index")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFaceGroup.#texIndex")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFaceGroup.#numberOfFaces")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFaceGroup.#max")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFaceGroup.#model")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFaceGroup.#startVertex")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFaceGroup.#numberOfIndexes")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFaceGroup.#lmIndex")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFaceGroup.#min")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLeafFaces.#face")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspMeshVertexes.#offset")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLeaf.#leafFace")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLeaf.#min")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLeaf.#cluster")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLeaf.#numberOfLeafBrushes")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLeaf.#max")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLeaf.#area")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLeaf.#leafBrush")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLeaf.#numberOfLeafFaces")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspModels.#min")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspModels.#face")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspModels.#numberOfFaces")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspModels.#brush")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspModels.#numberOfBrushes")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspModels.#max")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspNode.#planeIndex")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspNode.#children")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspNode.#min")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspNode.#max")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLightVolume.#DirectionalColor")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLightVolume.#Direction")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLightVolume.#ambient")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLightVolume.#AmbientColor")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLightVolume.#directional")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspLightVolume.#dir")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#vertex")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#normal")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#faceGroup")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#tangent")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#lm_origin")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#effect")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#lm_start")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#old_normal")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#lightMapVectors")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#lm_size")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#numberOfMeshVertexes")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#binormal")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#type")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#numberOfVertexes")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#texture")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#meshVertex")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#size")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspFace.#lm_index")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVisibilityData.#visibilityData")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVisibilityData.#sizeOfVectors")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVisibilityData.#numberOfVectors")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspTextures.#textureName")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspPlane.#distance")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspPlane.#normal")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspDirectoryEntry.#length")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspDirectoryEntry.#offset")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspHeader.#directoryEntry")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspHeader.#header")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspHeader.#version")]
#endregion

namespace Sxe.Library.Bsp
{

    /// <summary>
    /// The header is the table of contents for BSP data
    /// </summary>
    [Serializable]
    public class BspHeader
    {
        public byte[] header;
        public int version;
        public BspDirectoryEntry[] directoryEntry;

    }

    /// <summary>
    /// A dir entry is an entry in the table of contents for BSP data
    /// </summary>
    [Serializable]
    public class BspDirectoryEntry
    {
        public int offset;
        public int length;
    }

    /// <summary>
    /// Textures contains the name of the texture
    /// </summary>
    [Serializable]
    public class BspTextures
    {
        public string textureName;

        public void DebugPrint(ILogService log)
        {
            log.Print("[Textures] " + textureName.ToString());
        }

    }

    /// <summary>
    /// Contains all the dividing planes of the BSP
    /// </summary>
    [Serializable]
    public class BspPlane
    {
        public Vector3 normal; //normal of the plane
        public float distance; //distance from origin

        public void DebugPrint(ILogService log)
        {
            log.Print(String.Format(CultureInfo.CurrentCulture, "[Plane] Normal: {0} Distance: {1}", normal, distance));
        }
    }

    /// <summary>
    /// Describes a node in the BSP tree
    /// </summary>
    [Serializable]
    public class BspNode
    {
        public int planeIndex;
        public int[] children;
        public int[] min;
        public int[] max;

        public BspNode()
        {
            children = new int[2];
            min = new int[3];
            max = new int[3];
        }

        public void DebugPrint(ILogService log)
        {
            log.Print(String.Format(CultureInfo.CurrentCulture, "[Node] PlaneIndex: {0} Child1: {1} Child2: {2}", planeIndex, children[0], children[1]));
        }
    }


    /// <summary>
    /// Describes a leaf in the BSP tree
    /// </summary>
    [Serializable]
    public class BspLeaf
    {
        public int cluster;
        public int area;
        public int[] min;
        public int[] max;
        public int leafFace;
        public int numberOfLeafFaces;
        public int leafBrush;
        public int numberOfLeafBrushes;

        public BspLeaf()
        {
            min = new int[3];
            max = new int[3];
        }

        public void DebugPrint(ILogService log)
        {
            log.Print(String.Format(CultureInfo.CurrentCulture, "[Leaf] Cluster: {0} Leafface: {1} N_Leaffaces: {2}", cluster, leafFace, numberOfLeafFaces));
        }
    }

    /// <summary>
    /// Offileet for leaf faces
    /// </summary>
    [Serializable]
    public class BspLeafFaces
    {
        public int face;

        public void DebugPrint(ILogService log)
        {
            log.Print(String.Format(CultureInfo.CurrentCulture, "[LeafFace] Face: {0}", face));
        }
    }


    /// <summary>
    /// Describes rigid lumps of world geometry
    /// models[0] = world geometry
    /// </summary>
    [Serializable]
    public class BspModels
    {
        public Vector3 min; //bounding box min coord
        public Vector3 max; //bounding box max cood
        public int face; //first face for model
        public int numberOfFaces; //number of faces for model
        public int brush; //first readerush for mdoel
        public int numberOfBrushes; //number of readerushes for model

        public void DebugPrint(ILogService log)
        {
            log.Print(String.Format(CultureInfo.CurrentCulture, "[Model] Face: {0} N_Faces: {1}", face, numberOfFaces));
        }
    }

    /// <summary>
    /// Information about each vertex in the BSP
    /// </summary>
    [Serializable]
    public class BspVertexes
    {
        public Vector3 position;
        public Vector2[] texCoords;
        public Vector3 normal;
        public byte[] color;

        public BspVertexes()
        {
            texCoords = new Vector2[2];
            color = new byte[4];
        }

        public void DebugPrint(ILogService log)
        {
            log.Print(String.Format(CultureInfo.CurrentCulture, "[Vertex] Position: {0} DiffuseTexCoords: {1} LightTexCoords: {2}", position, texCoords[0], texCoords[1]));
        }

    }

    /// <summary>
    /// Vertex offset
    /// </summary>
    [Serializable]
    public class BspMeshVertexes
    {
        public int offset;

        public void DebugPrint(ILogService log)
        {
            log.Print(String.Format(CultureInfo.CurrentCulture, "[Meshverts] Offset: {0}", offset));
        }

    }


    /// <summary>
    /// Different face types
    /// </summary>
    public enum BspFaceType
    {
        Polygon = 1,
        Patch = 2,
        Mesh = 3,
        Billboard = 4
    }


    /// <summary>
    /// Describes each type of face
    /// </summary>
    [Serializable]
    public class BspFace
    {
        public int texture;
        public int effect;
        public int type;
        public int vertex;
        public int numberOfVertexes;
        public int meshVertex;
        public int numberOfMeshVertexes;
        public int lm_index;
        public int[] lm_start;
        public int[] lm_size;
        public Vector3 lm_origin;
        public Vector3[] lightMapVectors;
        public Vector3 normal;
        public Vector3 old_normal;
        public Vector3 binormal;
        public Vector3 tangent;
        public int[] size;

        public int faceGroup = -1; //used internally to group faces together

        //public BSPPatch patch; //Only used if the face is a patch type,otherwise should always be null

        public BspFace()
        {
            lm_start = new int[2];
            lm_size = new int[2];
            lightMapVectors = new Vector3[2];
            size = new int[2];
        }

        public void DebugPrint(ILogService log)
        {
            log.Print(String.Format(CultureInfo.CurrentCulture, "[Face] Texture: {0} Vertex: {1} N_vertexes: {2}", texture, vertex, numberOfVertexes));
        }

    }

    [Serializable]
    public class BspLightMap
    {
        public int lightMapSize;
        public Color[] map;
        //public Texture2D lightTexture;

        public Vector2 startCoords;
        public Vector2 endCoords;

        public BspLightMap()
        {
        }

        public BspLightMap(int size)
        {
            lightMapSize = size;
            map = new Color[size * size];
        }

        public void DebugPrint(ILogService log)
        {

        }

    }

    /// <summary>
    /// Light volumes give information about ambient world light
    /// </summary>
    [Serializable]
    public class BspLightVolume
    {
        public byte[] ambient; //ambient light component
        public byte[] directional; //directional light component
        public byte[] dir; //direction to light 0=phi, 1=theta

        public Vector3 AmbientColor;
        public Vector3 DirectionalColor;
        public Vector3 Direction;

        public BspLightVolume()
        {
            ambient = new byte[3];
            directional = new byte[3];
            dir = new byte[2];
        }

        public void DebugPrint(ILogService log)
        {
        }

    }

    [Serializable]
    public class BspVisibilityData
    {
        public int numberOfVectors;
        public int sizeOfVectors;
        public byte[] visibilityData;


        public void DebugPrint(ILogService log)
        {
        }
    }

}
