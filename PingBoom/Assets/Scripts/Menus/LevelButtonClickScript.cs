using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace AllMenusUI
{
    public class LevelButtonClickScript : MonoBehaviour
    {

        [SerializeField]
		int myNum;

        // Use this for initialization
		public void OnClick()
		{
            MenuCtrlManager MCM = FindObjectOfType<MenuCtrlManager>();
            if (MCM)
            {
                MCM.ButtonClickPlay();
            }
			Invoke("LoadSceneFunc", 0.2f);
		}

        void LoadSceneFunc()
        {
            SceneLoaderScript.inst.LoadScene(myNum);
        }
    }
}