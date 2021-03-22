using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllMenusUI;
using GameObjects;
using Controls;
using Effects;


namespace SystemObjects
{
    public class LevelManager : MonoBehaviour
    {
		public AudioSource myAudioEffects;
		public AudioSource myAudioMusic;
		
		public AudioClip explodeEffect;
		public AudioClip victoryEffect;
		public AudioClip failEffect;
		public AudioClip metalEffect;
		int maxShootsCount;
		int leastShootsCount;
		int currentScore;

		int destrObjectsOnScene;

		bool isVictory;
		bool canCheckVictory;

		HeaderPanelScript headerPanelScript;
		EndLevelEffects endLevelEffects;
        // Use this for initialization
        void Start()
        {
			int level = SceneLoaderScript.inst.CurrentScene;
			maxShootsCount = GameManager.inst.everyLevelMaxShoots[level-1];
			leastShootsCount = maxShootsCount;
			currentScore = GameManager.inst.PreviousScore;

			isVictory = false;
			canCheckVictory = true;
			//StartCoroutine(ConnectWithGlobalObject());

			/*
			headerPanelScript = FindObjectOfType<HeaderPanelScript>();
			headerPanelScript.SetInitScore(leastShootsCount, currentScore);

			destrObjectsOnScene = FindObjectsOfType<ExplodeTargetMeta>().Length;
			endLevelEffects = FindObjectOfType<EndLevelEffects>();
			*/
        }

		
		public bool CheckLevelFinished(bool sliding)
		{
            if (canCheckVictory)
            {
                // Win!
                if (destrObjectsOnScene == 0)
                {
                    isVictory = true;
					canCheckVictory = false;
					Debug.Log("CHECK VICTORY destrObjectsOnScene: " + destrObjectsOnScene.ToString());
                    StartCoroutine(FinishLevelCoroutine());
                    return false;
                }

                // Fail!
                if (leastShootsCount == 0 && destrObjectsOnScene > 0 && !sliding)
                {
                    // Start finishing coroutine
					canCheckVictory = false;
                    StartCoroutine(FinishLevelCoroutine());
                    return false;
                }

                // Can proceed next
                if (leastShootsCount > 0 && destrObjectsOnScene > 0)
                {
                    // Do nothing
                    return false;
                }
            }
            return false;
			
		}
		
		public void DecreaseShoot(int shootDecr) // shootDecr can be positive or negative number
		{
			headerPanelScript.SetNewShoots(leastShootsCount, leastShootsCount + shootDecr);
			leastShootsCount += shootDecr;
		}

		public void AddNewScore(int addScore, int numOfDestructed) // addScore and numOfDestructed can be positive or negative numbers(!)
		{
			headerPanelScript.SetNewScore(currentScore, currentScore + addScore);
			currentScore += addScore;
			GameManager.inst.PreviousScore = currentScore;
			destrObjectsOnScene += numOfDestructed;
			Debug.Log("Renewed destrObjectsOnScene: " + destrObjectsOnScene.ToString());
			CheckLevelFinished(true);
		}

		public void StepNewLevel()
		{
			endLevelEffects.CloseAllEffects();
			if (isVictory)
			{
				// Step to next level
				SceneLoaderScript.inst.LoadNextScene();
			}
			else
			{
				// Step to main menu
				SceneLoaderScript.inst.LoadScene(0);
			}
		}
		IEnumerator FinishLevelCoroutine()
		{
			if (isVictory)
			{
				myAudioMusic.Stop();
				myAudioEffects.Stop();
				myAudioEffects.clip = victoryEffect;
				myAudioEffects.Play();
				endLevelEffects.PlayWinFinal(this);
			}
			else
			{
				myAudioMusic.Stop();
				myAudioEffects.Stop();
				myAudioEffects.clip = failEffect;
				myAudioEffects.Play();
				endLevelEffects.PlayFailFinal(this);
			}
			
			yield return null;
		}

		// Make this scene co-load from GameManager Script (!!!)
		IEnumerator ConnectWithGlobalObject()
		{
			yield return new WaitForSeconds(0.1f);

			headerPanelScript = FindObjectOfType<HeaderPanelScript>();
			headerPanelScript.SetInitScore(leastShootsCount, currentScore);

			ExplodeTargetMeta [] tmpDestrObj = FindObjectsOfType<ExplodeTargetMeta>();
			destrObjectsOnScene = FindObjectsOfType<ExplodeTargetMeta>().Length;
			for (int i = 0; i < destrObjectsOnScene; ++i)
			{
				tmpDestrObj[i].levelManager = this;
			}
			endLevelEffects = FindObjectOfType<EndLevelEffects>();
		}

		public void ConnectWithGlobalObjectFunc()
		{
			headerPanelScript = FindObjectOfType<HeaderPanelScript>();
			headerPanelScript.SetInitScore(leastShootsCount, currentScore);

			ExplodeTargetMeta [] tmpDestrObj = FindObjectsOfType<ExplodeTargetMeta>();
			destrObjectsOnScene = FindObjectsOfType<ExplodeTargetMeta>().Length;
			Debug.Log("destrObjectsOnScene: " + destrObjectsOnScene.ToString());
			for (int i = 0; i < destrObjectsOnScene; ++i)
			{
				tmpDestrObj[i].levelManager = this;
			}
			endLevelEffects = FindObjectOfType<EndLevelEffects>();
		}

		public void PlayEffect(GameManager.EffectSounds effect)
		{
			if (effect == GameManager.EffectSounds.Explosion)
			{
				myAudioEffects.clip = explodeEffect;
			}
			if (effect == GameManager.EffectSounds.Steel)
			{
				myAudioEffects.clip = metalEffect;
			}
			myAudioEffects.Play();
		}
    }
}