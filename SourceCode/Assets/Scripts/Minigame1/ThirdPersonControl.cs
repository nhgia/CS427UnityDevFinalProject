using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonControl : MonoBehaviour
{
    public float speed = 2f;
    public float existTime = 2f;

    float exist = 0, x, z;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        exist = existTime;
    }

    // Update is called once per frame
    void Update()
    {
        exist -= Time.deltaTime;
        if (exist < 0) {
            MazeManager.mz.Restart(gameObject);
            exist = existTime;
        }


        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        

        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector3(x, 0, z) * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        exist = existTime;
    }

}
