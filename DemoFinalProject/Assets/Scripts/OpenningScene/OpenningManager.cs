using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class OpenningManager : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }



    public void StarGame()
    {
        Debug.Log("Enter");
        GetComponent<PlayableDirector>().Play();


    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
