using System;
using UnityEngine;

namespace Assets.Scripts
{
	internal class ScrollingObjectBehaviour : MonoBehaviour
	{
		public Terrain ObservableField;

		private void Update()
		{
			if (!Input.GetKey(KeyCode.U))
				return;

			float mousePosX = Input.mousePosition.x;
			float mousePosY = Input.mousePosition.y;
			float scroll = Input.GetAxis("Mouse ScrollWheel");

			int scrollDistance = 5;
			float scrollSpeed = 150;

			if (mousePosX < scrollDistance)
			{
				if (transform.position.x > ObservableField.transform.position.x)
					transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
			}

			if (mousePosX >= Screen.width - scrollDistance)
			{
				if (transform.position.x < ObservableField.transform.position.x + ObservableField.terrainData.size.x )
					transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
			}

			if (mousePosY < scrollDistance)
			{
				if (transform.position.z > ObservableField.transform.position.z - ObservableField.terrainData.size.z)
					transform.Translate(Vector3.forward * -scrollSpeed * Time.deltaTime, Space.World);
			}

			if (mousePosY >= Screen.height - scrollDistance)
			{
				if (transform.position.z < ObservableField.transform.position.z)
					transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime, Space.World);
			}

			if (Math.Abs(scroll) > 0.0001f)
			{

				Vector3 transition = transform.forward * scroll * scrollSpeed * 10f * Time.deltaTime;
				//Debug.Log(transition);
				transform.position += transition;
			}

		}
	}
}
