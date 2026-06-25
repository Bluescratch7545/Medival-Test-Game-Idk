using UnityEngine;

namespace U2D
{
    [CreateAssetMenu(menuName = "Unity 2d Sprite/Create Animation Clip")]
    public class u2dAnimationClip : ScriptableObject
    {
        public u2dSpriteCollectionData atlas;
        public int[] frames;
        public float fps;
        public bool loop;
    }
}