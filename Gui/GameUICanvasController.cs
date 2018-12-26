using Assets.Scripts.Gui;
using UnityEngine;

namespace Assets.Scripts
{
	public class GameUICanvasController : MonoBehaviour
	{
		public void ExitGame()
		{
			VisibleLogger.GetInstance().LogDebug("Exit button pressed");

#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
		}
	}
}