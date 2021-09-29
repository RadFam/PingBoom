using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace AllMenusUI
{
    public class PuckChooseScript : MonoBehaviour
    {
		[SerializeField]
		ChoosePuckElementScript chpePrefab;
		[SerializeField]
		GameObject contentArea;

		ChoosePuckElementScript currClicked;
		ChoosePuckElementScript prevClicked;

		int puckChoose;
        // Use this for initialization
        void Start()
        {
			puckChoose = 0;
        }

		public void OnEnable()
		{
			currClicked = null;
			prevClicked = null;

			// Clear contentArea
			foreach(Transform child in contentArea.transform)
			{
				Destroy(child.gameObject);
			}

			// fill contentArea with proper pucks
			int cnt = 0;
			int cnt2 = 0;
			foreach(bool puck in GameManager.inst.pucksUnblocked)
			{
				if (puck) // puck is unlocked
				{
					ChoosePuckElementScript chpe = Instantiate(chpePrefab, contentArea.transform) as ChoosePuckElementScript;
					chpe.SetParams(cnt, cnt2);
					chpe.meWasClicked += OnPuckElementClicked;
					if (cnt2 == 0)
					{
						chpe.OnMeClick();
					}
					cnt2++;
				}
				cnt++;
			}
		}

		void OnPuckElementClicked(ChoosePuckElementScript puck)
		{
			currClicked = puck;
			if (prevClicked != null)
			{
				prevClicked.OnMeDeclick();
			}
			prevClicked = currClicked;
		}

        public void OnCloseMenu()
		{
			puckChoose = currClicked.myRealNum;

			// Get to Know the players puck, what parameters will be applied to it

			this.gameObject.SetActive(false);
		}
    }
}