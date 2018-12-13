using UnityEngine;

namespace Assets.Scripts.Core
{
    public abstract class  StrictInitializedMonoBehaviour : MonoBehaviour
    {
        public void InitInOrder()
        {
            CoreInitialize();
            CustomInitialization();
        }

        protected abstract void CoreInitialize();
        protected abstract void CustomInitialization();
    }
}
