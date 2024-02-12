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
    [SerializeField] private GameObject tankTreads;
    [SerializeField] private Sprite tankBodyNormal;
    [SerializeField] private Sprite tankBodyFlipped;
    [SerializeField] private PhysicsMaterial2D friction, frictionless;

    public static bool grounded = false;
    public bool frictionCheck = false;
    private bool materialDelay = false;
    private SpriteRenderer tankBodySpr;
    private Rigidbody2D rb;
    private Vector2 moveVector;
    private float horizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tankBodySpr = tankBody.GetComponent<SpriteRenderer>();
        groundMap = GameObject.FindWithTag("Ground");
    }

    void Update()
    {
        Flip();

        horizontal = Input.GetAxisRaw("Horizontal");
        tankTreads.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(horizontal));
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
                grounded = true;
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
            if ((normal == Vector3.right || normal == Vector3.left) && frictionCheck)
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                frictionCheck = false;
            }

            ContactPoint2D[] contacts = new ContactPoint2D[10];
            int numContacts = rb.GetContacts(contacts);
            //Debug.Log(numContacts);

            if (numContacts == 6 || numContacts == 8)
            {
                groundMap.GetComponent<Rigidbody2D>().sharedMaterial = friction;
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
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
            tankTreads.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (horizontal > 0f)
        {
            tankBodySpr.sprite = tankBodyNormal;
            tankTreads.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}