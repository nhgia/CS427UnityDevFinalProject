using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    public GameObject sprite;
    public ParticleSystem particles;

    private void Start()
    {
        StartCoroutine(waitForIt());
    }

    IEnumerator waitForIt()
    {
        yield return new WaitForSeconds(1.5f);
        Play();
    }

    void Play()
    {
        sprite.gameObject.SetActive(false);
        particles.Emit(9999);
    }


}
