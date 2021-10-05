using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using AllMenusUI;

namespace SystemObjects
{
    public class GameManager : MonoBehaviour
    {
		public static GameManager inst;

		public enum EffectSounds {Explosion, Victory, Fail, Concrete, Steel, Wood};

		#region Global system params
        // Use this for initialization
		float musicVol;
		float effectsVol;
		int previousScore;
		[SerializeField]
		int availableLevel;
		bool saveScoreProgress;
		string settingsFilename;
		string settingsFilenamePath;

		Dictionary<string, object> savebleData;
		List<int> everyLevelScores;
		#endregion

		#region Game parameters
		public List<int> everyLevelMaxShoots;
		public int everyLevelMaxGloves;
		public int extraGloves;
		public int everyLevelChangePucks;
		public List<bool> pucksUnblocked;
		#endregion

		public float MusicVol
		{
			get {return musicVol;}
			set {musicVol = value;}
		}
		public float EffectsVol
		{
			get {return effectsVol;}
			set {effectsVol = value;}
		}

		public int PreviousScore
		{
			get {return previousScore;}
			set {previousScore = value;}
		}

		public int AvailableLevel
		{
			get {return availableLevel;}
			set {availableLevel = value;}
		}

		public bool SaveScoreProgress
		{
			get {return saveScoreProgress;}
			set {saveScoreProgress = value;}
		}

		public int CurrentLevelScore
		{
			get {
				int ans = 0;
				for (int i = 0; i < SceneLoaderScript.inst.CurrentScene - 1; ++i)
				{
					ans += everyLevelScores[i];
				}
				return ans;
			}
			set {everyLevelScores[SceneLoaderScript.inst.CurrentScene - 1] = value;}
		}

        void Awake()
        {
			if (inst == null)
			{
				inst = this;
			}
			else
			{
				Destroy(this.gameObject);
			}
			everyLevelScores = new List<int>();
			settingsFilename = "TechSav.ith";

			savebleData = new Dictionary<string, object>();
			savebleData.Add("MusicVol", null);
			savebleData.Add("EffectsVol", null);
			savebleData.Add("ScoreVol", null);
			savebleData.Add("LevelVol", null);
			savebleData.Add("ProcessVol", null);

			// At the end
			LoadData();
        }

        // Update is called once per frame
		public void LoadData()
		{
			settingsFilenamePath = Path.Combine(Application.persistentDataPath, settingsFilename);
            Debug.Log("Save in " + settingsFilenamePath);
            if (File.Exists(settingsFilenamePath))
            {
				Debug.Log("Path is really exists?");
                using (FileStream stream = File.Open(settingsFilenamePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                	savebleData = (Dictionary<string, object>)formatter.Deserialize(stream);
                }

				musicVol = (float)savebleData["MusicVol"];
				effectsVol = (float)savebleData["EffectsVol"];
				availableLevel = (int)savebleData["LevelVol"];
				saveScoreProgress = (bool)savebleData["ProcessVol"];
				if (saveScoreProgress)
				{
					previousScore = (int)savebleData["ScoreVol"];
				}
				else
				{
					previousScore = 0;
				}
				pucksUnblocked.Clear();
				pucksUnblocked = (List<bool>)savebleData["OpenedPucks"];
				everyLevelScores.Clear();
				everyLevelScores = (List<int>)savebleData["EarnedScore"];
            }
			else
			{
				musicVol = 0.5f;
				effectsVol = 0.5f;
				availableLevel = 1;
				previousScore = 0;
				saveScoreProgress = true;
				for (int i = 0; i < 28; ++i)
				{
					everyLevelScores.Add(0);
				}
			}

		}

		public void SaveData()
		{
			savebleData["MusicVol"] = (object)musicVol;
			savebleData["EffectsVol"] = (object)effectsVol;
			if (saveScoreProgress)
			{
				savebleData["ScoreVol"] = (object)previousScore;
			}
			else
			{
				previousScore = 0;
				savebleData["ScoreVol"] = (object)previousScore;
			}
			savebleData["LevelVol"] = (object)availableLevel;
			savebleData["ProcessVol"] = (object)saveScoreProgress;
			savebleData["OpenedPucks"] = (object)pucksUnblocked;
			savebleData["EarnedScore"] = (object)everyLevelScores;

			settingsFilenamePath = Path.Combine(Application.persistentDataPath, settingsFilename);
            //Debug.Log("Save in " + pathFull);
            using (FileStream stream = File.Open(settingsFilenamePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, savebleData);
            }
		}

		public void UpdateSoundSettings()
		{
			LevelManager LM = FindObjectOfType<LevelManager>();
			if (LM)
			{
				LM.UpdateSoundSettings();
			}
			else
			{
				MenuCtrlManager MCM = FindObjectOfType<MenuCtrlManager>();
				if (MCM)
				{
					MCM.UpdateSoundSettings();
				}
			}

			PauseMenuScript PMS = FindObjectOfType<PauseMenuScript>();
			if (PMS)
			{
				PMS.UpdateSoundSettings();
			}
		}

		public void ExitGame()
		{
			SaveData();
			Application.Quit();
		}
    }
}