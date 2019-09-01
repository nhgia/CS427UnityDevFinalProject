using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public static MazeManager mz;

    public Animator levelChanger;

    public Transform[] spawnPos;
    public Transform[] planes;
    public Transform head;

    public float appearSpeed = 2f;
    public float cameraChangeSpeed = 2f;

    int i = 0;
    Vector3 lowerVector, toVector, newCameraPos;

    public int testCamera;
    
    private void Awake()
    {
        mz = this;

        Cursor.visible = false;
    }

    private void Start()
    {
        toVector = planes[0].transform.position;
        lowerVector = (head.localPosition - Camera.main.transform.localPosition) / (planes.Length - 1);
        newCameraPos = Camera.main.transform.localPosition;

        newCameraPos += lowerVector * testCamera;
        i = testCamera;
    }

    private void Update()
    {
        planes[i].transform.position = Vector3.Lerp(planes[i].transform.position, toVector, appearSpeed * Time.deltaTime);
        Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, newCameraPos, cameraChangeSpeed * Time.deltaTime);
    }

    public void UpdateCamera()
    {
        if (i + 1 == planes.Length)
        {
            levelChanger.SetTrigger("FadeOut");
        }
        else
        {
            i++;

            toVector = planes[i].transform.position;
            toVector.y = planes[0].transform.position.y;
            //toVector.z = planes[0].transform.position.z;
            newCameraPos += lowerVector;
        }
    }

    public void Restart(GameObject player)
    {
        // dark scene animation
        player.transform.position = spawnPos[i].position;

    }


}




