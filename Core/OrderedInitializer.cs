using Assets.Scripts.GameUnits;
using Assets.Scripts.Gui;
using UnityEngine;

namespace Assets.Scripts.Core
{
    class OrderedInitializer : MonoBehaviour
    {
        public GameObject GUI;
        public GameObject Statistics; 

        public void Start()
        {
            Debug.Log("Initializing in order");

            UnitDestroyer.SetInstance(GetComponent<UnitDestroyer>());

            UpdateablesManager updateablesManager = GetComponent<UpdateablesManager>();
            updateablesManager.InitInOrder();

            new BuildingsManager().InitInOrder();
            new UnitsManager().InitInOrder();

            var ud = UnitDestroyer.GetInstance();

            Statistics.GetComponent<StrictInitializedMonoBehaviour>().InitInOrder();

            updateablesManager.Add(UnitsManager.GetInstance());
            updateablesManager.Add(BuildingsManager.GetInstance());

            foreach (var preplayBuildingInitializer in GetComponents<PreplayBuildingInitializer>())
            {
                preplayBuildingInitializer.InitInOrder();
            }

            foreach (var initializableChild in GUI.GetComponentsInChildren<IInOrderInitializable>())
            {
                initializableChild.InitInOrder();
            }

            Debug.Log("Initializing in order successfully finished");
        }
    }
}
