using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace AllMenusUI
{
    public class MainMenuScript : MonoBehaviour
    {

        // Use this for initialization
		public MenuCtrlManager MCM; 
        void Start()
        {
			MCM = FindObjectOfType<MenuCtrlManager>();
        }

        public void OnChooseLevelClick()
		{
			MCM.ButtonClickPlay();
			AllMenusScript.inst.OpenCloseLevels(true);
		}

		public void OnSettingsOpenClick()
		{
			MCM.ButtonClickPlay();
			AllMenusScript.inst.OpenCloseSettings(true);
		}

		public void OnFullExitClick()
		{
			// Exit Application(!)
			MCM.ButtonClickPlay();
			GameManager.inst.ExitGame();
		}
    }
}