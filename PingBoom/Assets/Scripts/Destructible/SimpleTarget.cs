using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class SimpleTarget : ExplodeTargetMeta
    {

        SpriteRenderer mySpriteRenderer;
		bool isExploded;
		
		// Use this for initialization
        protected override void Start()
        {
			base.Start();
			mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			isExploded = false;
        }

        void OnTriggerEnter2D(Collider2D col)
		{
			if (!isExploded && col.gameObject.CompareTag("Player"))
			{
				//Debug.Log(gameObject.name + " is exploded");
				isExploded = true;
				Debug.Log("Simple target, minus ONE destTarget");
				levelManager.AddNewScore(scoreCount, -1);
				SetItselfInvisible();
				levelManager.PlayEffect(GameManager.EffectSounds.Explosion);
				SetExplosion();
			}
		}

		protected override void SetItselfInvisible()
		{
			mySpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
			DeadHand();
		}
    }
}