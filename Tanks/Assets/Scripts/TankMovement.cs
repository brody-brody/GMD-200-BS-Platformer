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

    public static bool grounded = false;
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
        AirControl();

        Debug.Log("VelX: " + rb.velocity.x + " VelY: " + rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = other.GetContact(0).normal;

            if (normal == Vector3.up)
                grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    void MoveTank(Vector2 input)
    {
        rb.AddForce(input * movementSpeed);
    }

    void CapSpeed()
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeedX, maxSpeedX), rb.velocity.y);
    }

    void AirControl()
    {
        if (!grounded)
        {
            movementSpeed = 20f;
        }
        else
        {
            movementSpeed = 75f;
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