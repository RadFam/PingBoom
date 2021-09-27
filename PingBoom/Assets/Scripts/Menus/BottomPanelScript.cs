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

		int puckChanges;
		int gloveChanges;
        void Start()
        {

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
			if (puckChanges >= 0)
			{

			}
		}

		public void OnChangePuckClick()
		{
			if (gloveChanges >= 0)
			{
				
			}
		}
    }
}