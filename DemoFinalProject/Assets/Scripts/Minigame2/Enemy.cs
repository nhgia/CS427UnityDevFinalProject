using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ball;
    public float maxSpeed = 4f;
    public float minSpeed = 2f;
    public float stopTime = 2f;

    float time = 0f;
    bool canMoveUp = true;
    bool canMoveDown = true;

    private void Update()
    {
        time -= Time.deltaTime;
        if (time >= 0) return;

        float move = (ball.transform.position.y - transform.position.y) * minSpeed * Time.deltaTime;
        if ((move < 0 && canMoveDown) || (move > 0 && canMoveUp))
            transform.Translate(move * Vector2.left);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BoundUp")
        {
            canMoveUp = false;
        }

        if (collision.tag == "BoundDown")
        {
            canMoveDown = false;
        }

        if (collision.tag == "Obstacle")
        {
            Debug.Log("Stop");
            time = stopTime;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canMoveUp = true;
        canMoveDown = true;
    }



}
