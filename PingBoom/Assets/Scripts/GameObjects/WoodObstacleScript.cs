using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class WoodObstacleScript : MonoBehaviour
    {
        LevelManager levelManager;
        void Start()
        {
			levelManager = FindObjectOfType<LevelManager>();
        }

		void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.CompareTag("Player"))
			{
				levelManager.PlayEffect(GameManager.EffectSounds.Wood);
			}
		}
    }
}