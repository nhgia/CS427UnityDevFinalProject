using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform start;
    public Transform end;

    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start.position);
        lineRenderer.SetPosition(1, end.position);
        
    }
}
