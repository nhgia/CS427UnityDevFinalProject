using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayableDirector))]
public class HightlightObject : MonoBehaviour
{
	private Color startcolor;
    new Renderer renderer;

    public float distance = 1f;
    public Transform mainChar;

    public bool changeType = false;
    public Animator changeScene;

    bool isAccess = false;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        startcolor = renderer.material.color;
    }

    void OnMouseOver()
	{
        Vector3 newPos = transform.position;
        newPos.y = mainChar.position.y;

        if (Vector3.Distance(mainChar.position, newPos) < distance)
        {
            isAccess = true;
            renderer.material.color = Color.red;
        }
	}
	void OnMouseExit()
	{


        isAccess = false;

		renderer.material.color = startcolor;
	}

    private void Update()
    {
        if (isAccess && Input.GetMouseButtonDown(0))
        {
            DoSomething();
        } 



    }

    public void DoSomething()
    {
        if (changeType) changeScene.SetTrigger("FadeOut");
        else GetComponent<PlayableDirector>().Play();
    }




}
