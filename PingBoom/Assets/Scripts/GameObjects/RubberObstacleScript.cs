using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class RubberObstacleScript : MonoBehaviour
    {
		LevelManager levelManager;
        // Use this for initialization
        void Start()
        {
			levelManager = FindObjectOfType<LevelManager>();
        }

        void OnCollisionEnter2D(Collision2D col)
		{
			string name = col.gameObject.GetComponent<PlayerMoveControl>().myName;
			if (col.gameObject.CompareTag("Player"))
			{
				levelManager.PlayEffect(GameManager.EffectSounds.Wood); // Change sound to "rubber"
			}
		}
    }
}