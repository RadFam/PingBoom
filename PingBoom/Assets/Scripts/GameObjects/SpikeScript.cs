using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class SpikeScript : MonoBehaviour
    {
		LevelManager levelManager;
        void Start()
        {
			levelManager = FindObjectOfType<LevelManager>();
        }

        void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.CompareTag("Player"))
			{
				col.gameObject.GetComponent<PlayerMoveControl>().DeathStop();
				levelManager.PlayEffect(GameManager.EffectSounds.Steel);
				levelManager.PlayerIsDead();
			}
		}
    }
}