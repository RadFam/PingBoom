using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class RubberObstacleScript : BaseObstacleScript
    {
		LevelManager levelManager;
        // Use this for initialization
        void Start()
        {
			levelManager = FindObjectOfType<LevelManager>();
        }

        void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.CompareTag("Player"))
			{
				levelManager.PlayEffect(GameManager.EffectSounds.Wood); // Change sound to "rubber"
				string name = col.gameObject.GetComponent<PlayerMoveControl>().myName;
			}
		}
    }
}