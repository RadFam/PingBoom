using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemObjects;

namespace AllMenusUI
{
    public class LevelInfoPanel : MonoBehaviour
    {
		public void OnOkButtonPressed()
		{
			LevelManager LM = FindObjectOfType<LevelManager>();
			LM.StartLevelStage();
			gameObject.SetActive(false);
		}		
    }
}