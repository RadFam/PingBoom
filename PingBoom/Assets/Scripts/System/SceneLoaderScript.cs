using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AllMenusUI;

namespace SystemObjects
{
    public class SceneLoaderScript : MonoBehaviour
    {
		public static SceneLoaderScript inst;

		public enum ScenesNames {MenuScene, GameScene, GameScene2, GameScene3, GameScene4, GameScene5, GameScene6, GameScene7, GameScene8, GameScene9, GameScene10, GameScene11,
		GameScene12, GameScene13, GameScene14, GameScene15, GameScene16, GameScene17, GameScene18, GameScene19, GameScene20, GameScene21, GameScene22, GameScene23, GameScene24,
		GameScene25, GameScene26, GameScene27, GameScene28};
		[SerializeField]
		int maxFieldNum;
		[SerializeField]
		int currentScene;
		[SerializeField]
		int sceneToLoad;

		[SerializeField]
        Image fadeImage;
        [SerializeField]
        float fadeInterval;
        [SerializeField]
        float fadeAlpha;

		public int CurrentScene
		{
			get {return currentScene;}
		}

		void Awake()
		{
			currentScene = 0;
			sceneToLoad = 0;
		}

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
        }

		public void LoadScene(int sceneNum)
		{
			sceneToLoad = sceneNum;

			if (currentScene != sceneToLoad)
			{
				StartCoroutine(LoadGameScene(false));
			}
		}

		public void LoadNextScene()
		{
			Debug.Log("SCLS - currentScene: " + currentScene + "  maxFieldNum: " + maxFieldNum);
			if (currentScene >= 1 && currentScene < maxFieldNum)
			{
				sceneToLoad = currentScene + 1;
				StartCoroutine(LoadGameScene(true));
			}
			if (currentScene == maxFieldNum)
			{
				sceneToLoad = 0;
				StartCoroutine(LoadGameScene(false));
			}
		}

		IEnumerator LoadGameScene(bool val)
		{
			AllMenusScript.inst.OnSceneClosed(val);
			yield return StartCoroutine(FadeIn());

			currentScene = sceneToLoad;
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(((ScenesNames)sceneToLoad).ToString("g"), LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

			yield return StartCoroutine(FadeOut());
			AllMenusScript.inst.OnSceneOpened(val);

			if (currentScene > 0)
			{
				LevelManager LM = FindObjectOfType<LevelManager>();
				LM.ConnectWithGlobalObjectFunc();
			}
		}

		IEnumerator FadeIn()
		{
			fadeAlpha = 0.0f;
			Color blackC = Color.black;

			for (int i = 0; i < 25; ++i)
			{
				blackC = new Color (0.0f, 0.0f, 0.0f, fadeAlpha);
				fadeAlpha += 0.04f;
				fadeImage.color = blackC;
				yield return new WaitForSeconds(0.01f);
			}

			blackC = new Color (0.0f, 0.0f, 0.0f, 1.0f);
			fadeImage.color = blackC;

			yield return null;
		}

		IEnumerator FadeOut()
		{
			fadeAlpha = 1.0f;
			Color blackC = Color.black;

			for (int i = 0; i < 25; ++i)
			{
				blackC = new Color (0.0f, 0.0f, 0.0f, fadeAlpha);
				fadeAlpha -= 0.04f;
				fadeImage.color = blackC;
				yield return new WaitForSeconds(0.01f);
			}

			blackC = new Color (0.0f, 0.0f, 0.0f, 0.0f);
			fadeImage.color = blackC;

			yield return null;
		}
    }
}