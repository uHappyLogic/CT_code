using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
    class KillsCounter : MonoBehaviourSingletonCapability<KillsCounter>
    {
        protected override void CustomInitialization()
        {
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

        private String GetText()
        {
            return String.Format(
                "Kills:\nTeam A\t-\t{0}\nTeam B\t-\t{1}",
                _killsPerTeam[Team.TEAM_A],
                _killsPerTeam[Team.TEAM_B]
             );
        }

        private Text _text;
        private Dictionary<Team, int> _killsPerTeam;
    }
}
