using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Playables;

public class MiniEventTrigger : MonoBehaviour
{
    
    PlayableDirector pd;

    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
      
        pd.Play();

    }


}
