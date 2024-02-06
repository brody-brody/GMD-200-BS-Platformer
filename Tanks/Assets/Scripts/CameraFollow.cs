using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform tank;


    // Update is called once per frame
    void Update()
    {
        Vector3 tankPosition = tank.position + offset;
        // through the smooth damp function, follow the target (player) but lag behind by an offset to achieve a smoother camera
        transform.position = Vector3.SmoothDamp(transform.position, tankPosition, ref velocity, smoothTime);
    }
}
