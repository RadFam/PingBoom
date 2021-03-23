using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class MovePointsMove : MonoBehaviour
    {

        // Use this for initialization
        public int childVol;
        List<Transform> myMovePoints;

		int nextPointNum;

        public List<Transform> MovePoints
        {
            get { return myMovePoints; }
        }

        public Transform GetPoint(int num)
        {
            if (num < childVol && num >= 0)
            {
                return myMovePoints[num];
            }

            return null;
        }

        void Start()
        {
            if (transform.childCount > 0)
            {
                childVol = transform.childCount;
                myMovePoints = new List<Transform>();
                for (int i = 0; i < childVol; ++i)
                {
                    myMovePoints.Add(transform.GetChild(i).transform);
                }
            }

			nextPointNum = 0;
        }

		public Transform GetNextPoint()
		{
			Transform ans = myMovePoints[nextPointNum];
			nextPointNum++;
			if (nextPointNum == childVol)
			{
				nextPointNum = 0;
			}

			return ans;
		}
    }
}