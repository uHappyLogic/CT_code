using System;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Multi
{
	public class PlayersManager
	{
		public static PlayersManager GetInstance()
		{
			if (_playersManager == null)
				_playersManager = new PlayersManager();

			return _playersManager;
		}

		public PlayerScript Get(Team team)
		{
			return _teamPlayer[team];
		}

		public Team Add(PlayerScript playerScript)
		{
			playerScript.Team = availableTeam[0];

			availableTeam.Remove(availableTeam[0]);

			_teamPlayer[playerScript.Team] = playerScript;

			return playerScript.Team;
		}

		private PlayersManager()
		{
			_teamPlayer = new Dictionary<Team, PlayerScript>();

			availableTeam = Enum.GetValues(typeof(Team)).Cast<Team>().ToList();
		}

		private Dictionary<Team, PlayerScript> _teamPlayer;

		private List<Team> availableTeam;

		private static PlayersManager _playersManager; 
	}
}
