using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class PlayerMoveControl : MonoBehaviour
    {
		Rigidbody2D myRigid;
		bool isSliding;
		[SerializeField]
		float viscousFriction;
		float minVelocity;
		float timerCheck = 0.1f;
		float timerCnt;
        // Use this for initialization
        void Awake()
        {
			myRigid = gameObject.GetComponent<Rigidbody2D>();
			isSliding = false;
			timerCnt = 0.0f;
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
				timerCnt = 0.0f;
				isSliding = true;
			}
		}
    }
}