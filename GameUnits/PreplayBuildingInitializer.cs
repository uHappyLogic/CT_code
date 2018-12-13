using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;

namespace Assets.Scripts.GameUnits
{
    class PreplayBuildingInitializer : MonoBehaviour, IInOrderInitializable
    {
        public Team Team;
        public List<GameObject> PrePlayBuildings;
        public List<GameObject> PrePlayUnits;

        public void InitInOrder()
        {
            foreach (var prePlayBuilding in PrePlayBuildings)
            {
                GameBuilding gameBuilding = prePlayBuilding.GetComponent<GameBuilding>();
                gameBuilding.Init();
                gameBuilding.ActorAttributes.InitActorAttributes(Team);
                gameBuilding.CompleteConstruction();
                gameBuilding.OnConstructionComplete();

                BuildingsManager.GetInstance().Add(gameBuilding);
            }

            foreach (var prePlayUnit in PrePlayUnits)
            {
                GameUnit gameUnit = prePlayUnit.GetComponent<GameUnit>();
                gameUnit.ActorAttributes.InitActorAttributes(Team);
                gameUnit.Init();

                UnitsManager.GetInstance().Add(gameUnit);
            }
        }
    }
}
