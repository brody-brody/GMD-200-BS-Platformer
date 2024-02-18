using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockButton : MonoBehaviour
{
    [SerializeField] private Sprite unpressed, pressed;
    public static bool buttonPressed = false;
    private int triggers = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PushBlock") || other.CompareTag("Block"))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = pressed;
            triggers++;
            buttonPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PushBlock") || other.CompareTag("Block"))
        {
            if (triggers == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = unpressed;
                buttonPressed = false;
            }

            triggers--;
        }
    }
}
