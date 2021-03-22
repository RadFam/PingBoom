using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameObjects;
using SystemObjects;

namespace Controls
{
    public class PlayerTapControl : MonoBehaviour
    {
		Camera cam;
		PlayerMoveControl myPlayerMove;
		ForceTailScript myForceTail;

		Vector2 directionVect;
		float forceValue;
		float forceExpand;

		[SerializeField]
		float strengthForce;

		bool isTapped;
		bool canProceed;

		LevelManager levelManager;

		public bool CanProceed
		{
			get {return canProceed;}
			set {canProceed = value;}
		}
        
        void Start()
        {	
			levelManager = FindObjectOfType<LevelManager>();

			myPlayerMove = gameObject.GetComponent<PlayerMoveControl>();
			myForceTail = transform.GetChild(0).gameObject.GetComponent<ForceTailScript>();
			cam = Camera.main;
			isTapped = false;
			canProceed = true;
        }

        // Update is called once per frame
        void Update()
        {			
			if (Input.GetMouseButtonDown(0))
			{
				PrepareForShoot();
			}
			
			if (Input.GetMouseButton(0))
			{
				AdjustingShoot();
			}
			if (Input.GetMouseButtonUp(0))
			{
				MakeShoot();
			}
        }
		
		void PrepareForShoot()
		{
            if (canProceed && !myPlayerMove.IsSliding)
            {
                RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
                if (hit.collider != null && hit.transform.CompareTag("Player"))
                {
					canProceed = !levelManager.CheckLevelFinished(false);
					if (!canProceed)
					{
						return;
					}
                    isTapped = true;
                    myForceTail.gameObject.SetActive(true);
                }
            }
        }

		void AdjustingShoot()
		{
			if (canProceed && isTapped)
            {
                directionVect = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
                forceExpand = directionVect.magnitude * 4;
                forceValue = directionVect.magnitude * strengthForce;
                directionVect.Normalize();

                myForceTail.SetForceAngle(forceExpand, Vector3.SignedAngle(Vector3.left, directionVect, Vector3.forward));
            }
		}

		void MakeShoot()
		{
			if (canProceed && isTapped)
			{
				myForceTail.gameObject.SetActive(false);
				myPlayerMove.StrikePuck(directionVect, forceValue);
				isTapped = false;
			}
		}
    }
}