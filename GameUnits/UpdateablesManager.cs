using System.Collections.Generic;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.GameUnits
{
	public class UpdateablesManager : MonoBehaviour
	{
		public void Update()
		{
			foreach (var updateable in _registered)
			{
				updateable.PerformUpdate();
			}
		}
		
		public void Add(IUpdateable actor)
		{
			Debug.Log("Updateable added to updateable manager");

			_registered.Add(actor);
		}

		protected List<IUpdateable> _registered = new List<IUpdateable>();
	}
}