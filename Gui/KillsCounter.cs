using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
	internal class KillsCounter : MonoBehaviour
	{
		public static KillsCounter GetInstance()
		{
			return _killsCounter;
		}

		public void Start()
		{
			_killsCounter = this;

			_killsPerTeam = new Dictionary<Team, int>()
			{
				{Team.TEAM_A, 0},
				{Team.TEAM_B, 0}
			};

			_text = GetComponent<Text>();
			_text.text = GetText();
		}

		public void Increment(Team team)
		{
			_killsPerTeam[team] = _killsPerTeam[team] + 1;
			_text.text = GetText();
		}

		private string GetText()
		{
			return string.Format(
				"Kills:\nTeam A\t-\t{0}\nTeam B\t-\t{1}",
				_killsPerTeam[Team.TEAM_A],
				_killsPerTeam[Team.TEAM_B]
			 );
		}

		private static KillsCounter _killsCounter;

		private Text _text;
		private Dictionary<Team, int> _killsPerTeam;
	}
}
