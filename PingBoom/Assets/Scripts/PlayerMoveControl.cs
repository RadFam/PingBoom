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
		[SerializeField]
		PuckCanvas myCanvas;
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
		float realFriction;
		float timerCheck = 0.1f;
		float timerCnt;

		int innerLifes;
		bool canCollide;
        // Use this for initialization
		LevelManager levelManager;

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
			canCollide = true;
			timerCnt = 0.0f;
			//pos = Resources.Load<PuckObjectsScript>("ScriptableObjects/PuckContainer");
        }

		void Start()
		{
			levelManager = FindObjectOfType<LevelManager>();
			//myAudio = GetComponent<AudioSource>();
			TakeImagery(0); // By default, we start with the 0 (base) puck
		}

		public void TakeImagery(int puckNum)
		{
			myCanvas.gameObject.SetActive(false);
			if (isSliding)
			{
				EmergencyStop();
			}

			myName = GameManager.inst.allPucks.GetPuckByNum(puckNum).Name;
			myRusName = GameManager.inst.allPucks.GetPuckByNum(puckNum).RusName;
			myRusDescription = GameManager.inst.allPucks.GetPuckByNum(puckNum).RusDescr;
			mySpriteR.sprite = GameManager.inst.allPucks.GetPuckByNum(puckNum).PuckSprite;
			strikePuckSound = GameManager.inst.allPucks.GetPuckByNum(puckNum).StrikeSound;
			myCollider.sharedMaterial = GameManager.inst.allPucks.GetPuckByNum(puckNum).PuckMat;
			viscousFriction = GameManager.inst.allPucks.GetPuckByNum(puckNum).Friction;
			minVelocity = GameManager.inst.allPucks.GetPuckByNum(puckNum).StopSpeed;

			realFriction = viscousFriction;
			innerLifes = 10;
			canCollide = true;

			if (myName == "Concrete")
			{
				myCanvas.gameObject.SetActive(true);
				myCanvas.SetLives(10);
			}
			if (myName == "Ice")
			{
				myCanvas.gameObject.SetActive(true);
				innerLifes = 5;
				myCanvas.SetLives(5);
			}
			if (myName == "Pirate")
			{
				myCanvas.gameObject.SetActive(true);
				innerLifes = 1;
				myCanvas.SetLives(1);
			}
		}

		public void SetSlowing()
		{
			viscousFriction *= 4.0f;
		}
		public void EndSlowing()
		{
			viscousFriction = realFriction;
		}

		public void SetBoosting()
		{
			viscousFriction /= 4.0f;
		}

		public void EndBoosting()
		{
			viscousFriction = realFriction;
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
			Debug.Log("Emergency stop invoked");
			isSliding = false;
			myRigid.velocity = new Vector2(0.0f, 0.0f);
		}

		public void DeathStop()
		{
			myCollider.enabled = false;
			EmergencyStop();
		}

		void OnCollisionEnter2D(Collision2D col)
		{
			if (canCollide)
			{
				innerLifes -= 1;
				if (myName == "Concrete" || myName == "Ice" || myName == "Pirate")
				{
					myCanvas.DecreaseLives();
				}
			}
			if (innerLifes <= 0 && (myName == "Concrete" || myName == "Ice" || myName == "Pirate"))
			{
				canCollide = false;
				EmergencyStop();
				TakeImagery(0);
			}
			
			if (myName == "Tar")
			{
				EmergencyStop();
			}

			// Check material of Obstacle
			if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Steel"))
			{
				myRigid.velocity *= 1.0f;
			}
			if (col.gameObject.CompareTag("Wood"))
			{
				myRigid.velocity *= 0.5f;
			}
			if (col.gameObject.CompareTag("Stone"))
			{
				myRigid.velocity *= 0.7f;
			}
			if (col.gameObject.CompareTag("Rubber"))
			{
				myRigid.velocity *= 2.0f;
			}
			if (col.gameObject.CompareTag("Sand"))
			{
				myRigid.velocity *= 0.2f;
			}
		}
    }
}