using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.GameUnits
{
    public class UpdateablesManager : MonoBehaviourSingletonCapabilityManager<UpdateablesManager, IUpdateable>
    {
        protected override void CustomInitialization()
        {
            
        }

        public void Update()
        {
            foreach (var updateable in _registered)
            {
                updateable.PerformUpdate();
            }
        }

        public override void LogAddOperation(IUpdateable ctor)
        {
            Debug.Log("Updateable added to updateable manager");
        }

        public override string GetName()
        {
            return "Updateable manager";
        }
    }
}