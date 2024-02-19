using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffect;
    private float length, startPos;

    void Start()
    {
        startPos = transform.position.x;
        // length of the sprite
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        MoveBackground();
    }

    void MoveBackground()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        // the further that the camera is from its original origin, the larger distance gets
        // parallaxEffect must be between 0 and 1 for any proper effect
        float distance = cam.transform.position.x * parallaxEffect;

        // move the background layer based on distance
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // if you are getting too far from the backgrounds, move them over
        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
