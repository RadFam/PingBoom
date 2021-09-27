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
					//chpe.meWasClicked += OnPuckElementClicked(ChoosePuckElementScript puck);
					cnt2++;
				}
				cnt++;
			}



		}

		void OnPuckElementClicked(ChoosePuckElementScript puck)
		{

		}

        // Update is called once per frame
        public void OnCloseMenu()
		{
			this.gameObject.SetActive(false);
		}
    }
}