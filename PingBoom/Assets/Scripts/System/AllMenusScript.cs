﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SystemObjects;


namespace AllMenusUI
{
    public class AllMenusScript : MonoBehaviour
    {
		public static AllMenusScript inst;

		[SerializeField]
		HeaderPanelScript HPS;
		[SerializeField]
		BottomPanelScript BPS;
		[SerializeField]
		PuckChooseScript PCS;
		[SerializeField]
		LevelMenuScript LMS;
		[SerializeField]
		MainMenuScript MMS;
		[SerializeField]
		PauseMenuScript PMS;
		[SerializeField]
		SettingsMenuScript SMS;

		bool startSceneOpen;
		bool levelSceneOpen;	
        // Use this for initialization
        void Start()
        {
			if (inst == null)
			{
				inst = this;
			}
			else
			{
				Destroy(this.gameObject);
			}

			startSceneOpen = true;
			levelSceneOpen = false;
        }

		public void OnSceneClosed(bool val)
		{
            if (!val)
            {
                if (startSceneOpen && !levelSceneOpen)
                {
                    OnStartSceneClosed();
                }
                else if (!startSceneOpen && levelSceneOpen)
                {
                    OnLevelSceneClosed();
                }
            }
			else
			{
				OnLevelSceneClosed();
			}
        }

		public void OnSceneOpened(bool val)
        {
            if (!val)
            {
                if (!startSceneOpen && levelSceneOpen)
                {
                    OnLevelSceneLoaded();
                }
                else if (startSceneOpen && !levelSceneOpen)
                {
                    OnStartSceneLoaded();
                }
            }
			else
			{
				OnLevelSceneLoaded();
			}
		}
		void OnStartSceneClosed()
		{
			MenuCtrlManager MCM = FindObjectOfType<MenuCtrlManager>();
			MCM.StopAllPlay();

			startSceneOpen = false;
			levelSceneOpen = true;
			MMS.gameObject.SetActive(false);
			SMS.gameObject.SetActive(false);
			LMS.gameObject.SetActive(false);
		}
        void OnStartSceneLoaded()
		{
			startSceneOpen = true;
			levelSceneOpen = false;
			MMS.gameObject.SetActive(true);

			MMS.MCM = FindObjectOfType<MenuCtrlManager>();
		}

		void OnLevelSceneClosed()
		{
			levelSceneOpen = false;
			startSceneOpen = true;
			HPS.gameObject.SetActive(false);
			BPS.gameObject.SetActive(false);
			PCS.gameObject.SetActive(false);
			SMS.gameObject.SetActive(false);
			PMS.gameObject.SetActive(false);
		}
		void OnLevelSceneLoaded()
		{
			levelSceneOpen = true;
			startSceneOpen = false;
			HPS.gameObject.SetActive(true);
			BPS.gameObject.SetActive(true);
			PCS.gameObject.SetActive(false);
		}

		public void OpenCloseLevels(bool val)
		{
			if (startSceneOpen)
			{
				LMS.gameObject.SetActive(val);
			}
		}

		public void OpenClosePause(bool val)
		{
			if (levelSceneOpen)
			{
				PMS.gameObject.SetActive(val);
			}
		}

		public void OpenCloseSettings(bool val)
		{
			SMS.gameObject.SetActive(val);
		}

		public void OpenClosePuckMenu(bool val)
		{
			PCS.gameObject.SetActive(val);
		}

    }
}