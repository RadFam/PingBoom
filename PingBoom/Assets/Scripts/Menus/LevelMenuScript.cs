using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SystemObjects;

namespace AllMenusUI
{
    public class LevelMenuScript : MonoBehaviour
    {
        [SerializeField]
        List<Button> myButtons;
        // Use this for initialization
        public void OnEnable()
        {
            OnOpen();
        }

        public void OnDisable()
        {
            OnClose();
        }
        public void OnClose()
        {
            gameObject.SetActive(false);
        }
        public void OnOpen()
        {
            gameObject.SetActive(true);
            // Set Active so many buttons, as we can

            int openedLevels = GameManager.inst.AvailableLevel;
            for (int i = 0; i < myButtons.Count; ++i)
            {
                if (i < openedLevels)
                {
                    myButtons[i].interactable = true;
                }
                else
                {
                    myButtons[i].interactable = false;
                }
            }
        }
    }
}