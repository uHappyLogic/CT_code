using UnityEngine;

namespace Assets.Scripts.Core
{
	public static class TerrainPointerControllerProvider
	{
		public static TerrainPointerController GetInstance()
		{
			return _terrainPointerController;
		}

		public static void SetTerrainPointerController(TerrainPointerController terrainPointerController)
		{
			_terrainPointerController = terrainPointerController;
		}

		private static TerrainPointerController _terrainPointerController;
	}
}
