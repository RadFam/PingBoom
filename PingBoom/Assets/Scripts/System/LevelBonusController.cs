using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemObjects
{
    public class LevelBonusController : MonoBehaviour
    {
		PuckPrizeScript puckPrize;
		PuckObjectsScript puckCollect;
		int level;
		int lScore;
		int fScore;
		float strikes;

		void Start()
		{
			puckPrize = Resources.Load<PuckPrizeScript>("ScriptableObjects/PuckPrize");		
		}
		public void MakePrizeDecision(int currLevel, int levelScore, int fullScore, float strikesPart)
		{
			level = currLevel;
			lScore = levelScore;
			fScore = fullScore;
			strikes = strikesPart;
			puckCollect = Resources.Load<PuckObjectsScript>("ScriptableObjects/PuckContainer");
			Debug.Log("Successfully get data for prize analysis");
		}

		public bool GetPuckPrize(out string puckDesr, out Sprite puckView)
		{
			puckDesr = "";
			puckView = null;
			Debug.Log("My level: " + level);
			foreach (PuckPrizeInfo ppi in puckPrize.puckPrizes)
			{
				if (ppi.LevelFromAchieve <= level)
				{
					int puckNum = puckCollect.GetPuckNumByName(ppi.PuckName);
					if (!GameManager.inst.pucksUnblocked[puckNum])
					{
						if (ppi.LevelScrore <= lScore || ppi.WholeScore <= fScore || ppi.StrikePart >= strikes)
						{
							Debug.Log("Puck " + puckNum.ToString() + " was chosen");
							GameManager.inst.pucksUnblocked[puckNum] = true;
							puckDesr = puckCollect.GetPuckByName(ppi.PuckName).RusName + "\n" + puckCollect.GetPuckByName(ppi.PuckName).RusDescr;
							puckView = puckCollect.GetPuckByName(ppi.PuckName).PuckSprite;
							return true;
						}
					}
				}
			}

			return false;
		}
    }
}