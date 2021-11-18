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
		public AudioClip woodEffect;
		public AudioClip concreteEffect;
		[SerializeField]
		FlowScoresPool scoreEffects;
		[SerializeField]
		LevelInfoPanel levelInfoHints;
		[SerializeField]
		FieldClickController fieldClickController;
		int level;
		int maxShootsCount;
		int leastShootsCount;
		int currentScore;
		int currentLevelScore;
		int gloveCount;
		int puckChangesCount;
		float timeOfPlay;

		int destrObjectsOnScene;

		bool isVictory;
		public bool IsVictory
		{
			get {return isVictory;}
		}
		bool canCheckVictory;

		public int GetLevelScore
		{
			get {return currentLevelScore;}
		}
		public int GetFullScore
		{
			get {return currentScore;}
		}

		HeaderPanelScript headerPanelScript;
		BottomPanelScript bottomPanelScript;
		EndLevelEffects endLevelEffects;
		LevelBonusController levelBonusController;

		PlayerMoveControl player;
		PlayerTapControl playerTap;
        // Use this for initialization
        void Start()
        {
			level = SceneLoaderScript.inst.CurrentScene;
			maxShootsCount = GameManager.inst.everyLevelMaxShoots[level-1];
			leastShootsCount = maxShootsCount;
			currentScore = GameManager.inst.CurrentLevelScore; //PreviousScore;
			currentLevelScore = 0;
			puckChangesCount = GameManager.inst.CurrentLevelPucks;
			gloveCount = GameManager.inst.CurrentLevelGloves + GameManager.inst.extraGloves;

			GameManager.inst.AvailableLevel = Mathf.Max(level, GameManager.inst.AvailableLevel);

			isVictory = false;
			canCheckVictory = true;

			player = FindObjectOfType<PlayerMoveControl>();
			playerTap = FindObjectOfType<PlayerTapControl>();

			// On load scene make correct sound and effects volume
			myAudioEffects.volume = GameManager.inst.EffectsVol;
			myAudioMusic.volume = GameManager.inst.MusicVol;

			timeOfPlay = 0.0f;

			Debug.Log("Current Screen resolution: " + Screen.currentResolution);
        }

		void Update()
		{
			timeOfPlay += Time.deltaTime;
		}

		
		public bool CheckLevelFinished(bool sliding)
		{
            if (canCheckVictory)
            {
                // Win!
                if (destrObjectsOnScene == 0)
                {
					myAudioEffects.Stop();
                    isVictory = true;
					canCheckVictory = false;
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

		public void AddNewScore(int addScore, int numOfDestructed, Vector3 pos) // addScore and numOfDestructed can be positive or negative numbers(!)
		{
			if (scoreEffects.freeScores.Count > 0)
			{
				TextScoreFlowEffect tsfe = scoreEffects.freeScores[0];
				tsfe.gameObject.SetActive(true);
				tsfe.SetInitData(addScore, new Color(1.0f, 1.0f, 1.0f, 1.0f), pos);
			}
			headerPanelScript.SetNewScore(currentScore, currentScore + addScore);
			currentScore += addScore;
			currentLevelScore += addScore;
			// GameManager.inst.PreviousScore = currentScore;
			//Debug.Log("Add new destructed objects: " + numOfDestructed);
			destrObjectsOnScene += numOfDestructed;
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
			playerTap.CanProceed = false;
			// Star check
			bool res = levelBonusController.MakeWalkThroughDecision(level, (maxShootsCount-leastShootsCount), currentLevelScore, timeOfPlay);
			if (!res)
			{
				isVictory = false;
			}
			// Bonus check
			levelBonusController.MakePrizeDecision(level, currentLevelScore, currentScore, 1.0f*leastShootsCount/maxShootsCount);

			// Play Final Results
			if (isVictory)
			{
				GameManager.inst.CurrentLevelScore = currentLevelScore;
				myAudioMusic.Stop();
				//Debug.Log("Try to stop effect begin");
				myAudioEffects.Stop();
				//Debug.Log("Try to stop effect end");
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

		public void ConnectWithGlobalObjectFunc()
		{
			if (levelInfoHints != null)
			{
				levelInfoHints.gameObject.SetActive(true);
			}

			headerPanelScript = FindObjectOfType<HeaderPanelScript>();
			headerPanelScript.SetInitScore(leastShootsCount, currentScore);
			bottomPanelScript = FindObjectOfType<BottomPanelScript>();
			bottomPanelScript.SetParameters(puckChangesCount, gloveCount);

			fieldClickController.GlobalPreparation();

			ExplodeTargetMeta [] tmpDestrObj = FindObjectsOfType<ExplodeTargetMeta>();
			destrObjectsOnScene = FindObjectsOfType<ExplodeTargetMeta>().Length;

			for (int i = 0; i < destrObjectsOnScene; ++i)
			{
				tmpDestrObj[i].levelManager = this;
			}
			
			endLevelEffects = gameObject.GetComponent<EndLevelEffects>();
			levelBonusController = gameObject.GetComponent<LevelBonusController>();

			if (levelInfoHints == null)
			{
				playerTap.CanProceed = true;
			}
		}

		public void ChangeGloveCount(int change)
		{
			gloveCount += change;
		}

		public void ChangePuckCount(int change)
		{
			puckChangesCount += change;
			bottomPanelScript.SetPucks(puckChangesCount);
		}

		public void StartLevelStage()
		{
			playerTap.CanProceed = true;
		}

		public void PlayEffect(GameManager.EffectSounds effect)
		{
			if (canCheckVictory)
			{
				if (effect == GameManager.EffectSounds.Explosion)
				{
					//Debug.Log("Explode starts");
					myAudioEffects.clip = explodeEffect;
				}
				if (effect == GameManager.EffectSounds.Steel)
				{
					myAudioEffects.clip = metalEffect;
				}
				if (effect == GameManager.EffectSounds.Wood)
				{
					myAudioEffects.clip = woodEffect;
				}
				if (effect == GameManager.EffectSounds.Concrete)
				{
					myAudioEffects.clip = concreteEffect;
				}
				myAudioEffects.Play();
			}
		}

		public void UpdateSoundSettings()
		{
			myAudioEffects.volume = GameManager.inst.EffectsVol;
			myAudioMusic.volume = GameManager.inst.MusicVol;
		}

		public void PlayerIsDead()
		{
			player.DeathStop();
			isVictory = false;
			canCheckVictory = false;
			StartCoroutine(FinishLevelCoroutine());
		}
    }
}