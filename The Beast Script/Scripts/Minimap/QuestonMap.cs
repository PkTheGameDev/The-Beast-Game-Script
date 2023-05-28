using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestonMap : MonoBehaviour
{
    public Transform MinimapCam;
    public float MapRadius;
    Vector3 TempV3;

    // Update is called once per frame
    void Update()
    {
        //TempV3 = transform.position;
        //Debug.Log(TempV3);
    }

    private void LateUpdate()
    {
        TempV3 = transform.parent.position;
        TempV3.y = transform.position.y;
        transform.position = TempV3;

        //Center of Minimap
        Vector3 CenterPoint = MinimapCam.position;

        float DistanceBtw = Vector3.Distance(TempV3, CenterPoint);

        // If the Distance is less than MinimapSize, it is within the Minimap view and we don't need to do anything
        // But if the Distance is greater than the MinimapSize, then do this

        if (DistanceBtw > MapRadius)
        {
            Vector3 FromOrigintoObj = TempV3 - CenterPoint;
            //FromorigintoObj = FromOrigintoObj * MapRadius / DistanceBtw
            FromOrigintoObj *= MapRadius / DistanceBtw;

            transform.position = CenterPoint + FromOrigintoObj;
        }

    }
}
