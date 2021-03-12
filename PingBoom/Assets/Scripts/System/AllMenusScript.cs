using System.Collections;
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

		public void OnSceneClosed()
		{
			if (startSceneOpen && !levelSceneOpen)
			{
				OnStartSceneClosed();
			}
			if (!startSceneOpen && levelSceneOpen)
			{
				OnLevelSceneClosed();
			}
		}

		public void OnSceneOpened()
		{
			if (!startSceneOpen && levelSceneOpen)
			{
				OnLevelSceneLoaded();
			}
			if (startSceneOpen && !levelSceneOpen)
			{
				OnStartSceneLoaded();
			}
		}
		void OnStartSceneClosed()
		{
			startSceneOpen = false;
			levelSceneOpen = true;
			MMS.gameObject.SetActive(false);
		}
        void OnStartSceneLoaded()
		{
			startSceneOpen = true;
			levelSceneOpen = false;
			MMS.gameObject.SetActive(true);
		}

		void OnLevelSceneClosed()
		{
			levelSceneOpen = false;
			startSceneOpen = true;
			HPS.gameObject.SetActive(false);
		}
		void OnLevelSceneLoaded()
		{
			levelSceneOpen = true;
			startSceneOpen = false;
			HPS.gameObject.SetActive(true);
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
			if (levelSceneOpen)
			{
				SMS.gameObject.SetActive(val);
			}
		}

    }
}