using UnityEngine.Networking;

namespace Assets.Scripts.Core
{
	public abstract class  StrictInitialized : NetworkBehaviour
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
