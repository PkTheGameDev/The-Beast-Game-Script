using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Rotate : MonoBehaviour
{
    public Transform CubeTransform;
    //public float Amplitude = 0.1f;
    //public float TimeScaleVal;
    
    void Update()
    {
        //Rotates the Assigned Transform of the object 
        transform.RotateAround(CubeTransform.transform.position, Vector3.up, 20 * Time.deltaTime);

        //Gives a floating effect to the script assigned object
        //transform.position = new Vector3(CubeTransform.position.x, CubeTransform.position.y + Amplitude * Mathf.Sin(TimeScaleVal * Time.time), CubeTransform.position.z);
    }
}
