using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class MovePointsPool : MonoBehaviour
    {
		[SerializeField]
		List<MovePointsMove> movePointsPath;

		public MovePointsMove MovePath(int path)
        {
            if (path < movePointsPath.Count)
			{
				return movePointsPath[path];
			}
			return null;
        }
    }
}