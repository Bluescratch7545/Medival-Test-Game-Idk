using UnityEngine;

namespace U2D
{
    [RequireComponent(typeof(u2dSprite))]
    public class u2dSpriteAnimator : MonoBehaviour
    {
        public u2dAnimationClip currentClip;

        private u2dSprite sprite;

        private int currentFrame;
        private float timer;
        private bool playing;

        void Awake()
        {
            sprite = GetComponent<u2dSprite>();
        }

        void Update()
        {
            if (!playing || currentClip == null)
            {
                return;
            }

            timer += Time.deltaTime;

            currentFrame++;

            if (currentFrame >= currentClip.frames.Length)
            {
                if (currentClip.loop)
                {
                    currentFrame = 0;
                }
                else
                {
                    currentFrame = currentClip.frames.Length - 1;
                    playing = false;
                }
            }

            sprite.SetSprite(currentClip.frames[currentFrame]);
        }

        public void Play(u2dAnimationClip clip)
        {
            if (clip == null) return;

            currentClip = clip;

            sprite.collection = clip.atlas;

            currentFrame = 0;
            timer = 0;

            sprite.SetSprite(clip.frames[0]);

            playing = true;

            sprite.SetSprite(currentClip.frames[0]);
        }

        public void Stop()
        {
            playing = false;
        }

        public bool IsPlaying()
        {
            return playing;
        }
    }
}