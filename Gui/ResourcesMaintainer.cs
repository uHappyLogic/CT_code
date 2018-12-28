using Assets.Scripts.Multi;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
	public class ResourcesMaintainer : MonoBehaviour
	{
		public void Start()
		{
			Text = GetComponent<Text>();
		}

		public void Update()
		{
			if (PlayerScript.GetInstance() == null)
				return;

			if (Text.text != PlayerScript.GetInstance().Gold.ToString())
				Text.text = PlayerScript.GetInstance().Gold.ToString();
		}

		private Text Text;
	}
}
