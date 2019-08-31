using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    Rigidbody2D rb2D;
    Vector3 _initPos;
    Vector2 _initVector;

	public float[] minSpeed;
    public float[] maxSpeed;
    public float speedInterval = 0.2f;


    public GameManager gm;
    Vector2 vel;
    float speed;
    int i = 0;

    bool playerLose = false;
    
    private void Awake()
    {
        playerLose = true;
        
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;
        _initPos = transform.position;
        //_initVector = new Vector2(-3, 3);
    }

    public void Start()
    {
        transform.position = _initPos;
        //        Debug.Log("Start Again");
        
        speed = minSpeed[i];
        vel = randomVector() * speed;

        rb2D.velocity = vel;
        //vel = rb2D.velocity;

//       Debug.Log(rb2D.velocity);
		
    }

    public void Restart(bool pLose)
    {
        ++i;
        playerLose = pLose;
        Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.tag);

         vel = rb2D.velocity;

        if (collision.gameObject.tag == "PlayerBound")
		{
            if (speed > maxSpeed[i]) speed = maxSpeed[i];

            AudioManager.am.PlayLose();
			gm.UpdateRatio(false);
		}
        else if (collision.gameObject.tag == "EnemyBound")
		{
            
            if (speed > maxSpeed[i]) speed = maxSpeed[i];

            AudioManager.am.PlayWin();
            gm.UpdateRatio(true);
		}

		if (collision.gameObject.tag == "BoundDown" || collision.gameObject.tag == "BoundUp")
        {
            // Debug.Log("BoundTrigger");
            AudioManager.am.BallCollide();

            vel.y *= -1;
            rb2D.velocity = vel;
        }
        else
        {
            AudioManager.am.BallCollide();
            //          Debug.Log("PlayerTrigger");
            speed += speedInterval;
            if (speed > maxSpeed[i]) speed = maxSpeed[i];

            vel.x *= -1;
            rb2D.velocity = vel * speed / minSpeed[i];
        }

       
         
//        Debug.Log(rb2D.velocity.magnitude);

    }

    Vector3 randomVector()
    {
        Vector3 result = new Vector3(0,0);

        if (!playerLose) result.x = Random.Range(2, 4);
        else result.x = - Random.Range(2, 4);

        int i = Random.Range(0, 2);
        if (i == 0) result.y = Random.Range(2,4);
        else result.y = - Random.Range(2, 4);

        Debug.Log(result);

        return result.normalized;
    }

    public void ResetVel()
    {
        i = 0;
    }

}

