using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_wheel : MonoBehaviour
{
    private int speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, speed);
        speed += 10;
        speed = speed % 360;

    }
}
