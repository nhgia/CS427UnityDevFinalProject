using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleSource : MonoBehaviour
{
    public float maxDistance = 3f;     
    public float appearTime = 2f;
    public float speed = 3f;
    public float destroyAfterTime = 3f;

    [HideInInspector]
    public bool stop = false;
	[HideInInspector]
	public float stopDistance = 0f;

	float time = 0;

    private void Awake()
    {
        // create source on the lake
        Vector3 lakePos = GameObject.FindGameObjectWithTag("BORDERS").transform.position;
        transform.position = new Vector3(transform.position.x, lakePos.y, transform.position.z);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= appearTime) stop = true; // stop the ripple creation
        if (stop) stopDistance += Time.deltaTime; // stop water near the source
        if (time >= destroyAfterTime) Destroy(gameObject); // destroy the source
    }

  

}
