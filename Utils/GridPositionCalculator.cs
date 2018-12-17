using UnityEngine;

namespace Assets.Scripts.Utils
{
	public static class GridPositionCalculator
	{
		public static Vector3 GetNearestGridPosition(Vector3 start, Vector3 current, Vector3 dimm)
		{
			return new Vector3(
				start.x + Mathf.RoundToInt((current.x - start.x) / dimm.x) * dimm.x,
				start.y,
				start.z + Mathf.RoundToInt((current.z - start.z) / dimm.z) * dimm.z
			);
		}
	}
}
