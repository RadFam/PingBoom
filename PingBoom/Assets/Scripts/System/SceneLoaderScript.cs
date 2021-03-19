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

		public enum ScenesNames {MenuScene, GameScene, GameScene2, GameScene3}
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

			Debug.Log("BFR currentScene: " + currentScene.ToString());
			Debug.Log("BFR sceneToLoad: " + sceneToLoad.ToString());

			if (currentScene != sceneToLoad)
			{
				StartCoroutine(LoadGameScene(false));
			}
		}

		public void LoadNextScene()
		{
			if (currentScene >= 1 && currentScene < 28)
			{
				sceneToLoad = currentScene + 1;
				StartCoroutine(LoadGameScene(true));
			}
			if (currentScene == 28)
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

			//currentScene = sceneToLoad;
			Debug.Log("currentScene: " + currentScene.ToString());
			Debug.Log("sceneToLoad: " + sceneToLoad.ToString());

			yield return StartCoroutine(FadeOut());
			AllMenusScript.inst.OnSceneOpened(val);
		}

		IEnumerator FadeIn()
		{
			yield return null;
		}

		IEnumerator FadeOut()
		{
			yield return null;
		}
    }
}