using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SystemObjects;


namespace AllMenusUI
{
    public class EndLevelPreInfo : MonoBehaviour
    {
		public string myColor;
        [SerializeField]
		Image strikeCheck;
		[SerializeField]
		Image scoreCheck;
		[SerializeField]
		Image timeCheck;
		[SerializeField]
		Image resultAll;
		[SerializeField]
		GameObject goodText;
		[SerializeField]
		GameObject badText;
		// Use this for initialization
		MenuSpritesScript menuSpritesScript;
		bool coroutinePlayed;

		public void OnEnable()
		{
			menuSpritesScript = Resources.Load<MenuSpritesScript>("ScriptableObjects/MenuSpritesScript");

			strikeCheck.sprite = menuSpritesScript.greyBox;
			scoreCheck.sprite = menuSpritesScript.greyBox;
			timeCheck.sprite = menuSpritesScript.greyBox;
			resultAll.sprite = menuSpritesScript.starResults[0];
			coroutinePlayed = false;
			goodText.SetActive(false);
			badText.SetActive(false);
		}
        public void SetResults(bool strikes, bool scores, bool time, int need)
		{
			StartCoroutine(ShowCoroutine(strikes, scores, time, need));
		}

		IEnumerator ShowCoroutine(bool strikes, bool scores, bool time, int need)
		{
			int cnt = 0;
			yield return new WaitForSeconds(0.2f);

			if (strikes)
			{
				strikeCheck.sprite = menuSpritesScript.blueChkBox;
				cnt++;
				resultAll.sprite = menuSpritesScript.starResults[cnt];
				yield return new WaitForSeconds(0.2f);
			}

			if (scores)
			{
				scoreCheck.sprite = menuSpritesScript.blueChkBox;
				cnt++;
				resultAll.sprite = menuSpritesScript.starResults[cnt];
				yield return new WaitForSeconds(0.2f);
			}

			if (time)
			{
				timeCheck.sprite = menuSpritesScript.blueChkBox;
				cnt++;
				resultAll.sprite = menuSpritesScript.starResults[cnt];
				yield return new WaitForSeconds(0.2f);
			}

			if (cnt >= need)
			{
				goodText.SetActive(true);
			}
			else
			{
				badText.SetActive(true);
				if (need == 1)
				{
					badText.GetComponent<Text>().text = "Увы :(\nДля перехода на следующий уровень вам нужно набрать не меньше 1й звезды из 3х";
				}
				if (need > 1)
				{
					badText.GetComponent<Text>().text = "Увы :(\nДля перехода на следующий уровень вам нужно набрать не меньше " + need.ToString() + "х звезд из 3х";
				}
			}
			yield return new WaitForSeconds(0.2f);

			coroutinePlayed = true;
		}
    }
}