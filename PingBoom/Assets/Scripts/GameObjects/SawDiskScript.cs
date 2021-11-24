using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class SawDiskScript : BaseObstacleScript
    {
		[SerializeField]
		float rotationSpeed;
		LevelManager levelManager;
        // Use this for initialization
        void Start()
        {
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
				col.gameObject.GetComponent<PlayerMoveControl>().DeathStop();
				levelManager.PlayEffect(GameManager.EffectSounds.Steel);
				levelManager.PlayerIsDead();
			}
		}
    }
}