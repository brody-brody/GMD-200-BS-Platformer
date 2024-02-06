using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float maxSpeedX = 20f;
    private Rigidbody2D rb;
    private Vector2 moveVector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);
    }

    void FixedUpdate()
    {
        rb.velocity = moveVector * movementSpeed;
        if (rb.velocity.x > maxSpeedX)
        {

            rb.velocity = new Vector2(maxSpeedX, rb.velocity.y);
        }

    }
}