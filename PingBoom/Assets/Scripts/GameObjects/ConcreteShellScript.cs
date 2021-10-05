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
				string name = col.gameObject.GetComponent<PlayerMoveControl>().myName;
				levelManager.PlayEffect(GameManager.EffectSounds.Concrete);
				if (name == "Crushing" || name == "Moon" || name == "Pirate")
				{
					currLife = -1;
				}
				else if (name == "Concrete")
				{
					currLife -= 2;
				}
				else
				{
					currLife -= 1;
				}
				
				if (currLife >= 0)
				{
					mySpriteRenderer.sprite = shellStages[shellStages.Count - 1 - currLife];
				} 
				else
				{
					Destroy(gameObject);
				}

			}
		}
    }
}