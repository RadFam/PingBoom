﻿using System.Collections;
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
		int maxShootsCount;
		int leastShootsCount;
		int currentScore;

		int destrObjectsOnScene;

		bool isVictory;
		bool canCheckVictory;

		HeaderPanelScript headerPanelScript;
		EndLevelEffects endLevelEffects;

		PlayerMoveControl player;
		PlayerTapControl playerTap;
        // Use this for initialization
        void Start()
        {
			int level = SceneLoaderScript.inst.CurrentScene;
			maxShootsCount = GameManager.inst.everyLevelMaxShoots[level-1];
			leastShootsCount = maxShootsCount;
			currentScore = GameManager.inst.PreviousScore;

			GameManager.inst.AvailableLevel = Mathf.Max(level, GameManager.inst.AvailableLevel);

			isVictory = false;
			canCheckVictory = true;

			player = FindObjectOfType<PlayerMoveControl>();
			playerTap = FindObjectOfType<PlayerTapControl>();

			// On load scene make correct sound and effects volume
			myAudioEffects.volume = GameManager.inst.EffectsVol;
			myAudioMusic.volume = GameManager.inst.MusicVol;
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

		public void AddNewScore(int addScore, int numOfDestructed) // addScore and numOfDestructed can be positive or negative numbers(!)
		{
			headerPanelScript.SetNewScore(currentScore, currentScore + addScore);
			currentScore += addScore;
			GameManager.inst.PreviousScore = currentScore;
			Debug.Log("Add new destructed objects: " + numOfDestructed);
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
			if (isVictory)
			{
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
			headerPanelScript = FindObjectOfType<HeaderPanelScript>();
			headerPanelScript.SetInitScore(leastShootsCount, currentScore);

			ExplodeTargetMeta [] tmpDestrObj = FindObjectsOfType<ExplodeTargetMeta>();
			destrObjectsOnScene = FindObjectsOfType<ExplodeTargetMeta>().Length;
			Debug.Log("Destructible objects: " + destrObjectsOnScene);
			for (int i = 0; i < destrObjectsOnScene; ++i)
			{
				tmpDestrObj[i].levelManager = this;
			}
			endLevelEffects = FindObjectOfType<EndLevelEffects>();
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