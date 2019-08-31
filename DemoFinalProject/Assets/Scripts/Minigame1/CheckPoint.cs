using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    TextMesh tm;

    private void Awake()
    {
        tm = GetComponent<TextMesh>();
    }

    private void OnTriggerEnter(Collider other)
    {
//        Debug.Log("Appear");
        if (other.tag == "Player")
        {
            // appear plane
       
            MazeManager.mz.UpdateCamera();
            Destroy(GetComponent<TextMesh>());
            Destroy(GetComponent<BoxCollider>());
        }
    }



}
