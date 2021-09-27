using System;
using UnityEngine;
using UnityEngine.UI;

namespace AllMenusUI
{
    public class ChoosePuckElementScript : MonoBehaviour
    {
		public int myNum;
		public int myRealNum;
		public Action<ChoosePuckElementScript> meWasClicked;
		[SerializeField]
		Image myImage;

		public void SetParams(int num, int realNum)
		{
			myNum = num;
			myRealNum = realNum;
			// ......
			// Get from SO sprite of pusk based on realNum argument
		}

		public void OnMeClick()
		{
			gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
			meWasClicked(this);
		}

		public void OnMeDeclick()
		{
			gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}
    }
}