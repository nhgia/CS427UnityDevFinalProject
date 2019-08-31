using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;

    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip collideClip;
    public AudioClip endClip;
    public AudioClip againClip;

    AudioSource audioSource;
    [HideInInspector]
    public bool checkFirstTime = true;

    private void Awake()
    {
        if (am == null)
        {
            am = this;
            audioSource = GetComponent<AudioSource>();
        }
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayWin()
    {
        audioSource.PlayOneShot(winClip);

    }

    public void PlayLose()
    {
        audioSource.PlayOneShot(loseClip);

    }

    public void BallCollide()
    {
        audioSource.PlayOneShot(collideClip);
    }



    public void EndMusic()
    {
        //audioSource.PlayOneShot(endClip);
    }

    public void PlayAgain()
    {
        audioSource.PlayOneShot(againClip);
    }

}
