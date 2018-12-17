using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts
{
	public class TerrainPointerController : MonoBehaviour
	{
		public Terrain Terrain;
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

		private void Start()
		{
			_collider = Terrain.GetComponent<Collider>();
			_transform = GetComponent<Transform>();
			_gridAllignementOption = GridAllignementOption.NOT_ALLIGN_TO_GRID;
		}

		private void Update()
		{
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
		private GridAllignementOption _gridAllignementOption;
	}
}
