using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class ShieldCoverTarget : MonoBehaviour
    {

        // Use this for initialization
		LevelManager levelManager;
        void Start()
        {
			levelManager = FindObjectOfType<LevelManager>();
        }

        // Update is called once per frame
		
		void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.CompareTag("Player"))
			{
				levelManager.PlayEffect(GameManager.EffectSounds.Steel);
			}
		}
		
    }
}