using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemObjects
{
    public class LevelBonusController : MonoBehaviour
    {
		public void MakePrizeDecision(int levelScore, int fullScore)
		{

		}

		public bool GetPuckPrize(out string puckDesr, out Sprite puckView)
		{
			puckDesr = "";
			puckView = null;
			return false;
		}
    }
}