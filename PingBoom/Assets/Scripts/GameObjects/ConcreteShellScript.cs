using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;


namespace GameObjects
{
    public class ConcreteShellScript : MonoBehaviour
    {
		[SerializeField]
		List<Sprite> shellStages;
        LevelManager levelManager;
		SpriteRenderer mySpriteRenderer;
		int currLife;
        void Start()
        {
			mySpriteRenderer = GetComponent<SpriteRenderer>();
			levelManager = FindObjectOfType<LevelManager>();
			currLife = shellStages.Count - 1;
        }

		void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.CompareTag("Player"))
			{
				levelManager.PlayEffect(GameManager.EffectSounds.Concrete);
				currLife -= 1;
				if (currLife >= 0)
				{
					mySpriteRenderer.sprite = shellStages[currLife];
				}
				else
				{
					Destroy(gameObject);
				}

			}
		}
    }
}