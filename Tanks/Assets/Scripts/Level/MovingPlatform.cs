using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapted from "Unity: 2D Moving Platforms" (https://www.youtube.com/watch?v=GtX1p4cwYOc)
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float pSpeed;
    [SerializeField] private int startPoint;
    private const float HEIGHT_BUFFER = 1.85f;
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
        
        // if the distance to the next point is less than a very small amount, increment the counter
        if (Vector2.Distance(transform.position, points[counter].position) < POINT_BUFFER)
        {
            counter++;

            if (counter == points.Length)
            {
                counter = 0;
            }
        }

        // position is constantly moving towards the next point based on the counter variable
        // speed is controller by pSpeed
        transform.position = Vector2.MoveTowards(transform.position, points[counter].position, pSpeed * Time.deltaTime);
    }

    void LateUpdate()
    {
        // if the target is assigned, which would be the player, move the player along the platform for clean movement.\
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        // if the player is actually above the platform
        if (other.gameObject.transform.position.y > transform.position.y + HEIGHT_BUFFER)
        {
            // set player as target, and set off set as the players offset to the position of the platform
            target = other.gameObject;
            offset = target.transform.position - transform.position;
        }
    }

    // once the player leaves the platform, dereference target variable
    void OnCollisionExit2D(Collision2D other)
    {
        target = null;
    }
}
