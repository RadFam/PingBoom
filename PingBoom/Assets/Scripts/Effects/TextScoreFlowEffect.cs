using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    public class TextScoreFlowEffect : MonoBehaviour
    {
		[SerializeField]
		FlowScoresPool flowScoresPool;
		[SerializeField]
		Text textScore;
		float alphaVol;
		float delta;
        // Use this for initialization
        void Start()
        {
			alphaVol = 1.0f;
			delta = 1.0f;
        }

        public void OnEnable()
		{
			flowScoresPool.busyScore.Add(this);
			flowScoresPool.freeScores.RemoveAt(0);
			alphaVol = 1.0f;
			delta = 1.0f;
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}
		public void SetInitData(int score, Color color, Vector3 pos)
		{
			textScore.text = score.ToString();
			transform.position = pos;

			StartCoroutine(FlowCoroutine());
		}

		IEnumerator FlowCoroutine()
		{
			float scale = 1.0f;
			float y_pos = transform.position.y;

			//Debug.Log("Init flow score coords: " + transform.position);

			while (alphaVol > 0.2f)
			{
				y_pos += 0.1f * delta;
				scale *= (1.0f + delta/40.0f);
				alphaVol -= (1.0f - delta)*0.05f;

				//Debug.Log("y_pos: " + y_pos + "  scale: " + scale + "  alphaVol: " + alphaVol);

				transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
				transform.localScale = new Vector3(scale, scale, 1.0f);
				textScore.color = new Color(textScore.color.r, textScore.color.g, textScore.color.b, alphaVol);

				delta -= 0.05f;
				yield return new WaitForEndOfFrame();
			}

			EndShow();
			yield return null;
		}

		void EndShow()
		{
			flowScoresPool.freeScores.Add(this);
			flowScoresPool.busyScore.RemoveAt(0);
			gameObject.SetActive(false);
		}
    }
}