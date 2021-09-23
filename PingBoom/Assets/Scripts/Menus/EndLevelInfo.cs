using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SystemObjects;

namespace AllMenusUI
{
    public class EndLevelInfo : MonoBehaviour
    {
		[SerializeField]
		GameObject prizeObjectsPanel;
		[SerializeField]
		Text commonFinalText;
		[SerializeField]
		Text puckDescription;
		[SerializeField]
		Image puckImage;

		LevelManager LM;
		LevelBonusController LBC;
		
        // Use this for initialization
		public void OnEnable()
		{
			SetFinalResults();
		}
		public void SetFinalResults()
		{
			LM = FindObjectOfType<LevelManager>();
			commonFinalText.text = "Вы заработали " + LM.GetLevelScore.ToString() + " очков";

			LBC = FindObjectOfType<LevelBonusController>();
			string puckTxt;
			Sprite puckImg;
			bool openPrize = LBC.GetPuckPrize(out puckTxt, out puckImg);


			if (openPrize)
			{
				prizeObjectsPanel.SetActive(true);
				puckDescription.text = "Открыта:/n" + puckTxt;
				puckImage.sprite = puckImg;
			}
			else
			{
				prizeObjectsPanel.SetActive(false);
			}
		}
        public void OnOkButtonPressed()
		{
			LM.StepNewLevel();
			gameObject.SetActive(false);
		}	
    }
}