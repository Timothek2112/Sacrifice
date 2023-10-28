using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOnMouse : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        var mousePos = Input.mousePosition;
        var mouseCoordinates = Camera.main.ScreenToWorldPoint(mousePos);
        var angle = Vector2.Angle(-Vector2.right, mouseCoordinates - transform.position);
        transform.eulerAngles = new Vector3(0, 0, transform.position.y < mouseCoordinates.y ? -angle : angle);
    }
}
