using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.GameUnits
{
    class UnitDestroyer : MonoBehaviourSingletonCapability<UnitDestroyer>
    {
        protected override void CustomInitialization()
        {
            
        }

        public void DestroyUnit(GameObject unit)
        {
            Destroy(unit);
        }

    }
}
