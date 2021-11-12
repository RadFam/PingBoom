using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class StoneObstacleScript : MonoBehaviour
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
				levelManager.PlayEffect(GameManager.EffectSounds.Wood); // Change sound to "stone"
                string name = col.gameObject.GetComponent<PlayerMoveControl>().myName;
			}
		}
    }
}