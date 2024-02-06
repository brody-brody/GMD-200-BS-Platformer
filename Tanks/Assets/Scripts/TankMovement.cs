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

        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rb.velocity = moveVector * movementSpeed;
        if (rb.velocity.x > maxSpeedX)
        {

            rb.velocity = new Vector2(maxSpeedX, rb.velocity.y);
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