using UnityEngine;

namespace SystemObjects
{
    public class LevelBonusController : MonoBehaviour
    {
		PuckPrizeScript puckPrize;
		LevelWalkThroughScript levelWalkThrough;
		int level;
		int lScore;
		int fScore;
		float strikes;
		bool[] walkCheck;
		int starNeed;

		public bool[] WalkCheck
		{
			get {return walkCheck;}
		}

		public int StarNeed
		{
			get {return starNeed;}
		}

		void Start()
		{
			puckPrize = Resources.Load<PuckPrizeScript>("ScriptableObjects/PuckPrize");		
		}
		public bool MakeWalkThroughDecision(int currLevel, int strikes, int scores, float time)
		{
			level = currLevel;
			walkCheck = new bool[3];
			walkCheck[0] = false;
			walkCheck[1] = false;
			walkCheck[2] = false;
			int cnt = 0;
			if (GameManager.inst.walkThrough.levelWalkthrough[level].PuckStrikes >= strikes)
			{
				walkCheck[0] = true;
				cnt++;
			}
			if (GameManager.inst.walkThrough.levelWalkthrough[level].LevelMinScore <= scores)
			{
				walkCheck[1] = true;
				cnt++;
			}
			if (GameManager.inst.walkThrough.levelWalkthrough[level].LevelTime >= time)
			{
				walkCheck[2] = true;
				cnt++;
			}
			starNeed = GameManager.inst.walkThrough.levelWalkthrough[level].StarScore;

			return (cnt >= GameManager.inst.walkThrough.levelWalkthrough[level].StarScore);
		}
		public void MakePrizeDecision(int currLevel, int levelScore, int fullScore, float strikesPart)
		{
			level = currLevel;
			lScore = levelScore;
			fScore = fullScore;
			strikes = strikesPart;
			//Debug.Log("Successfully get data for prize analysis");
		}

		public bool GetPuckPrize(out string puckDesr, out Sprite puckView)
		{
			puckDesr = "";
			puckView = null;
			//Debug.Log("My level: " + level);
			foreach (PuckPrizeInfo ppi in puckPrize.puckPrizes)
			{
				if (ppi.LevelFromAchieve <= level)
				{
					int puckNum = GameManager.inst.allPucks.GetPuckNumByName(ppi.PuckName);
					if (!GameManager.inst.pucksUnblocked[puckNum])
					{
						if (ppi.LevelScrore <= lScore || ppi.WholeScore <= fScore || ppi.StrikePart >= strikes)
						{
							Debug.Log("Puck " + puckNum.ToString() + " was chosen");
							GameManager.inst.pucksUnblocked[puckNum] = true;
							puckDesr = GameManager.inst.allPucks.GetPuckByName(ppi.PuckName).RusName + "\n" + GameManager.inst.allPucks.GetPuckByName(ppi.PuckName).RusDescr;
							puckView = GameManager.inst.allPucks.GetPuckByName(ppi.PuckName).PuckSprite;
							return true;
						}
					}
				}
			}

			return false;
		}
    }
}