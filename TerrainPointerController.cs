using UnityEngine;

namespace Assets.Scripts
{
	public class TerrainPointerController : MonoBehaviour
	{
		public Terrain Terrain;
		public GameObject AttachedObject;

		private void Start()
		{
			_collider = Terrain.GetComponent<Collider>();
			_transform = GetComponent<Transform>();
		}

		private void Update()
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (_collider.Raycast(ray, out hit, Mathf.Infinity))
			{
				_transform.position = hit.point + Vector3.up;
			}
		}

		public void AttachObject(GameObject instantiatedPrefab)
		{
			DetachObject();

			instantiatedPrefab.transform.parent = transform;
			instantiatedPrefab.transform.localPosition = Vector3.up * 3.8f;
			AttachedObject = instantiatedPrefab;
		}

		public void DetachObject()
		{
			if (AttachedObject == null)
				return;

			transform.DetachChildren();
			Destroy(AttachedObject);
			AttachedObject = null;
		}

		private Collider _collider;
		private Transform _transform;
	}
}
