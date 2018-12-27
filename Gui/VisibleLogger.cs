using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
	public class VisibleLogger : MonoBehaviour
	{	
		[SerializeField]
		public Text ScrollViewContent;

		public void Start()
		{
			_instance = this;
		}

		public static VisibleLogger GetInstance()
		{
			return _instance;
		}

		public void LogDebug(string msg)
		{
			Debug.Log(msg);
			ScrollViewContent.text += "\n" + msg;
		}

		public void LogException(Exception e)
		{
			Debug.LogException(e);
			ScrollViewContent.text += string.Format("<color=red>{0}</color>",
				e.ToString()
			);
		}

		private static VisibleLogger _instance;
	}
}
