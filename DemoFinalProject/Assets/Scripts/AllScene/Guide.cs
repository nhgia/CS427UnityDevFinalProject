using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public float appearTime = 5f;

    private void OnEnable()
    {
        Invoke("DisEnable", appearTime);
    }

    void DisEnable()
    {
        gameObject.SetActive(false);

    }





}
