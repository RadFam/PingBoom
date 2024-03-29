﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllMenusUI
{
    public class BottomPanelScript : MonoBehaviour
    {

        [SerializeField]
		Text puckChangesCount;
		[SerializeField]
		Text gloveChangesCount;
		[SerializeField]
		Image gloveImage;
		[SerializeField]
		List<Sprite> gloveSprites;

		public Action GlovePressedTrue;
		public Action GlovePressedFalse;

		int puckChanges;
		int gloveChanges;

		bool glovePressed;
        void Start()
        {
			glovePressed = false;
        }

		public void SetParameters(int pucks, int gloves)
		{
			SetPucks(pucks);
			SetGloves(gloves);
		}
		public void SetPucks(int pucks)
		{
			puckChanges = pucks;
			puckChangesCount.text = "x" + puckChanges.ToString();
		}
		public void SetGloves(int gloves)
		{
			gloveChanges = gloves;
			gloveChangesCount.text = "x" + gloveChanges.ToString();
		}
        public void OnGloveClick()
		{
			if (gloveChanges > 0)
			{
				ChangeGloveStatus();		
			}
		}

		public void OnChangePuckClick()
		{
			if (puckChanges > 0)
			{
				AllMenusScript.inst.OpenClosePuckMenu(true);
			}
		}

		void ChangeGloveStatus()
		{
			glovePressed = !glovePressed;
			
			if (!glovePressed)
			{
				GlovePressedFalse();
				gloveImage.sprite = gloveSprites[0];
			}
			else
			{
				GlovePressedTrue();
				gloveImage.sprite = gloveSprites[1];
			}
		}

		public void SetGloveStatus(bool val, int del)
		{
			glovePressed = val;
			if (!glovePressed)
			{
				GlovePressedFalse();
				gloveImage.sprite = gloveSprites[0];
			}
			else
			{
				GlovePressedTrue();
				gloveImage.sprite = gloveSprites[1];
			}

			gloveChanges += del;
			gloveChangesCount.text = "x" + gloveChanges.ToString();
		}
    }
}