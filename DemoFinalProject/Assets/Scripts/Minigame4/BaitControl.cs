using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitControl : MonoBehaviour
{
    [Header("Bait transformation")]
    public float throwDistance = 10f;
    public float speed = 2f;
    public float returnSpeed = 5f;
    public float height = 10f;

    [Header("Object input")]
    public Transform baitPos;
    public Transform lakePos;
    public Transform player;

    [Header("Sound")]
    public AudioClip baitDrop;

    bool throwd = false;
    bool isThrowing = true;
    float time = 0;

    Transform _pos;
    Vector3 _dir;

    private void Start()
    {
        _pos = baitPos;
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && !throwd)
        {
            // throw if not throw yet
            ThrowBait(Camera.main.transform.forward);
        }

        if (Input.GetMouseButton(1) && !isThrowing && throwd)
        {
            // return if have alreadt throwd and throw completely
            Return();
        }

        // control the bait's position
        transform.position = Vector3.Lerp(transform.position, _pos.position, Time.deltaTime * returnSpeed);
        
        if (throwd && isThrowing)
        {
            // throw animation 
            time += Time.deltaTime * speed;
            transform.position = MathParabola.Parabola(baitPos.position,
                baitPos.position + _dir * throwDistance - Vector3.up * (transform.position.y - lakePos.position.y), height, time/5f);
        }
        
        
    }

    public void ThrowBait(Vector3 dir)
    {
        // start throwd bait
        throwd = true;
        _dir = dir;
        _pos = transform;
    }

    public void Return()
    {
        Debug.Log("Did return");
        // start return
        _pos = baitPos;
        isThrowing = true;
        throwd = false;
        time = 0;

        FishingGameManager.fgm.EndFishing(); 

    }


    private void OnTriggerStay(Collider other)
    {
        if (GetComponent<FishAttack>().enabled || !throwd) return; // if fish is attack or not throw => not doing anything

        if (other.gameObject.layer == 4) // start fishing activity and stop throwing
        {
            FishingGameManager.fgm.StartFishing();
            if (isThrowing) SoundManager.sm.PlayOneShot(baitDrop);


            isThrowing = false;
        }

        if (other.GetComponent<RippleSource>() != null) // reduce time if neccesary
        {
            Debug.Log("Happen");
            FishingGameManager.fgm.CheckWater();
        }

        
        
             
    }

   





}
