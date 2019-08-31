using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityStandardAssets;

public class EventTrigger : MonoBehaviour
{
    PlayableDirector playableDirector;
    public CharacterMove cm;
   
    
    public GameObject ChooseCanvas;

    public int continousDialogue;

    public Sprite[] meleeWeapons;
    public Sprite[] rangeWeapons;
    public Sprite[] pets;
    public TimelineAsset[] timeLine;

    bool prepareForFinal = false;
    bool finalTimeLine = false;

    int destroyForAll;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
        destroyForAll = timeLine.Length;
    }

    private void Update()
    {
        if (playableDirector.state == PlayState.Paused && prepareForFinal) {

            playableDirector.playableAsset = timeLine[timeLine.Length - 1];
            playableDirector.Play();

            prepareForFinal = false;
            finalTimeLine = true;
        }

        if (playableDirector.state == PlayState.Paused && finalTimeLine)
        {
            GoOn();
        }




    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cm != null) cm.move = false;
        playableDirector.Play();
    }

    public void ChooseMeleeWeapon(int i)
    {
        cm.ChangeMeleeWeapon(meleeWeapons[i]);
        // change melee one
        GoOn();
    }

    public void ChooseRangeWeapon(int i)
    {
        cm.ChangeRangeWeapon(rangeWeapons[i]);
        // change range one
        GoOn();
    }

    public void ChoosePet(int i)
    {
        cm.ChangePet(pets[i]);
        // change pet one
        GoOn();
    }

    public void ChooseAction(int i)
    {
        // change action fight
        playableDirector.playableAsset = timeLine[i];
        playableDirector.Play();

        string buttonName = "Button" + (i + 1).ToString();
        GameObject button = GameObject.Find(buttonName);
        Destroy(button);
        destroyForAll--;

        if (destroyForAll == 1)
        {
            // prepare for final
            prepareForFinal = true;
            Debug.Log("Prepare");
        }

    }

    public void Replay()
    {
        playableDirector.time = 0;
        playableDirector.Play();
    }

    public void GoOn()
    {
        DialogueSystem.ds.Display(new int[] { continousDialogue });

        cm.move = true;
        ChooseCanvas.SetActive(false);
        //playableDirector.Play();
        Destroy(gameObject);
    }



    



}
