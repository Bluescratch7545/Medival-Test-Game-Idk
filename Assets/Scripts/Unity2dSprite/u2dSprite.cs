using UnityEngine;

namespace U2D{
    [System.Serializable]
    public class u2dSpriteDefinition
    {
        public string name;
        public Sprite sprite;
    }

    [CreateAssetMenu(menuName = "Unity 2d Sprite/Create Sprite Collection")]
    public class u2dSpriteCollectionData : ScriptableObject
    {
        public Material material;
        public u2dSpriteDefinition[] sprites;
    }

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class u2dSprite : MonoBehaviour
    {
        public u2dSpriteCollectionData collection;
    
        Mesh mesh;
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;

        int currentSpriteID = -1;

        public bool FlipX;

        void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();

            mesh = new Mesh();
            meshFilter.mesh = mesh;

            meshRenderer.material = collection.material;

            BuildQuad();
            mesh.RecalculateBounds();
        }

        public void SetSprite(int id)
        {
            if (id == currentSpriteID)
                return;

            currentSpriteID = id;

            Sprite sprite = collection.sprites[id].sprite;

            float width = sprite.rect.width / sprite.pixelsPerUnit;
            float height = sprite.rect.height / sprite.pixelsPerUnit;

            mesh.vertices = new Vector3[]
            {
                new Vector3(-width * 0.5f, -height * 0.5f, 0),
                new Vector3(-width * 0.5f,  height * 0.5f, 0),
                new Vector3( width * 0.5f,  height * 0.5f, 0),
                new Vector3( width * 0.5f, -height * 0.5f, 0)
            };

            meshRenderer.material.mainTexture = sprite.texture;

            Vector2[] uvs = sprite.uv;

            if (!FlipX)
            {
                mesh.uv = uvs;
            }
            else
            {
                mesh.uv = new Vector2[]
                {
                uvs[3],
                uvs[2],
                uvs[1],
                uvs[0]
                };
            }

            mesh.RecalculateBounds();
        }
        private void BuildQuad()
        {
            mesh.vertices = new Vector3[]
            {
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(-0.5f,  0.5f, 0),
            new Vector3( 0.5f,  0.5f, 0),
            new Vector3( 0.5f, -0.5f, 0)
            };

            mesh.triangles = new int[]
            {
            0, 1, 2,
            0, 2, 3
            };

            mesh.normals = new Vector3[]
            {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
            };
        }
    }

    [CreateAssetMenu(menuName = "Unity 2d Sprite/Create Animation Clip")]
    public class u2dAnimationClip : ScriptableObject
    {
        public u2dSpriteCollectionData atlas;
        public int[] frames;
        public float fps;
        public bool loop;
    }
}