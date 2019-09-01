using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGameCheckPoint : MonoBehaviour
{

    public bool change;
    public int[] diologue;

    public bool final;

    private void OnTriggerEnter(Collider other)
    {
        DialogueSystem.ds.Display(diologue);
        // trigger dialogue
        if (other.GetComponent<CharacterControl>() != null)
        RunningGameManager.rgm.UpdateCheckPoint();

        if (final) RunningGameManager.rgm.End();



        Destroy(GetComponent<BoxCollider>());

        



    }
}
