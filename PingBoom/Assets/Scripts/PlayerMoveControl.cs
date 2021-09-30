using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace GameObjects
{
    public class PlayerMoveControl : MonoBehaviour
    {
		public string myName;

		string myRusName;
		string myRusDescription;
		AudioClip strikePuckSound;
		[SerializeField]
		AudioSource myAudio;
		[SerializeField]
		Rigidbody2D myRigid;
		[SerializeField]
		Collider2D myCollider;
		[SerializeField]
		SpriteRenderer mySpriteR;
		bool isSliding;
		[SerializeField]
		float viscousFriction;
		[SerializeField]
		float minVelocity;
		float timerCheck = 0.1f;
		float timerCnt;
        // Use this for initialization
		LevelManager levelManager;
		PuckObjectsScript pos;

		public bool IsSliding
		{
			get {return isSliding;}
			set {isSliding = value;}
		}
        void Awake()
        {
			//myRigid = gameObject.GetComponent<Rigidbody2D>();
			//myCollider = gameObject.GetComponent<Collider2D>();
			isSliding = false;
			timerCnt = 0.0f;
			pos = Resources.Load<PuckObjectsScript>("ScriptableObjects/PuckContainer");
        }

		void Start()
		{
			levelManager = FindObjectOfType<LevelManager>();
			//myAudio = GetComponent<AudioSource>();
			TakeImagery(0); // By default, we start with the 0 (base) puck
		}

		public void TakeImagery(int puckNum)
		{
			if (isSliding)
			{
				EmergencyStop();
			}

			myName = pos.GetPuckByNum(puckNum).Name;
			myRusName = pos.GetPuckByNum(puckNum).RusName;
			myRusDescription = pos.GetPuckByNum(puckNum).RusDescr;
			mySpriteR.sprite = pos.GetPuckByNum(puckNum).PuckSprite;
			strikePuckSound = pos.GetPuckByNum(puckNum).StrikeSound;
			myCollider.sharedMaterial = pos.GetPuckByNum(puckNum).PuckMat;
			viscousFriction = pos.GetPuckByNum(puckNum).Friction;
			minVelocity = pos.GetPuckByNum(puckNum).StopSpeed;
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
						EmergencyStop();
						levelManager.CheckLevelFinished(false);
					}
					timerCnt = 0.0f;
				}

				// Add viscous friction
				//myRigid.AddForce(new Vector2(myRigid.velocity.x * (-1.0f) * viscousFriction, myRigid.velocity.y * (-1.0f) * viscousFriction));
				//Debug.Log("My velocity: " + myRigid.velocity);
			}
			myRigid.AddForce(new Vector2(myRigid.velocity.x * (-1.0f) * viscousFriction, myRigid.velocity.y * (-1.0f) * viscousFriction));
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

		public void EmergencyStop()
		{
			isSliding = false;
			myRigid.velocity = new Vector2(0.0f, 0.0f);
		}

		public void DeathStop()
		{
			myCollider.enabled = false;
			EmergencyStop();
		}
    }
}