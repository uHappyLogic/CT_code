using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Multi
{
	public class PlayerScript : NetworkBehaviour
	{
		public GameObject TerrainPointer;

		public void Start()
		{
			name += GetInstanceID();

			CmdSpawn();
		}

		[Command]
		public void CmdSpawn()
		{
			GameObject pointer = (GameObject)Instantiate(TerrainPointer, transform.position, Quaternion.identity);
			NetworkServer.SpawnWithClientAuthority(pointer, connectionToClient);
		}
	}
}
