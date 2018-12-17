using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
	public class TerrainPointerController : NetworkBehaviour
	{
		public GameObject AttachedObject;
		public GridAllignementProvider GridAllignementProvider;

		public enum GridAllignementOption
		{
			ALLIGN_TO_GRID,
			NOT_ALLIGN_TO_GRID
		}

		public void AttachObject(GameObject instantiatedPrefab, GridAllignementOption gridAllignementOption)
		{
			DetachObject();

			_gridAllignementOption = gridAllignementOption;

			instantiatedPrefab.transform.parent = transform;
			instantiatedPrefab.transform.localPosition = Vector3.up * 3.8f;
			AttachedObject = instantiatedPrefab;
		}

		public void DetachObject()
		{
			if (AttachedObject == null)
				return;

			_gridAllignementOption = GridAllignementOption.NOT_ALLIGN_TO_GRID;

			transform.DetachChildren();
			Destroy(AttachedObject);
			AttachedObject = null;
		}

		public override void OnStartAuthority()
		{
			base.OnStartAuthority();

			if (hasAuthority)
				ControlPointerProvider.SetTerrainPointerController(this);
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
