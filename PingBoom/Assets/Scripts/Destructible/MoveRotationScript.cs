using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class MoveRotationScript : MonoBehaviour
    {

        // Use this for initialization
        [SerializeField]
        float rotationSpeed;

		[SerializeField]
		float linearSpeed;
		[SerializeField]
		MovePointsPool movePointsPool;

		[SerializeField]
		int pathNum;

		[SerializeField]
		float deltaClosePoint;
		MovePointsMove movePointsMove;

		bool linearMove;

		Rigidbody2D myRigid;
		Transform nextPosition;
		Vector2 nextPos;
		Vector2 moveVct;
		

        void Start()
        {
			linearMove = false;
			myRigid = GetComponent<Rigidbody2D>();

			movePointsMove = movePointsPool.MovePath(pathNum);
			if (movePointsMove != null && movePointsMove.childVol > 1)
			{
				linearMove = true;
				nextPosition = movePointsMove.GetNextPoint();
				nextPos = new Vector2(nextPosition.position.x, nextPosition.position.y);
				moveVct = nextPos - myRigid.position;
				moveVct.Normalize();
			}			
			Debug.Log("myRigid: " + myRigid);

        }

        // Update is called once per frame
        void FixedUpdate()
        {
			myRigid.rotation += rotationSpeed*Time.fixedDeltaTime;

			if (linearMove)
			{
				myRigid.MovePosition(myRigid.position + moveVct * linearSpeed * Time.fixedDeltaTime);
				if (Vector2.Distance(myRigid.position, nextPos) < deltaClosePoint)
				{
					nextPosition = movePointsMove.GetNextPoint();
					nextPos = new Vector2(nextPosition.position.x, nextPosition.position.y);
					moveVct = nextPos - myRigid.position;
					moveVct.Normalize();
				}
			}
        }
    }
}