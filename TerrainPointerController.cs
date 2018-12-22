using Assets.Scripts.Core;
using Assets.Scripts.Multi;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
	public class TerrainPointerController : NetworkBehaviour
	{
		[SyncVar]
		public bool IsObjectAttached;

		public GridAllignementProvider GridAllignementProvider;

		public enum GridAllignementOption
		{
			ALLIGN_TO_GRID,
			NOT_ALLIGN_TO_GRID
		}

		/*
		 * Invoked by client
		 */
		public void AttachObject(GameObject objectToAttachPrefab, GridAllignementOption gridAllignementOption)
		{
			DetachObject();

			CmdAttachObject(objectToAttachPrefab.GetStaticAssetUniqueId(), PlayerScript.GetInstance().Team);

			_gridAllignementOption = gridAllignementOption;

			if (_gridAllignementOption == GridAllignementOption.ALLIGN_TO_GRID)
			{
				_transform.position = GridAllignementProvider.GetGridPosition(transform.localPosition);
			}
		}

		/*
		 * Invoked by client
		 */
		public void DetachObject()
		{
			if (!IsObjectAttached)
				return;

			_gridAllignementOption = GridAllignementOption.NOT_ALLIGN_TO_GRID;

			CmdDetachObject();
		}

		public override void OnStartAuthority()
		{
			base.OnStartAuthority();

			if (hasAuthority)
				TerrainPointerControllerProvider.SetTerrainPointerController(this);
		}

		[Command]
		public void CmdAttachObject(string objectGuid, Team team)
		{
			var instantiatedPrefab = Instantiate(
				StaticAssetsManagerScript.GetInstance().GetRegistered(objectGuid),
				transform.position,
				Quaternion.identity
			);

			instantiatedPrefab.transform.parent = transform;
			instantiatedPrefab.transform.localPosition = Vector3.up * 3.8f;

			IsObjectAttached = true;
			var res = PlayersManager.GetInstance().Get(team).connectionToClient;


			NetworkServer.SpawnWithClientAuthority(instantiatedPrefab, res);

			RpcSetParent(instantiatedPrefab.name);
		}

		[ClientRpc]
		public void RpcSetParent(string name)
		{
			GameObject.Find(name).transform.parent = transform;
		}

		[Command]
		public void CmdDetachObject()
		{
			if (!IsObjectAttached)
				return;

			IsObjectAttached = false;
			NetworkServer.Destroy(transform.GetChild(0).gameObject);
		}

		private void Start()
		{
			if (Terrain == null)
			{
				Terrain = GameObject.Find("WarTheater").GetComponent<Terrain>();
				GridAllignementProvider = Terrain.GetComponent<GridAllignementProvider>();
			}

			// name += GetInstanceID();

			_collider = Terrain.GetComponent<Collider>();
			_transform = GetComponent<Transform>();
			_gridAllignementOption = GridAllignementOption.NOT_ALLIGN_TO_GRID;
		}

		private void Update()
		{
			if (hasAuthority != true)
				return;

			if (!Input.GetKey(KeyCode.E))
				return;

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (_collider.Raycast(ray, out hit, Mathf.Infinity))
			{
				if (_gridAllignementOption == GridAllignementOption.ALLIGN_TO_GRID)
				{
					_transform.position = GridAllignementProvider.GetGridPosition(hit.point);
				}
				else
				{
					_transform.position = hit.point + Vector3.up;
				}
			}
		}

		private Collider _collider;
		private Transform _transform;
		private Terrain Terrain;
		private GridAllignementOption _gridAllignementOption;
	}
}
