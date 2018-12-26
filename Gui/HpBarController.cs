using Assets.Scripts.GameUnits.Generic;
using UnityEngine;

namespace Assets.Scripts.Gui
{
	public class HpBarController : MonoBehaviour
	{
		[SerializeField]
		public TextMesh HpTextMeshObject;

		[SerializeField]
		public UnitAttributes UnitAttributes;

		public void Start()
		{
			_maxHp = UnitAttributes.HealthPoints;

			Update();
		}

		public void Update()
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);

			HpTextMeshObject.text = string.Format("{0}/{1}"
				, UnitAttributes.HealthPoints
				, _maxHp);
		}

		private float _maxHp;

	}
}
