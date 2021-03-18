using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace AllMenusUI
{
    public class MainMenuScript : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        public void OnChooseLevelClick()
		{
			AllMenusScript.inst.OpenCloseLevels(true);
		}

		public void OnSettingsOpenClick()
		{
			AllMenusScript.inst.OpenCloseSettings(true);
		}

		public void OnFullExitClick()
		{
			// Exit Application(!)
			GameManager.inst.ExitGame();
		}
    }
}