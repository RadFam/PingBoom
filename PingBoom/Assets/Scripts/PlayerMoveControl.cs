using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class PlayerMoveControl : MonoBehaviour
    {
		AudioClip strikePuckSound;
		AudioSource myAudio;
		Rigidbody2D myRigid;
		Collider2D myCollider;
		bool isSliding;
		[SerializeField]
		float viscousFriction;
		[SerializeField]
		float minVelocity;
		float timerCheck = 0.1f;
		float timerCnt;
        // Use this for initialization
		LevelManager levelManager;

		public bool IsSliding
		{
			get {return isSliding;}
			set {isSliding = value;}
		}
        void Awake()
        {
			myRigid = gameObject.GetComponent<Rigidbody2D>();
			myCollider = gameObject.GetComponent<Collider2D>();
			isSliding = false;
			timerCnt = 0.0f;
        }

		void Start()
		{
			levelManager = FindObjectOfType<LevelManager>();
			myAudio = GetComponent<AudioSource>();
		}

        // Update is called once per frame
        void FixedUpdate()
        {
			if (isSliding)
			{
				timerCnt += Time.deltaTime;
				if (timerCnt >= timerCheck)
				{
					if (myRigid.velocity.magnitude <= minVelocity)
					{
						myRigid.velocity = new Vector2(0.0f, 0.0f);
						isSliding = false;
						levelManager.CheckLevelFinished(false);
					}
					timerCnt = 0.0f;
				}

				// Add viscous friction
				myRigid.AddForce(new Vector2(myRigid.velocity.x * (-1.0f) * viscousFriction, myRigid.velocity.y * (-1.0f) * viscousFriction));
			}
        }

		public void StrikePuck(Vector2 direction, float force)
		{
			if (!isSliding)
			{
				myRigid.AddForce(direction * force, ForceMode2D.Impulse);
				myAudio.Play();
				timerCnt = 0.0f;
				isSliding = true;
				levelManager.DecreaseShoot(-1);
			}
		}

		public void DeathStop()
		{
			myCollider.enabled = false;
			isSliding = false;
			myRigid.velocity = new Vector2(0.0f, 0.0f);
		}
    }
}