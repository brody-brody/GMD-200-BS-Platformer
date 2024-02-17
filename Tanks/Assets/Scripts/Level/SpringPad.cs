using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 normal = other.GetContact(other.contactCount - 1).normal;
            Vector2 upwardForce = new Vector2(0, jumpForce);

            if (normal == Vector3.down)
            {
                Debug.Log("bounce");
                other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(other.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0f);
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(upwardForce);
            }
        }
    }
}
