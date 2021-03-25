using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class SawDiskScript : MonoBehaviour
    {
		[SerializeField]
		float rotationSpeed;
		Rigidbody2D myRigid;
		LevelManager levelManager;
        // Use this for initialization
        void Start()
        {
			myRigid = GetComponent<Rigidbody2D>();
			levelManager = FindObjectOfType<LevelManager>();
        }

        // Update is called once per frame
        void Update()
        {
			myRigid.rotation += rotationSpeed * Time.fixedDeltaTime;
        }

		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.CompareTag("Player"))
			{
				levelManager.PlayEffect(GameManager.EffectSounds.Steel);
				levelManager.PlayerIsDead();
			}
		}
    }
}