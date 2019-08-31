using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FinalRoomManager : MonoBehaviour
{
    public static FinalRoomManager frm;

    public FirstPersonController rfpc;
    public float disPlayTime = 15f;
    public float finalMoveSpeed = 1f;

    public float endGameTime = 5f;
    public Animator animator;

    public GameObject[] textObjs;

    Vector3 direction;
    bool Move = false;

    private void Awake()
    {
        frm = this;
    }


    private void Start()
    {
        
        DialogueSystem.ds.Display(new int[] {0,1,2});
        Invoke("EnabledText", disPlayTime);
    }

    void EnabledText()
    {
        foreach (GameObject textObj in textObjs) textObj.SetActive(true);
        rfpc.enabled = true;
    }

    public void ChooseLife()
    {
        rfpc.enabled = false;

        Move = true;
        direction = Vector3.forward;

        DialogueSystem.ds.Display(new int[] {3});

        Invoke("EndGame", endGameTime);
    }

    public void ChooseMom()
    {
        rfpc.enabled = false;

        Move = true;
        direction = -Vector3.forward;

        DialogueSystem.ds.Display(new int[] {4});

        Invoke("EndGame", endGameTime);
    }

    private void Update()
    {
        if (Move)
        {
            Camera.main.transform.forward =
                Vector3.Lerp(Camera.main.transform.forward, direction, finalMoveSpeed * Time.deltaTime);
            rfpc.gameObject.transform.position =
                Vector3.Lerp(rfpc.gameObject.transform.position, rfpc.gameObject.transform.position + direction, finalMoveSpeed * Time.deltaTime);
        }


    }

    void EndGame()
    {
        animator.SetTrigger("FadeOut");

    }






}
