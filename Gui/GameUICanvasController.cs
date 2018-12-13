using System;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameUICanvasController : MonoBehaviour
    {
        public void ExitGame()
        {
            Debug.Log("Exit button pressed");

            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
            #else
                    Application.Quit();
            #endif
        }
    }
}