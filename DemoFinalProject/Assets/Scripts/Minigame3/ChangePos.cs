using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePos : MonoBehaviour
{
    public GameObject left;
    public GameObject right;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (left != null && other.GetComponent<CharacterControl>().checkOpen)
            {
                left.SetActive(false);
            }
            else if (right != null)
            {
                right.SetActive(false);
            }


            other.GetComponent<CharacterControl>().StartChangePosition();





        }
    }
}
