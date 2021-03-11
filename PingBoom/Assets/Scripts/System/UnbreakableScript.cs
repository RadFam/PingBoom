using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemObjects
{
    public class UnbreakableScript : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
			DontDestroyOnLoad(this.gameObject);
        }

    }
}