﻿using UnityEngine;
using GameObjects;
using AllMenusUI;
using SystemObjects;

namespace Controls
{
    public class FieldClickController : MonoBehaviour
    {
		[SerializeField]
		Camera cam;

		[SerializeField]
		PlayerMoveControl playerMoveControl;
		[SerializeField]
		CatchingGloveScript glovePrefab;
		BottomPanelScript bottomPanel;

		bool canPlaceGlove;
		
        // Use this for initialization
        void Start()
        {
			canPlaceGlove = false;
        }

        // Update is called once per frame
        void Update()
        {
			if (Input.GetMouseButtonUp(0))
			{
				CreateGlove();
			}
        }

		public void GlobalPreparation()
		{
			bottomPanel = FindObjectOfType<BottomPanelScript>();
			bottomPanel.GlovePressedTrue += CanPlaceGlove;
			bottomPanel.GlovePressedFalse += CanNotPlaceGlove;

			// Set count of gloves
			int allGloves = GameManager.inst.everyLevelMaxGloves + GameManager.inst.extraGloves;
			bottomPanel.SetParameters(GameManager.inst.everyLevelChangePucks, allGloves);
		}

		void CanPlaceGlove()
		{
			canPlaceGlove = true;
		}

		void CanNotPlaceGlove()
		{
			canPlaceGlove = false;
		}

		void CreateGlove()
		{
			if (playerMoveControl.IsSliding && canPlaceGlove) // also check, if we previously check the glove on menu
			{
				Vector3 touchPlace = cam.ScreenToWorldPoint(Input.mousePosition);
				// Check if there is no any GO colliders
				RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
				if (hit.collider == null)
				{
					CatchingGloveScript cgs = Instantiate(glovePrefab, touchPlace, Quaternion.identity);
					cgs.SetPlayerCtrl(playerMoveControl);
					bottomPanel.SetGloveStatus(false, -1);
				}
			}
		}
    }
}