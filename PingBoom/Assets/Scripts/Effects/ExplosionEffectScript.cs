using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class ExplosionEffectScript : MonoBehaviour
    {
        public SpriteRenderer mySprite;
        public Animator myAnimator;
        // Use this for initialization
        Color colorAlpha;

        void Start()
        {
            colorAlpha = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        public void SetVisible()
        {
            mySprite.color = colorAlpha;
            myAnimator.SetTrigger("SetExplode");
        }

        public void DestroyAll()
        {
            // Destroy parent object
			Destroy(transform.parent.gameObject);
        }
    }
}