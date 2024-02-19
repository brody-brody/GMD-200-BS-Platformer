using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockButton : MonoBehaviour
{
    [SerializeField] private Sprite unpressed, pressed;
    public static bool buttonPressed = false;
    private int triggers = 0;

    // if the player, push block, or regular block enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PushBlock") || other.CompareTag("Block"))
        {
            // switch to pressed sprite and increment triggers
            gameObject.GetComponent<SpriteRenderer>().sprite = pressed;
            triggers++;
            // static button pressed will open end door in doormanager.cs
            buttonPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PushBlock") || other.CompareTag("Block"))
        {
            // if there is only one trigger left, and is exiting, switch to unpressed sprite
            if (triggers == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = unpressed;
                buttonPressed = false;
            }

            triggers--;
        }
    }
}
