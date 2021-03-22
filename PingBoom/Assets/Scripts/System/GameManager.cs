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
		#endregion

		#region Game parameters
		public List<int> everyLevelMaxShoots;
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
            //Debug.Log("Save in " + pathFull);
            if (File.Exists(settingsFilenamePath))
            {
                using (FileStream stream = File.Open(settingsFilenamePath, FileMode.Create))
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
            }
			else
			{
				musicVol = 1.0f;
				effectsVol = 1.0f;
				availableLevel = 5;
				previousScore = 0;
				saveScoreProgress = true;
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

			settingsFilenamePath = Path.Combine(Application.persistentDataPath, settingsFilename);
            //Debug.Log("Save in " + pathFull);
            using (FileStream stream = File.Open(settingsFilenamePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, savebleData);
            }
		}

		public void ExitGame()
		{
			SaveData();
			Application.Quit();
		}
    }
}