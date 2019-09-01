using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    bool canMoveUp = true;
    bool canMoveDown = true;

    private void Update()
    {
//        Debug.Log(canMoveDown);
//        Debug.Log(canMoveUp);

        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        if ((y < 0 && canMoveDown) || (y > 0 && canMoveUp))
            transform.Translate(y * Vector2.left);
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canMoveUp = true;
        canMoveDown = true;
    }

}
