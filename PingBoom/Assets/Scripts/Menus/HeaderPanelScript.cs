using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllMenusUI
{
    public class HeaderPanelScript : MonoBehaviour
    {

        [SerializeField]
		Text lastShoots;
		[SerializeField]
		Text currentScore;

		int shootsVol;
		int scoreVol;
		
		// Use this for initialization
        void Start()
        {

        }

        public void OnGearClick()
		{
			AllMenusScript.inst.OpenClosePause(true);
		}
    }
}