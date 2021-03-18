using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

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
			SceneLoaderScript.inst.LoadScene(0);
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