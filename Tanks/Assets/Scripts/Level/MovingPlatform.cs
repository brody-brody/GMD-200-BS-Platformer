using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapted from "Unity: 2D Moving Platforms" (https://www.youtube.com/watch?v=GtX1p4cwYOc)
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float pSpeed;
    [SerializeField] private int startPoint;
    private const float HEIGHT_BUFFER = 1.5f;
    private const float POINT_BUFFER = 0.05f;
    private const float PLATFORM_SPEED = 10f;
    private GameObject target = null;
    private Vector3 offset;
    public Transform[] points;
    private int counter;

    void Start()
    {
        transform.position = points[startPoint].position;
        target = null;
    }

    void Update()
    {
        
        if (Vector2.Distance(transform.position, points[counter].position) < POINT_BUFFER)
        {
            counter++;

            if (counter == points.Length)
            {
                counter = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[counter].position, pSpeed * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.transform.position.y > transform.position.y + HEIGHT_BUFFER)
        {
            target = other.gameObject;
            offset = target.transform.position - transform.position;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        target = null;
    }
}
