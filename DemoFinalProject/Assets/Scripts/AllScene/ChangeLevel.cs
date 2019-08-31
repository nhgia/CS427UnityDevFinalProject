using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    public Animator levelChanger;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.GetComponent<Rigidbody>() != null)
        {
            levelChanger.SetTrigger("FadeOut");
        }


    }
}
