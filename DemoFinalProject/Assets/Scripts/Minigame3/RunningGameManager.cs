using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGameManager : MonoBehaviour
{
    public static RunningGameManager rgm;
    public Transform[] checkPoint;
    public Transform player;

    public Animator sceneChanger;
    public float endTime = 8f;

    public GameObject guide;

    int presentPoint = -1;

    private void Awake()
    {
        rgm = this;
        guide.SetActive(true);
    }

    private void Start()
    {
        DialogueSystem.ds.Display(new int[] { 8,9});
    }

    public void UpdateCheckPoint()
    {
        presentPoint += 1;
    }

    public void Restart()
    {
        player.position = checkPoint[presentPoint].position;

        player.transform.forward = checkPoint[presentPoint].right;
        player.GetComponent<CharacterControl>().newPos = checkPoint[presentPoint].position;
        player.GetComponent<CharacterControl>()._direction = checkPoint[presentPoint].right;
        player.GetComponent<CharacterControl>().change = checkPoint[presentPoint].GetComponent<RunningGameCheckPoint>().change;
    }

    public void End()
    {
        Invoke("EndScene", 8f);
    }

    public void EndScene()
    {
        sceneChanger.SetTrigger("FadeOut");
    }




}
