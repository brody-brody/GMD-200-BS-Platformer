using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float maxSpeedX = 20f;
    [SerializeField] private GameObject tankBody;
    [SerializeField] private Sprite tankBodyNormal;
    [SerializeField] private Sprite tankBodyFlipped;

    private SpriteRenderer tankBodySpr;
    private Rigidbody2D rb;
    private Vector2 moveVector;
    private float horizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tankBodySpr = tankBody.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Flip();

        horizontal = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        MoveTank(moveVector);
        CapSpeed();

        Debug.Log("VelX: " + rb.velocity.x + " VelY: " + rb.velocity.y);
    }

    void MoveTank(Vector2 input)
    {
        rb.AddForce(input * movementSpeed);
    }

    void CapSpeed()
    {
        if (rb.velocity.x > maxSpeedX)
        {
            rb.velocity = new Vector2(maxSpeedX, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxSpeedX)
        {
            rb.velocity = new Vector2(-maxSpeedX, rb.velocity.y);
        }
    }

    private void Flip()
    {
        if (horizontal < 0f)
        {
            tankBodySpr.sprite = tankBodyFlipped;
        }
        if (horizontal > 0f)
        {
            tankBodySpr.sprite = tankBodyNormal;
        }
    }
}