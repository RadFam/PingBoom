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
		int maxShootsCount;
		int leastShootsCount;
		int currentScore;

		int destrObjectsOnScene;

		bool isVictory;

		HeaderPanelScript headerPanelScript;
		EndLevelEffects endLevelEffects;
        // Use this for initialization
        void Start()
        {
			int level = SceneLoaderScript.inst.CurrentScene;
			maxShootsCount = GameManager.inst.everyLevelMaxShoots[level-1];
			leastShootsCount = maxShootsCount;
			currentScore = GameManager.inst.PreviousScore;

			headerPanelScript = FindObjectOfType<HeaderPanelScript>();
			headerPanelScript.SetInitScore(leastShootsCount, currentScore);

			destrObjectsOnScene = FindObjectsOfType<ExplodeTargetMeta>().Length;
			isVictory = false;

			endLevelEffects = FindObjectOfType<EndLevelEffects>();
        }

		
		public bool CheckLevelFinished()
		{
			// Win!
			if (destrObjectsOnScene == 0)
			{
				isVictory = true;
				StartCoroutine(FinishLevelCoroutine());
				return false;
			}

			// Fail!
			if (leastShootsCount == 0 && destrObjectsOnScene > 0)
			{
				// Start finishing coroutine
				StartCoroutine(FinishLevelCoroutine());
				return false;
			}

			// Can proceed next
			if (leastShootsCount > 0 && destrObjectsOnScene > 0)
			{
				// Do nothing
				return false;
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
			headerPanelScript.SetNewShoots(currentScore, currentScore + addScore);
			currentScore += addScore;
			GameManager.inst.PreviousScore = currentScore;
			destrObjectsOnScene += numOfDestructed;
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
				endLevelEffects.PlayWinFinal(this);
			}
			else
			{
				endLevelEffects.PlayFailFinal(this);
			}
			
			yield return null;
		}
    }
}