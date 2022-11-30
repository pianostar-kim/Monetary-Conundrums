using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 offsetFromMouse;
    private bool mouseDown = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        mouseDown = true;
        offsetFromMouse = gameObject.transform.position
            - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    void OnMouseDrag()
    {
        Vector3 currentMousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        transform.position = Camera.main.ScreenToWorldPoint(currentMousePoint) + offsetFromMouse;
    }

    void OnMouseUp()
    {
        mouseDown = false;
        transform.position = initialPosition;
    }

    public bool getMouseDown()
    {
        return mouseDown;
    }
}
