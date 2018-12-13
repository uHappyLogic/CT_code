using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class ScrollingObjectBehaviour : MonoBehaviour
    {
        void Update()
        {
            float mousePosX = Input.mousePosition.x;
            float mousePosY = Input.mousePosition.y;
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            int scrollDistance = 5;
            float scrollSpeed = 150;

            if (mousePosX < scrollDistance)
            {
                transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
            }

            if (mousePosX >= Screen.width - scrollDistance)
            {
                transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
            }

            if (mousePosY < scrollDistance)
            {
                transform.Translate(transform.up * -scrollSpeed * Time.deltaTime);
            }

            if (mousePosY >= Screen.height - scrollDistance)
            {
                transform.Translate(transform.up * scrollSpeed * Time.deltaTime);
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
