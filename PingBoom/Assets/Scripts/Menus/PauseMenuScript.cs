using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMenusUI
{
    public class PauseMenuScript : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        public void OnMainMenuClick()
		{

		}

		public void OnSettingsClick()
		{
			AllMenusScript.inst.OpenCloseSettings(true);
		}

		public void OnProceedClick()
		{
			gameObject.SetActive(false);
		}
    }
}