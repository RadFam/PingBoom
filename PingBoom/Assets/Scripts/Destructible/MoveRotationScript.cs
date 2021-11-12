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
		float waitTime;
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
		float timeToStay;
		bool moveAfterStay;
		

        void Start()
        {
			linearMove = false;
			myRigid = GetComponent<Rigidbody2D>();

            if (pathNum != -1)
            {
                movePointsMove = movePointsPool.MovePath(pathNum);
                if (movePointsMove != null && movePointsMove.childVol > 1)
                {
                    linearMove = true;
                    nextPosition = movePointsMove.GetNextPoint();
                    nextPos = new Vector2(nextPosition.position.x, nextPosition.position.y);
                    moveVct = nextPos - myRigid.position;
                    moveVct.Normalize();
                }
                timeToStay = 0.0f;
				moveAfterStay = true;
            }	

        }

        // Update is called once per frame
        void FixedUpdate()
        {
			myRigid.rotation += rotationSpeed*Time.fixedDeltaTime;

			if (linearMove)
			{
                if (moveAfterStay)
                {
                    myRigid.MovePosition(myRigid.position + moveVct * linearSpeed * Time.fixedDeltaTime);
                    if (Vector2.Distance(myRigid.position, nextPos) < deltaClosePoint)
                    {
						//Debug.Log("myRigid.position: " + myRigid.position + "  nextPos: " + nextPos + "  deltaClosePoint: " + deltaClosePoint);
                        nextPosition = movePointsMove.GetNextPoint();
                        nextPos = new Vector2(nextPosition.position.x, nextPosition.position.y);
                        moveVct = nextPos - myRigid.position;
                        moveVct.Normalize();
						if (waitTime > 0.0f)
						{
							moveAfterStay = false;
							timeToStay = 0.0f;
							//Debug.Log("Move to next point: " + nextPos);
						}
                    }
                }
				else
				{
					timeToStay += Time.deltaTime;
					if (timeToStay >= waitTime)
					{
						timeToStay = 0.0f;
						moveAfterStay = true;
						//Debug.Log("Move after start to: " + nextPos);
					}
				}
			}
        }
    }
}