using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllMenusUI
{
    public class HeaderPanelScript : MonoBehaviour
    {

        [SerializeField]
		Text lastShoots;
		[SerializeField]
		Text currentScore;

		[SerializeField]
		Text changeShoots;

		int shootsVol;
		int scoreVol;

		Vector3 initShootEffectPos;
		Vector3 initScoreScale;
		Color initScoreColor;
		
		// Use this for initialization
        void Start()
        {

        }

        public void OnGearClick()
		{
			AllMenusScript.inst.OpenClosePause(true);
		}

		public void SetInitScore(int shoots, int score=0)
		{
			lastShoots.text = shoots.ToString();
			currentScore.text = score.ToString();

			initScoreScale = currentScore.transform.localScale;
			initScoreColor = currentScore.color;
		}

		public void SetNewShoots(int oldShoots, int newShoots)
		{
			int val = newShoots - oldShoots;
			lastShoots.text = newShoots.ToString();
			
			// Effect on
			changeShoots.gameObject.SetActive(true);
			initShootEffectPos = changeShoots.transform.localPosition;
			changeShoots.text = val.ToString();
			if (val > 0)
			{
				changeShoots.text = "+" + val.ToString();
			}
			changeShoots.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			StartCoroutine(ChangeShootsCoroutine());
		}

		public void SetNewScore(int oldScore, int newScore)
		{
			// Effect on
			StartCoroutine(ChangeScoreCoroutine(oldScore, newScore));
		}

		IEnumerator ChangeShootsCoroutine()
		{
			Vector3 tempPos = initShootEffectPos;
			Color tmpColor = changeShoots.color;
			float tmpY = tempPos.y;
			float alpha = 1.0f;

			for (int i = 0; i < 100; ++i)
			{
				tmpY -= 2.5f;
				alpha *= 0.95f;
				tempPos = new Vector3(tempPos.x, tmpY, tempPos.z);
				tmpColor = new Color(tmpColor.r, tmpColor.g, tmpColor.b, alpha);

				changeShoots.transform.localPosition = tempPos;
				changeShoots.color = tmpColor;

				yield return new WaitForSeconds(0.01f);	
			}

			yield return null;

			changeShoots.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
			changeShoots.transform.localPosition = initShootEffectPos;
			changeShoots.gameObject.SetActive(false);
		}

		IEnumerator ChangeScoreCoroutine(int oldVal, int newVal)
		{
			Vector3 tempScale = initScoreScale;
			Color tmpColor = initScoreColor;
			float scX = tempScale.x;
			float scY = tempScale.y;
			int deltaVal = (newVal - oldVal) / 50;
			int tmpVal = oldVal;

			for (int i = 0; i < 50; ++i)
			{
				scX *= 1.01f;
				scY *= 1.01f;
				tmpVal += deltaVal;
				
				tempScale = new Vector3(scX, scY, 1.0f);
				currentScore.text = tmpVal.ToString();
				currentScore.transform.localScale = tempScale;

				yield return new WaitForSeconds(0.02f);
			}

			currentScore.text = newVal.ToString();

			for (int i = 0; i < 50; ++i)
			{
				scX /= 1.01f;
				scY /= 1.01f;
				
				tempScale = new Vector3(scX, scY, 1.0f);
				currentScore.transform.localScale = tempScale;

				yield return new WaitForSeconds(0.01f);
			}
			currentScore.transform.localScale = initScoreScale;

			yield return null;
		}
    }
}