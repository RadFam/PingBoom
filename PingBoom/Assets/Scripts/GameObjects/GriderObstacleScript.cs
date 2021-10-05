using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class GriderObstacleScript : MonoBehaviour
    {

        // Use this for initialization
		LevelManager levelManager;
        void Start()
        {
			levelManager = FindObjectOfType<LevelManager>();
        }

		void OnCollisionEnter2D(Collision2D col)
		{
			string name = col.gameObject.GetComponent<PlayerMoveControl>().myName;
			if (col.gameObject.CompareTag("Player"))
			{
				levelManager.PlayEffect(GameManager.EffectSounds.Steel);
				if (name == "Pirate")
				{
					Destroy(this.gameObject);
				}
			}
		}
    }
}