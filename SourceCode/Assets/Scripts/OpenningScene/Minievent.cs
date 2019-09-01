using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Minievent : MonoBehaviour
{
    public Transform des;
    public Transform mainChar;
    public float moveSpeed = 0.5f;
    public float rotateSpeed = 2f;
    public HeadBob hb;
    // Update is called once per frame
    Quaternion _newRotate = Quaternion.Euler(0, 0, 0);

    bool startEvent = false;
    Vector3 _newDes;

    private void Awake()
    {
        _newDes = des.position;
        _newDes.y = mainChar.position.y;

    }

    void Update()
    {
        if (startEvent)
        {
            mainChar.forward = Vector3.Lerp(mainChar.forward, -Vector3.forward, rotateSpeed * Time.deltaTime);

            Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, _newRotate, rotateSpeed * Time.deltaTime);
            mainChar.position = Vector3.Lerp(mainChar.position, _newDes, moveSpeed * Time.deltaTime);


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        startEvent = true;
        mainChar.GetComponent<RigidbodyFirstPersonController>().enabled = false;
        

    }
}
