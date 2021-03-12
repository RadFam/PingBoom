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
			SceneLoaderScript.inst.LoadScene(myNum);
		}
    }
}