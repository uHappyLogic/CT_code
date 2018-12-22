using UnityEngine.Networking;

namespace Assets.Scripts.Core
{
	public abstract class StrictInitializedMonoBehaviour : NetworkBehaviour
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
