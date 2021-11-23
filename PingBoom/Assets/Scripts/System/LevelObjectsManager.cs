using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using SystemObjects;

namespace GameObjects
{
    public class LevelObjectsManager : MonoBehaviour
    {
		[SerializeField]
		LevelManager levelManager;
		[SerializeField]
		List<BaseObstacleScript> obstacles;
		[SerializeField]
		PlayableDirector playableDirector;

        // Use this for initialization
        void Start()
        {
			if (playableDirector != null && levelManager != null)
			{
				//playableDirector.played
			}
        }

		public bool PlayCameraCut()
		{
			if (playableDirector != null)
			{
				playableDirector.Play();
				return true;
			}

			return true;
		}

		public void PassObstacles()
		{
			foreach(BaseObstacleScript bo in obstacles)
			{
				if (bo != null)
				{
					bo.MakePassible();
				}
			}
		}

		public void UnpassObstacles()
		{
			foreach(BaseObstacleScript bo in obstacles)
			{
				if (bo != null)
				{
					bo.MakeUnpassible();
				}
			}
		}
        
    }
}