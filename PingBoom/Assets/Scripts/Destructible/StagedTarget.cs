using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

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
		bool isExploded;

		PhysicsMaterial2D myMaterial;
        // Use this for initialization
        protected override void Start()
        {
			currStage = myStages;
			spriteColorsList = new List<Color>();
			spriteColorsList.Add(new Color(0.45f, 0.8f, 0.3f, 1.0f));
			spriteColorsList.Add(new Color(1.0f, 0.8f, 0.0f, 1.0f));
			spriteColorsList.Add(new Color(0.95f, 0.61f, 0.04f, 1.0f));
			spriteColorsList.Add(new Color(0.91f, 0.42f, 0.09f, 1.0f));
			mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			myCollider = gameObject.GetComponent<Collider2D>();
			myCollider.isTrigger = false;

			mySpriteRenderer.color = spriteColorsList[spriteColorsList.Count - currStage];
			isExploded = false;
			myMaterial = gameObject.GetComponent<Rigidbody2D>().sharedMaterial;

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
					myMaterial = null;
				}
			}
		}

		void OnTriggerEnter2D(Collider2D col)
		{
			if (!isExploded && col.gameObject.CompareTag("Player"))
			{
				isExploded = true;
				Debug.Log("Staged target, minus ONE destTarget");
				levelManager.AddNewScore(scoreCount, -1);
				SetItselfInvisible();
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