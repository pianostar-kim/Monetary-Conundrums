using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    [SerializeField] Material matWhenOverlapping;
    [SerializeField] Material defaultMat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().material = matWhenOverlapping;
    }

    void OnTriggerExit(Collider other)
    {
        GetComponent<Renderer>().material = defaultMat;
        if (!other.gameObject.GetComponent<Cylinder>().getMouseDown())
        {
            Debug.Log("Received cylinder.");
        }
    }
}
