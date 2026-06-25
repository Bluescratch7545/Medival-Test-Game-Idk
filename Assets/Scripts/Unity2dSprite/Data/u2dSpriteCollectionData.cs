using UnityEngine;

namespace U2D
{
    [CreateAssetMenu(menuName = "Unity 2d Sprite/Create Sprite Collection")]
    public class u2dSpriteCollectionData : ScriptableObject
    {
        public Material material;
        public u2dSpriteDefinition[] sprites;
    }
}