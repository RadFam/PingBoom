using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMenusUI
{
    public class LevelMenuScript : MonoBehaviour
    {

        // Use this for initialization
        public void OnClose()
        {
            gameObject.SetActive(false);
        }
        public void OnOpen()
        {
            gameObject.SetActive(true);
        }
    }
}