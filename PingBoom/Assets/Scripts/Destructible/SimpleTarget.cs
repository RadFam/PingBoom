using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class SimpleTarget : ExplodeTargetMeta
    {

        SpriteRenderer mySpriteRenderer;
		
		// Use this for initialization
        protected override void Start()
        {
			base.Start();
			mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.CompareTag("Player"))
			{
				LevelManager lm = FindObjectOfType<LevelManager>();
				lm.AddNewScore(scoreCount, 1);
				SetItselfInvisible();
				SetExplosion();
			}
		}

		protected override void SetItselfInvisible()
		{
			mySpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		}
    }
}