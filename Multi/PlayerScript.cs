using UnityEngine;
using Mirror;

namespace Assets.Scripts.Multi
{
	public class PlayerScript : NetworkBehaviour
	{
		public GameObject TerrainPointer;

		public void Start()
		{
			name += GetInstanceID();
		
			if (isLocalPlayer)
				_instance = this;

			if (isServer)
			{
				CmdSpawnPointer();
				Team = PlayersManager.GetInstance().Add(this);
				Gold = 200;
			}
		}
		
		[Command]
		public void CmdSpawnPointer()
		{
			GameObject pointer = Instantiate(TerrainPointer, transform.position, Quaternion.identity);
			NetworkServer.SpawnWithClientAuthority(pointer, connectionToClient);
		}

		public static PlayerScript GetInstance()
		{
			return _instance;
		}

		private static PlayerScript _instance;

		[SyncVar]
		public Team Team;

		[SyncVar]
		public int Gold;
	}
}
