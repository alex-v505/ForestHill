using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Alice;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Alice == null) return;
        Vector3 posicion = transform.position;
        posicion.x = Alice.transform.position.x;
        posicion.y = Alice.transform.position.y;
        transform.position = posicion;
    }
}
