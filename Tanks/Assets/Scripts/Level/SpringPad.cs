using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;

    void OnCollisionEnter2D(Collision2D other)
    {
        // if the player collides with the spring
        if (other.gameObject.CompareTag("Player"))
        {
            // ensure that the player is on top of the spring pad by getting the normal, and comparing it
            Vector3 normal = other.GetContact(other.contactCount - 1).normal;
            Vector2 upwardForce = new Vector2(0, jumpForce);

            // give player upward velocity, removing any current y velocity first
            if (normal == Vector3.down)
            {
                other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(upwardForce);
            }
        }
    }
}
