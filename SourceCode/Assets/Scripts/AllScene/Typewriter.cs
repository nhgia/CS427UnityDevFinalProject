using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class Typewriter : MonoBehaviour
{
    [Header("Dialogue display")]
    [TextArea]
    public string dialogue;
    public float maxSize = 50;
    public float sizeInterval = 2f;
    public float nextCharInterval = 1f;
    public bool isActive;
    public float distanceBetweenDialogue = 1f;
    public bool isDisappear = false;

    TextMeshPro _txtMesh;
    string[] _txtChar;
    float[] size;
    float time, testTime;
    int nextChar;

    bool isDisplayNextOne = false;
    float disPlayTime = 0;

    private void Awake()
    {
        
        _txtMesh = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        PrepareForDisplay(dialogue);
    }

    void Update()
    {
        if (isActive)
        {
            time += Time.deltaTime;
            if (time > nextCharInterval)
            {
                nextChar++;
                time = 0;

                if (nextChar > dialogue.Length)
                {
                    isDisplayNextOne = true;
                    nextChar = dialogue.Length;
                }
            }

            string tmp = "";


            for (int i = 0; i < dialogue.Length; i++)
            {

                tmp += "<size=" + size[i].ToString() + "%>" + dialogue[i] + "</size>";
            }

            for (int i = 0; i < nextChar; i++)
            {
                size[i] += sizeInterval;
                if (size[i] > maxSize) size[i] = maxSize;
            }

            _txtMesh.SetText(tmp);

        }

        if (isDisplayNextOne)
        {
            disPlayTime += Time.deltaTime;
            if (disPlayTime > distanceBetweenDialogue)
            {
                isActive = false;

                if (isDisappear) _txtMesh.SetText("");
                if (DialogueSystem.ds != null) DialogueSystem.ds.DisplayNextOne();

                isDisplayNextOne = false;
                disPlayTime = 0;
            }
        }


    }

    public void PrepareForDisplay(string newDia)
    {
        dialogue = newDia;
        _txtMesh.SetText("");

        size = new float[dialogue.Length];

        isDisplayNextOne = false;
        disPlayTime = 0;

        time = 0;
        nextChar = 0;
    }

    public void StartDisplay()
    {
        isActive = true;

    }

   
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            StartDisplay();
            
        }
    }

}
