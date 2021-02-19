using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameObjects;

namespace Controls
{
    public class CommonClickControl : MonoBehaviour
    {
		Camera cam;
		PlayerMoveControl playerMoveControl;

		Vector2 mouseClick;
		Vector2 playerClick;

		Vector2 directionVect;
		float forceValue;

		[SerializeField]
		float strengthForce;
        // Use this for initialization
        void Awake()
        {
			playerMoveControl = GameObject.Find("Player").GetComponent<PlayerMoveControl>();
			cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
			if (Input.GetMouseButtonUp(0))
			{
				// Get coordinates of mouse
				mouseClick = cam.ScreenToWorldPoint(Input.mousePosition);
				playerClick = playerMoveControl.transform.position;

				directionVect = playerClick - mouseClick;
				forceValue = directionVect.magnitude * strengthForce;
				directionVect.Normalize();

				playerMoveControl.StrikePuck(directionVect, forceValue);
			}
        }
    }
}