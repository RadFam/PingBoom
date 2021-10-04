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
			SetFinalResults(0);
		}
		public void SetFinalResults(int cnt)
		{
			string puckTxt = "";
			Sprite puckImg = null;
			bool openPrize = false;

			if (cnt == 1)
			{
				openPrize = LBC.GetPuckPrize(out puckTxt, out puckImg);
				if (!openPrize)
				{
					LM.StepNewLevel(); // Maybe LM must check another prize pucks
					gameObject.SetActive(false);
				}
				else
				{
					commonFinalText.text = "";
					prizeObjectsPanel.SetActive(true);
				}
			}

			if (cnt == 0)
			{
				LM = FindObjectOfType<LevelManager>();
				commonFinalText.text = "Вы заработали " + LM.GetLevelScore.ToString() + " очков";

				LBC = FindObjectOfType<LevelBonusController>();
				openPrize = LBC.GetPuckPrize(out puckTxt, out puckImg);
				Debug.Log("Try to get prize decision: " + openPrize);
			}

			if (openPrize)
			{
				prizeObjectsPanel.SetActive(true);
				puckDescription.text = "Открыта:\n" + puckTxt;
				puckImage.sprite = puckImg;
			}
			else
			{
				prizeObjectsPanel.SetActive(false);
			}			
		}
        public void OnOkButtonPressed()
		{
			SetFinalResults(1);
			//LM.StepNewLevel(); // Maybe LM must check another prize pucks
			//gameObject.SetActive(false);
		}	
    }
}