using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float maxSpeedX = 20f;
    [SerializeField] private float frictionDelay = 0.25f;
    [SerializeField] private GameObject tankBody;
    [SerializeField] private GameObject groundMap;
    [SerializeField] private Sprite tankBodyNormal;
    [SerializeField] private Sprite tankBodyFlipped;
    [SerializeField] private PhysicsMaterial2D friction, frictionless;

    public static bool grounded = false;
    private bool materialDelay = false;
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

        //Debug.Log("VelX: " + rb.velocity.x + " VelY: " + rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = other.GetContact(0).normal;

            if (normal == Vector3.up)
            {
                StartCoroutine(MaterialDelay());
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = other.GetContact(other.contactCount - 1).normal;

            if (normal == Vector3.up && materialDelay)
            {
                grounded = true;
                groundMap.GetComponent<Rigidbody2D>().sharedMaterial = friction;
                materialDelay = false;
            }
            if (rb.velocity.y < 0.1f)
            {
                Debug.Log("real af");
                groundMap.GetComponent<Rigidbody2D>().sharedMaterial = friction;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    // how the player moves
    void MoveTank(Vector2 input)
    {
        rb.AddForce(input * movementSpeed);
    }

    // Clamping velocity, otherwise you would accelerate infinitely
    void CapSpeed()
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeedX, maxSpeedX), rb.velocity.y);
    }

    // reduce movement speed while in air
    void AirControl()
    {
        if (!grounded)
        {
            movementSpeed = 20f;
            groundMap.GetComponent<Rigidbody2D>().sharedMaterial = frictionless;
        }
        else
        {
            movementSpeed = 75f;
        }
    }

    IEnumerator MaterialDelay()
    {
        materialDelay = false;
        yield return new WaitForSeconds(frictionDelay);
        materialDelay = true;
    }

    // Flips the tank sprite based on the players last horizontal input
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