using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float camX, camY;
    public Transform cm;
    
    public float verticalRange;
    public float horizontalRange;

    private void Update()
    {
        camX += Input.GetAxisRaw("Mouse X");
        camY -= Input.GetAxisRaw("Mouse Y");

        camX = Mathf.Clamp(camX, -horizontalRange ,horizontalRange);
        camY = Mathf.Clamp(camY, -verticalRange, verticalRange);

     
        transform.rotation = Quaternion.Euler(0, camX, 0);
        cm.rotation = Quaternion.Euler(camY, camX, 0);

    
    }





}
