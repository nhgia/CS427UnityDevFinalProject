using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem ds;
    public string[] dialogue;
    public Typewriter[] tmp;
    public TypeWriteGUI[] tmpUGUI;

    Queue<int> dialogues;

    private void Awake()
    {
        ds = this;
        dialogues = new Queue<int>();
    }

    public void Display(int[] listOfSentence)
    {
        foreach (int i in listOfSentence) dialogues.Enqueue(i);
        DisplayNextOne();
    }

    public void DisplayNextOne()
    {
        if (dialogues.Count == 0) return;

        int j = dialogues.Dequeue();
        Debug.Log(j);

        if (tmp[j] != null)
        {
            tmp[j].PrepareForDisplay(dialogue[j]);
            tmp[j].StartDisplay();
        }
        else
        {
            tmpUGUI[j].PrepareForDisplay(dialogue[j]);
            tmpUGUI[j].StartDisplay();
        }



    }

    public void MyAnimationEventHandler(AnimationEvent animationEvent)
    {
        int intParam = animationEvent.intParameter;
        float floatParam = animationEvent.floatParameter;

        Display(new int[] {intParam});




        // Etc.
    }


}
