using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class StagedTarget : ExplodeTargetMeta
    {
		[SerializeField]
		int myStages;

		List<Color> spriteColorsList;
		SpriteRenderer mySpriteRenderer;

		Collider2D myCollider;

		int currStage;
        // Use this for initialization
        protected override void Start()
        {
			currStage = myStages;
			spriteColorsList = new List<Color>();
			spriteColorsList.Add(new Color(0.0f, 1.0f, 0.0f, 1.0f));
			spriteColorsList.Add(new Color(1.0f, 0.9f, 0.0f, 1.0f));
			spriteColorsList.Add(new Color(1.0f, 0.5f, 0.0f, 1.0f));
			spriteColorsList.Add(new Color(1.0f, 0.0f, 0.0f, 1.0f));
			mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			myCollider = gameObject.GetComponent<Collider2D>();
			myCollider.isTrigger = false;

			base.Start();
        }

        void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.CompareTag("Player"))
			{
				currStage -= 1;
				mySpriteRenderer.color = spriteColorsList[spriteColorsList.Count - currStage];
				if (currStage == 1)
				{
					myCollider.isTrigger = true;
				}
			}
		}

		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.CompareTag("Player"))
			{
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