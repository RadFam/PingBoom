using UnityEngine;
using UnityEngine.UI;

namespace GameObjects
{
    public class PuckCanvas : MonoBehaviour
    {
		[SerializeField]
		Text liveCounts;
		int myLives;
        // Use this for initialization
        public void SetLives(int lives)
		{
			myLives = lives;
			liveCounts.text = myLives.ToString();
		}

		public void DecreaseLives()
		{
			myLives -= 1;
			liveCounts.text = myLives.ToString();
		}
    }
}