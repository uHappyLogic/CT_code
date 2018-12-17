using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Core
{
	public class GridAllignementProvider : MonoBehaviour
	{
		[SerializeField]
		public Vector3 GridDimmensions;

		public Vector3 GetGridPosition(Vector3 currentPositon)
		{
			return GridPositionCalculator.GetNearestGridPosition(transform.position, currentPositon, GridDimmensions);
		}
	}
}
